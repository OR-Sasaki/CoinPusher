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
    
    void Start()
    {
        getCoinCollider.OnGetCoin.Subscribe(onInputMessageSubject);
        getSlotPointCollider.OnGetSlotPoint.Subscribe(onInputMessageSubject);
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
        Debug.Log("Start Direction");
        yield return new WaitForSeconds(5);
        Debug.Log("End Direction");
        onInputMessageSubject.OnNext(new DirectionInputMessage());
    }
}
