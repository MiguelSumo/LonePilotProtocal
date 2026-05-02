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
            Debug.Log("Shield block reached");
            ShieldState shieldState = ship.GetCurrentState() as ShieldState;
            if (shieldState != null)
            {
                Debug.Log("ShieldState found, absorbing hit");
                shieldState.AbsorbHit(ship, damageInfo.Amount);
            }
            else
            {
                Debug.Log("ShieldState cast failed, current state is: " + ship.GetCurrentState()?.GetType().Name);
            }
            return;
        }
        else
        {
            Debug.Log("Shield block NOT reached, IsShielded: " + ship?.IsShielded);
        }


        /* if (ship != null && ship.IsShielded)
        {
            ShieldState shieldState = ship.GetCurrentState() as ShieldState;
            if (shieldState != null)
                shieldState.AbsorbHit(ship, damageInfo.Amount);
            return;
        } */


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

    public void Die()
    {
        // 1. Clean up the manager (this triggers OnDestroy in SingleGameManager)
        if (SingleGameManager.Instance != null)
        {
            Destroy(SingleGameManager.Instance.gameObject);
        }

        // 2. Unfreeze the game (just in case the settings menu was open)
        Time.timeScale = 1f;

        // 3. Go back to start
        SceneManager.LoadScene("Main Menu");
    }
}