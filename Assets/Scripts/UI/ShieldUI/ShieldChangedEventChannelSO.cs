using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Shield Changed Event Channel")]
public class ShieldChangedEventChannelSO : ScriptableObject
{
    public UnityAction<float> OnEventRaised;

    public void RaiseEvent(float shieldAmount)
    {
        OnEventRaised?.Invoke(shieldAmount);
    }
}