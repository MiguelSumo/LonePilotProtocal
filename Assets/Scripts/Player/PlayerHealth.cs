using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    //Currency Reference 
    [SerializeField] private CurrencyDataSO currencyData;

    [Header("Event Channels")]
    [SerializeField] private HealthChangedEventChannelSO healthChangedEvent;

    [Header("Health Settings")]
    [SerializeField] private ShipStats_SO playerStats;

    private float maxHealth;
    private float currentHealth;

    public Team Team => Team.Player;

    private void Awake()
    {
        // Initialize health before anything else happens
        SyncWithSO();
    }

    /// <summary>
    /// Updates the health limit based on the ScriptableObject.
    /// Ensures only the bonus health is added to the current pool.
    /// </summary>
    public void SyncWithSO()
    {
        if (playerStats == null) return;

        float upgradedMax = playerStats.health.GetTotalValue();

        // Safety: ensure max health is at least at a base level
        if (upgradedMax <= 0) upgradedMax = 100f;

        if (maxHealth > 0)
        {
            // Calculate the bonus gain (e.g., if max went from 100 to 110, diff is 10)
            float healthIncrease = upgradedMax - maxHealth;

            maxHealth = upgradedMax;

            // Only add the extra capacity you bought
            currentHealth += healthIncrease;
        }
        else
        {
            // Initial setup on game start
            maxHealth = upgradedMax;
            currentHealth = maxHealth;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        NotifyHealthChanged();
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        ShipController ship = GetComponent<ShipController>();

        if (ship != null && ship.IsInvincible) return;

        // Shield Handling
        if (ship != null && ship.IsShielded)
        {
            ShieldState shieldState = ship.GetCurrentState() as ShieldState;
            if (shieldState != null)
            {
                shieldState.AbsorbHit(ship, damageInfo.Amount);
            }
            return;
        }

        // Subtract damage and clamp to current maxHealth
        currentHealth = Mathf.Clamp(currentHealth - damageInfo.Amount, 0f, maxHealth);
        NotifyHealthChanged();

        if (currentHealth <= 0)
        {
            Die();
        }

        HandleDamageSFX(damageInfo);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        if (healthChangedEvent != null)
            healthChangedEvent.RaiseEvent(currentHealth, maxHealth);
    }

    private void HandleDamageSFX(DamageInfo damageInfo)
    {
        if (AudioManager.Instance == null) return;

        switch (damageInfo.Type)
        {
            case DamageType.Enemy:
                AudioManager.Instance.PlaySound(AudioManager.Instance.enemyAttackSound);
                break;
            case DamageType.Asteroid:
                AudioManager.Instance.PlaySound(AudioManager.Instance.asteriodHitSound);
                break;
        }
    }

    public void Die()
    {
        Debug.Log("Player Died: Resetting Progress.");
    
        GameData.currentScore = ScoreManager.Instance.CurrentScore;

        currencyData.ResetCoins();

        if (playerStats != null)
        {
            ResetVisitor resetter = new ResetVisitor();
            playerStats.Accept(resetter, StatType.Damage);
            playerStats.Accept(resetter, StatType.Health);
            playerStats.Accept(resetter, StatType.Speed);

            ShipController ship = GetComponent<ShipController>();
            if (ship != null)
            {
                ship.RefreshStatsFromSO();
            }
        }

        if (SingleGameManager.Instance != null)
        {
            Destroy(SingleGameManager.Instance.gameObject);
        }

        Time.timeScale = 1f;
       // SceneManager.LoadScene("Main Menu");
       SceneManager.LoadScene("EndGameScene");
    }

    // These link the Health script back to the ShipController "Wrappers"
    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
}