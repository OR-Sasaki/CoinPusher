using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
    [SerializeField] GetSlotPointCollider getSlotPointCollider;
    [SerializeField] SlotReelsManager slotReelsManager;
    
    void Start()
    {
        getCoinCollider.OnGetCoin.Subscribe(onInputMessageSubject);
        getSlotPointCollider.OnGetSlotPoint.Subscribe(onInputMessageSubject);
        slotReelsManager.Setup();
    }

    public void OnOutputMessage(IOutputMessage outputMessage)
    {
        switch (outputMessage)
        {
            case FallCoinOutputMessage fallCoin:
                fallCoinController.FallCoin(fallCoin.ClickPosition);
                break;
            case DirectionOutputMessage direction:
                StartCoroutine(Direction());
                break;
        }
        
        onReceiveOutputMessageEvent.Invoke(outputMessage);
    }

    void Update()
    {
        onInputMessageSubject.OnNext(new EveryFrameInputMessage());

        if (Input.GetKeyDown(KeyCode.A))
        {
            onInputMessageSubject.OnNext(new GetSlotPointInputMessage());
        }
        
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = tapZ;
            onInputMessageSubject
                .OnNext(
                    new OnClickFallCoinInputMessage
                    {
                        ClickPosition = Camera.main == null ? Vector3.zero : Camera.main.ScreenToWorldPoint(mousePosition)
                    }
                );
        }
    }

    IEnumerator Direction()
    {
        slotReelsManager.StartRotateAllReel();
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(slotReelsManager.StopRotateAllReel());
        yield return new WaitForSeconds(0.1f);
        onInputMessageSubject.OnNext(new DirectionInputMessage());
    }
}
