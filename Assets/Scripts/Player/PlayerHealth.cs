using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Event Channels")]
    [SerializeField] private HealthChangedEventChannelSO healthChangedEvent;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        NotifyHealthChanged();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        healthChangedEvent.RaiseEvent(currentHealth, maxHealth);
    }
}