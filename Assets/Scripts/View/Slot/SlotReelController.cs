using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class SlotReelController : MonoBehaviour
{
    [SerializeField] SlotMarkImage slotMarkImagePrefab;
    [SerializeField] SlotMarkImage mainSlotMarkImage;
    [SerializeField] Transform backSlotMarkTransform;
    [SerializeField] CanvasGroup backSlotCanvasGroup;
    [SerializeField] float backSlotHeightOffset = -0.57f;
    [SerializeField] int backSlotIndexOffset = 3;
    [SerializeField] float backSlotReelSpeed = 2f;
    bool rotatingReel = false;
    Coroutine rotateCoroutine;
    List<SlotMarkImage> backSlotMarks = new();
    
    public void SetupReel()
    {
        foreach (SlotMark.Type type in Enum.GetValues(typeof(SlotMark.Type)))
        {
            var slotMarkImage = Instantiate(slotMarkImagePrefab, backSlotMarkTransform);
            var st = slotMarkImage.transform;
            st.localPosition = Vector3.zero;
            st.localRotation = Quaternion.identity;
            st.localScale = Vector3.one;
            slotMarkImage.SetMark(type);
            backSlotMarks.Add(slotMarkImage);
        }
        
        backSlotMarks = backSlotMarks.OrderBy(a => Guid.NewGuid()).ToList();
        var spriteSizeHeight = slotMarkImagePrefab.SpriteSize.y;
        for (var i = 0; i < backSlotMarks.Count; i++)
        {
            var t = backSlotMarks[i].RectTransform;
            var pos = t.anchoredPosition;
            pos.y = (i + backSlotIndexOffset) * (spriteSizeHeight + backSlotHeightOffset);
            t.anchoredPosition = pos;
        }

        backSlotCanvasGroup.alpha = 0;
    }
    
    public void StartReel()
    {
        if (rotatingReel || rotateCoroutine != null)
        {
            Debug.LogError("既に回転しているのに再度回そうとした");
            return;
        }

        rotatingReel = true;
        backSlotCanvasGroup.DOFade(0.6f, 0.5f);
        rotateCoroutine = StartCoroutine(ExecStartReel());
        mainSlotMarkImage.DoOut();
    }
    
    IEnumerator ExecStartReel()
    {
        while (true)
        {
            for (var i = 0; i < backSlotMarks.Count; i++)
            {
                var backSlotMark = backSlotMarks[i];
                var t = backSlotMark.RectTransform;
                var pos = t.anchoredPosition;
                pos.y += backSlotReelSpeed * Time.deltaTime;

                var limit = (backSlotIndexOffset + 0.5f) * (backSlotMark.SpriteSize.y + backSlotHeightOffset);
                if (pos.y < limit)
                {
                    var prevIndex = i == 0 ? backSlotMarks.Count - 1 : i - 1;
                    var prevT = backSlotMarks[prevIndex].RectTransform;
                    pos.y = prevT.anchoredPosition.y + (backSlotHeightOffset + backSlotMark.SpriteSize.y);
                }

                t.anchoredPosition = pos;
            }

            yield return null;
        }
    }

    public IEnumerator StopReel()
    {
        if (!rotatingReel || rotateCoroutine == null)
        {
            Debug.LogError("回転していないのに止めようとした");
            yield break;
        }

        rotatingReel = false;
        backSlotCanvasGroup.DOFade(0f, 0.6f).WaitForCompletion();
        yield return new WaitForSeconds(0.1f);
        mainSlotMarkImage.DoIn();
        yield return new WaitForSeconds(0.4f);
        StopCoroutine(rotateCoroutine);
        rotateCoroutine = null;
    }
}
