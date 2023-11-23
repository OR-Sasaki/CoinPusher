using UnityEngine;

public record OnClickFallCoinInputMessage : IInputMessage
{
    public Vector3 ClickPosition { get; init; }
}