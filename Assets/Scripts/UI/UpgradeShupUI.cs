using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeShopUI : MonoBehaviour
{
    [SerializeField] private ShipStats_SO stats;

    [Header("UI Text Displays")]
    [SerializeField] private TextMeshProUGUI damageValueText;
    [SerializeField] private TextMeshProUGUI healthValueText;
    [SerializeField] private TextMeshProUGUI speedValueText;

    [Header("Buttons")]
    [SerializeField] private Button buyDamageBtn;
    [SerializeField] private Button buyHealthBtn;
    [SerializeField] private Button buySpeedBtn;

    void Start()
    {
        // Hook up buttons via code (or do it in the Inspector)
        buyDamageBtn.onClick.AddListener(PurchaseDamage);
        buyHealthBtn.onClick.AddListener(PurchaseHealth);
        buySpeedBtn.onClick.AddListener(PurchaseSpeed);

        UpdateUI();
    }

    void PurchaseDamage() { stats.damage.Upgrade(); UpdateUI(); }
    void PurchaseHealth() { stats.health.Upgrade(); UpdateUI(); }
    void PurchaseSpeed() { stats.speed.Upgrade(); UpdateUI(); }

    public void UpdateUI()
    {
        // Shows the level (0, 1, 2...) as seen in your screenshot
        damageValueText.text = stats.damage.currentLevel.ToString();
        healthValueText.text = stats.health.currentLevel.ToString();
        speedValueText.text = stats.speed.currentLevel.ToString();
    }
}