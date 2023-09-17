using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using View;
using View.Collider;

public class ViewManager : SingletonMonoBehaviour<ViewManager>
{
    readonly Subject<IInputMessage> onInputMessageSubject = new();
    public IObservable<IInputMessage> OnInputMessage => onInputMessageSubject;

    public readonly UnityEvent<IOutputMessage> onReceiveOutputMessageEvent = new();
    
    [SerializeField] float tapZ = 5;
    [SerializeField] FallCoinController fallCoinController;
    [SerializeField] GetCoinCollider getCoinCollider;
    
    void Start()
    {
        getCoinCollider.OnGetCoin.Subscribe(onInputMessageSubject);
    }

    public void OnOutputMessage(IOutputMessage outputMessage)
    {
        switch (outputMessage)
        {
            case FallCoinOutputMessage fallCoin:
                fallCoinController.FallCoin(fallCoin.ClickPosition);
                break;
        }
        
        onReceiveOutputMessageEvent.Invoke(outputMessage);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = tapZ;
            onInputMessageSubject
                .OnNext(
                    new FallCoinInputMessage
                    {
                        ClickPosition = Camera.main == null ? Vector3.zero : Camera.main.ScreenToWorldPoint(mousePosition)
                    }
                );
        }
    }
}
