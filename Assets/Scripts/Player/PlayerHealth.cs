using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Event Channels")]
    [SerializeField] private HealthChangedEventChannelSO healthChangedEvent;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    public Team Team => Team.Player;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        NotifyHealthChanged();
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        ShipController ship = GetComponent<ShipController>();

        if (ship != null && ship.IsInvincible) return;

        if (ship != null && ship.IsShielded)
        {
            ShieldState shieldState = ship.GetCurrentState() as ShieldState;
            if (shieldState != null)
                shieldState.AbsorbHit(ship, damageInfo.Amount);
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth - damageInfo.Amount, 0f, maxHealth);
        NotifyHealthChanged();
        if (currentHealth <= 0)
        {
            Die();
        }

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

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        healthChangedEvent.RaiseEvent(currentHealth, maxHealth);
    }

    private void Die()
    {
        //Diesound
        SceneManager.LoadScene("Main Menu");
    }
}