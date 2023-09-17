using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public static class LogicManager
{
    static readonly Subject<IOutputMessage> onOutputMessageSubject = new();
    public static IObservable<IOutputMessage> OnOutputMessage => onOutputMessageSubject;

    public static void OnInputMessage(IInputMessage inputMessage)
    {
        switch (inputMessage)
        {
            case FallCoinInputMessage fallCoin:
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    onOutputMessageSubject.OnNext(new FallCoinOutputMessage { ClickPosition = fallCoin.ClickPosition });
                    if (DataStore.CoinNum > 0) DataStore.CoinNum--;
                    onOutputMessageSubject.OnNext(new OnChangeCoinOutputMessage());
                }
                break;
            case GetObjectInputMessage getObject:
                DataStore.CoinNum++;
                onOutputMessageSubject.OnNext(new OnChangeCoinOutputMessage());
                break;
        }
    }
}
