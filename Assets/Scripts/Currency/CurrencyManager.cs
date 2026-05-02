using UnityEngine; // This fixes the MonoBehaviour and SerializeField errors
using TMPro;       // This fixes the TextMeshProUGUI errors

public class CurrencyManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private IntEventChannelSO currencyEventChannel;

    [Header("Data")]
    [SerializeField] private CurrencyDataSO bankAccount; // Drag your "PlayerInventory" asset here

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI currencyText;

    private void OnEnable()
    {
        if (currencyEventChannel != null)
        {
            currencyEventChannel.OnEventRaised += HandleCoinCollected;
        }

        UpdateUI();
    }

    private void OnDisable()
    {
        if (currencyEventChannel != null)
        {
            currencyEventChannel.OnEventRaised -= HandleCoinCollected;
        }
    }

    private void HandleCoinCollected(int amount)
    {
        if (bankAccount != null)
        {
            bankAccount.AddCoins(amount);
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (currencyText != null && bankAccount != null)
        {
            // Displays the persistent balance from your ScriptableObject
            currencyText.text = $": {bankAccount.TotalCoins}";
        }
    }
}