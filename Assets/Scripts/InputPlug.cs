using System;
using UniRx;

public interface IInputMessage { }

public static class InputPlug
{
    static readonly Subject<IInputMessage> onInputMessageSubject = new();
    public static IObservable<IInputMessage> OnInputMessageObservable => onInputMessageSubject;
    public static IObserver<IInputMessage> OnInputMessageObserver => onInputMessageSubject;
}
