using UnityEngine;
using TMPro;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] private CurrencyDataSO careerData;
    [SerializeField] private TextMeshProUGUI shopCoinText;

    // This runs every time you open the Shop panel
    private void OnEnable()
    {
        if (careerData != null && shopCoinText != null)
        {
            shopCoinText.text = $": {careerData.TotalCoins}";
        }
    }
}

