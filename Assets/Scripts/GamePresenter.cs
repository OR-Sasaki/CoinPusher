using UniRx;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    void Start()
    {
        LogicManager.OnOutputMessage.Subscribe(OutputPlug.OnOutputMessageObserver);
        ViewManager.I.OnInputMessage.Subscribe(InputPlug.OnInputMessageObserver);

        InputPlug.OnInputMessageObservable.Subscribe(LogicManager.OnInputMessage);
        OutputPlug.OnOutputMessageObservable.Subscribe(ViewManager.I.OnOutputMessage);
    }
}
