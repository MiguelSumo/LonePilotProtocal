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
        currentHealth = Mathf.Clamp(currentHealth - damageInfo.Amount, 0f, maxHealth);
        NotifyHealthChanged();
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void NotifyHealthChanged()
    {
        healthChangedEvent.RaiseEvent(currentHealth, maxHealth);
    }


    private void Die()
    {
        SceneManager.LoadScene("Main Menu");
    }

}