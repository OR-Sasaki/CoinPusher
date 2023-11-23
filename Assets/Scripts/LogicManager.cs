using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using View.Collider;

public static class LogicManager
{
    static readonly Subject<IOutputMessage> onOutputMessageSubject = new();
    public static IObservable<IOutputMessage> OnOutputMessage => onOutputMessageSubject;

    public static void OnInputMessage(IInputMessage inputMessage)
    {
        switch (inputMessage)
        {
            case OnClickFallCoinInputMessage fallCoin:
            {
                onOutputMessageSubject.OnNext(new FallCoinOutputMessage { ClickPosition = fallCoin.ClickPosition });
                if (DataStore.coinNum > 0) DataStore.coinNum--;
                onOutputMessageSubject.OnNext(new OnChangeCoinOutputMessage());
                break;
            }
            case GetObjectInputMessage getObject:
            {
                DataStore.coinNum++;
                onOutputMessageSubject.OnNext(new OnChangeCoinOutputMessage());
                break;
            }
            case GetSlotPointInputMessage getSlotPoint:
            {
                DataStore.slotPointNum++;
                break;
            }
            case DirectionInputMessage direction:
            {
                DataStore.slotStatus.inDirection = false;
                break;
            }
            case EveryFrameInputMessage everyFrame:
            {
                // CheckSlotPoint
                if (!DataStore.slotStatus.inDirection && DataStore.slotPointNum > 0)
                {
                    // 演出開始
                    DataStore.slotPointNum--;
                    DataStore.slotStatus.inDirection = true;
                    onOutputMessageSubject.OnNext(new OnChangeCoinOutputMessage());
                    onOutputMessageSubject.OnNext(new DirectionOutputMessage());
                }
                break;
            }
        }
    }
}
