using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class UIShieldController : MonoBehaviour
{
    [SerializeField] private ShieldChangedEventChannelSO shieldChangedEvent;
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private TMP_Text shieldText;

    private void OnEnable()
    {
        shieldChangedEvent.OnEventRaised += UpdateShieldUI;
    }

    private void OnDisable()
    {
        shieldChangedEvent.OnEventRaised -= UpdateShieldUI;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void UpdateShieldUI(float amount)
    {
        shieldSlider.value = amount;
        shieldText.text = amount > 0 ? "Shield Active" : "";
        gameObject.SetActive(amount > 0);
    }
}