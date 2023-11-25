using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMarkImage : MonoBehaviour
{
    public SlotMark.Type slotMarkType;
    [SerializeField] Image image;
    [SerializeField] Animator animator;

    [Serializable]
    class SlotMarkImageContext
    {
        public SlotMark.Type SlotMarkType;
        public Sprite Sprite;
    }

    [SerializeField]
    List<SlotMarkImageContext> slotMarkImageContexts;
    
    Vector2 spriteSize;
    public Vector2 SpriteSize
    {
        get
        {
            if (spriteSize.magnitude == 0)
                spriteSize = image.rectTransform.rect.size;
            return spriteSize;
        }
    }

    RectTransform rectTransform;
    static readonly int Out = Animator.StringToHash("Out");
    static readonly int In = Animator.StringToHash("In");

    public RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            return rectTransform;
        }
    }

    public void SetMark(SlotMark.Type setSlotMarkType)
    {
        slotMarkType = setSlotMarkType;
        var sprite = slotMarkImageContexts.Find(c => c.SlotMarkType == setSlotMarkType).Sprite;
        image.sprite = sprite;
    }

    public void DoOut()
    {
        animator.Play("Out");
    }
    
    public void DoIn()
    {
        animator.Play("In");
    }
}