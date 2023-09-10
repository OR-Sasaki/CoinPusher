using System;
using UniRx;
using UnityEngine;
using View;

public class ViewManager : SingletonMonoBehaviour<ViewManager>
{
    readonly Subject<IInputMessage> onInputMessageSubject = new();
    public IObservable<IInputMessage> OnInputMessage => onInputMessageSubject;

    [SerializeField] float tapZ = 5;
    
    public void OnOutputMessage(IOutputMessage outputMessage)
    {
        switch (outputMessage)
        {
            case FallCoinOutputMessage fallCoin:
                FallCoinController.I.FallCoin(fallCoin.ClickPosition);
                break;
        }
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
