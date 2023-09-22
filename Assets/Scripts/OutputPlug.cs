using System;
using UniRx;

public interface IOutputMessage { }

public static class OutputPlug
{
    static readonly Subject<IOutputMessage> onOutputMessageSubject = new();
    public static IObservable<IOutputMessage> OnOutputMessageObservable => onOutputMessageSubject;
    public static IObserver<IOutputMessage> OnOutputMessageObserver => onOutputMessageSubject;
}
