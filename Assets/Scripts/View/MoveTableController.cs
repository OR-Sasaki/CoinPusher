using DG.Tweening;
using UnityEngine;

namespace View
{
    public class MoveTableController : MonoBehaviour
    {
        [SerializeField] float maxZ;
        [SerializeField] float minZ;
        [SerializeField] float duration;
        [SerializeField] Rigidbody rd;
        bool nowPush;
        
        void Start()
        {
           var seq = DOTween.Sequence();
           seq.Append(rd.DOMoveZ(maxZ, duration).SetEase(Ease.InOutSine));
           seq.Append(rd.DOMoveZ(minZ, duration).SetEase(Ease.InOutSine));
           seq.SetLoops(-1).Play();
        }
    }
}