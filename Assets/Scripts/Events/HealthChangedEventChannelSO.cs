using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "HealthChangedEvent",
    menuName = "Core/Events/Health Changed Event Channel"
)]
public class HealthChangedEventChannelSO : ScriptableObject
{
    public event Action<float, float> OnEventRaised;

    public void RaiseEvent(float currentHealth, float maxHealth)
    {
        OnEventRaised?.Invoke(currentHealth, maxHealth);
    }
}
