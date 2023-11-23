using System;
using UniRx;
using UnityEngine;

namespace View.Collider
{
    public class GetSlotPointCollider : MonoBehaviour
    {
        public IObservable<IInputMessage> OnGetSlotPoint => onGetSlotPointSubject;
        readonly Subject<IInputMessage> onGetSlotPointSubject = new();

        void OnTriggerEnter(UnityEngine.Collider other)
        {
            if (!other.TryGetComponent<CoinController>(out var coin))
                return;
            
            Destroy(coin.gameObject);
            onGetSlotPointSubject.OnNext(new GetObjectInputMessage());
            onGetSlotPointSubject.OnNext(new GetSlotPointInputMessage());
        }
    }
}