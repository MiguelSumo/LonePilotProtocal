using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "IntEvent",
    menuName = "Core/Events/Int Event Channel"
)]
public class IntEventChannelSO : ScriptableObject
{
    public event Action<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}