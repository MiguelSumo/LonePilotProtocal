using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeShopUI : MonoBehaviour
{
    [SerializeField] private ShipStats_SO stats;
    [SerializeField] private ShipController player;

    [Header("Value Displays (Base + Bonus)")]
    [SerializeField] private TextMeshProUGUI damageValueText;
    [SerializeField] private TextMeshProUGUI healthValueText;
    [SerializeField] private TextMeshProUGUI speedValueText;

    [Header("Cost Displays")]
    [SerializeField] private TextMeshProUGUI damageCostText;
    [SerializeField] private TextMeshProUGUI healthCostText;
    [SerializeField] private TextMeshProUGUI speedCostText;

    [Header("Buttons")]
    [SerializeField] private Button buyDamageBtn;
    [SerializeField] private Button buyHealthBtn;
    [SerializeField] private Button buySpeedBtn;

    private UpgradeVisitor _upgrader = new UpgradeVisitor();

    void Start()
    {
        // Link buttons to logic
        if (buyDamageBtn != null) buyDamageBtn.onClick.AddListener(PurchaseDamage);
        if (buyHealthBtn != null) buyHealthBtn.onClick.AddListener(PurchaseHealth);
        if (buySpeedBtn != null) buySpeedBtn.onClick.AddListener(PurchaseSpeed);

        UpdateUI();
    }

    // --- Purchase Methods ---

    void PurchaseDamage() => TryPurchase(stats.damage, StatType.Damage, "Damage");
    void PurchaseHealth() => TryPurchase(stats.health, StatType.Health, "Health");
    void PurchaseSpeed() => TryPurchase(stats.speed, StatType.Speed, "Speed");

    private void TryPurchase(Stat statToUpgrade, StatType type, string logName)
    {
        int cost = statToUpgrade.GetUpgradeCost();

        // 1. Check if ScoreManager exists and player has enough score
        if (ScoreManager.Instance != null && ScoreManager.Instance.CurrentScore >= cost)
        {
            // 2. Subtract the score
            ScoreManager.Instance.SubtractScore(cost);

            // 3. Perform the upgrade
            stats.Accept(_upgrader, type);

            // 4. Update the world
            OnUpgradeComplete();

            Debug.Log($"Purchased {logName} upgrade for {cost}.");
        }
        else
        {
            Debug.Log($"Cannot afford {logName}! Need: {cost}");
        }
    }

    private void OnUpgradeComplete()
    {
        UpdateUI();
        // Refresh ship values immediately
        if (player != null) player.RefreshStatsFromSO();
    }

    // --- UI Display Logic ---

    public void UpdateUI()
    {
        if (stats == null) return;

        // Display Stats as "Base + Bonus"
        damageValueText.text = $"{stats.damage.BaseValue} + {stats.damage.GetBonusValue()}";
        healthValueText.text = $"{stats.health.BaseValue} + {stats.health.GetBonusValue()}";
        speedValueText.text = $"{stats.speed.BaseValue} + {stats.speed.GetBonusValue()}";

        // Display Price tags
        if (damageCostText != null) damageCostText.text = $"Cost: {stats.damage.GetUpgradeCost()}";
        if (healthCostText != null) healthCostText.text = $"Cost: {stats.health.GetUpgradeCost()}";
        if (speedCostText != null) speedCostText.text = $"Cost: {stats.speed.GetUpgradeCost()}";
    }
}