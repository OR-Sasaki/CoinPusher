using System;
using UniRx;
using UnityEngine;

namespace View.Collider
{
    public class GetCoinCollider : MonoBehaviour
    {
        public IObservable<IInputMessage> OnGetCoin => onGetCoinSubject;
        readonly Subject<IInputMessage> onGetCoinSubject = new();

        void OnTriggerEnter(UnityEngine.Collider other)
        {
            if (!other.TryGetComponent<CoinController>(out var coin))
                return;
            
            onGetCoinSubject.OnNext(new GetObjectInputMessage());
        }
    }
}