using System.Collections;
using UnityEngine;

public class SlotReelsManager : MonoBehaviour
{
    [SerializeField] SlotReelController slotReelControllerPrefab;
    [SerializeField] Transform leftSlotReelPosition;
    [SerializeField] Transform centerSlotReelPosition;
    [SerializeField] Transform rightSlotReelPosition;
    SlotReelController leftSlotReelController;
    SlotReelController centerSlotReelController;
    SlotReelController rightSlotReelController;
    
    public void Setup()
    {
        leftSlotReelController = SetupReel(leftSlotReelPosition);
        centerSlotReelController = SetupReel(centerSlotReelPosition);
        rightSlotReelController = SetupReel(rightSlotReelPosition);
    }

    SlotReelController SetupReel(Transform parent)
    {
        var slotReel = Instantiate(slotReelControllerPrefab, parent);
        var t = slotReel.transform;
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        slotReel.SetupReel();
        return slotReel;
    }

    public void StartRotateAllReel()
    {
        leftSlotReelController.StartReel();
        centerSlotReelController.StartReel();
        rightSlotReelController.StartReel();
    }
    
    public IEnumerator StopRotateAllReel()
    {
        yield return StartCoroutine(leftSlotReelController.StopReel());
        yield return StartCoroutine(centerSlotReelController.StopReel());
        yield return StartCoroutine(rightSlotReelController.StopReel());
    }
}
