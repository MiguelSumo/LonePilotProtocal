using TMPro;
using UnityEngine;

public class UIValueDisplay : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO valueEventChannel;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private string prefix = "Score: ";

    private void OnEnable()
    {
        if (valueEventChannel != null)
            valueEventChannel.OnEventRaised += UpdateUI;
    }

    private void OnDisable()
    {
        if (valueEventChannel != null)
            valueEventChannel.OnEventRaised -= UpdateUI;
    }

    private void UpdateUI(int newValue)
    {
        // For the shop, newValue might be the total, 
        // for the game UI, it might be the current run score.
        displayText.text = $"{prefix}{newValue}";
    }
}