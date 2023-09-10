using UnityEngine;

public record FallCoinInputMessage : IInputMessage
{
    public Vector3 ClickPosition { get; init; }
}