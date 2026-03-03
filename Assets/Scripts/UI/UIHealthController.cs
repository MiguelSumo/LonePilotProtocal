
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    [SerializeField] private HealthChangedEventChannelSO healthChangedEvent;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    private void OnEnable()
    {
        healthChangedEvent.OnEventRaised += UpdateHealthUI;
    }

    private void OnDisable()
    {
        healthChangedEvent.OnEventRaised -= UpdateHealthUI;
    }

    private void UpdateHealthUI(float current, float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = current;
        healthText.text = $"{Mathf.RoundToInt(current)}/{max}";
    }
}