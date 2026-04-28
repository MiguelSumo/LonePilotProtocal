using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Stats System")]
    [SerializeField] private ShipStats_SO stats;

    [Header("Events")]
    [SerializeField] private HealthChangedEventChannelSO healthChangedEvent;

    [Header("Movement")]
    public float moveSpeed = 7f;
    public float defaultMoveSpeed { get; private set; }

    [Header("Shooting")]
    [SerializeField] public Transform firePoint;
    public float fireRate = 0.15f;
    public float defaultFireRate { get; private set; }
    private float nextFireTime;

    [Header("Components")]
    [SerializeField] private GameEntityFactory factory;

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public bool IsShielded { get; set; }
    public bool IsInvincible { get; set; }

    private IMovementStrategy _currentStrategy;
    private IPlayerState _currentState;

    void Start()
    {
        // 1. Pull the upgraded values from your ScriptableObject
        RefreshStatsFromSO();

        // 2. Set the 'default' anchors for power-ups
        defaultMoveSpeed = moveSpeed;
        defaultFireRate = fireRate;

        // 3. Initialize Health
        CurrentHealth = MaxHealth;

        // 4. Tell the UI what the starting health is
        StartCoroutine(InitialUISync());

        _currentStrategy = new HybridMovementStrategy();
        SetState(new NormalState());
    }

    void Update()
    {
        _currentStrategy?.Move(transform, moveSpeed);
        _currentState?.UpdateState(this);
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            factory.CreateBullet(firePoint.position, firePoint.rotation);

            // ADD THIS LINE:
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound(AudioManager.Instance.bulletSound);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (IsInvincible) return;

        CurrentHealth -= amount;
        NotifyHealthChanged();

        if (CurrentHealth <= 0) Die();
    }

    // Call this from Start and from the Upgrade Shop
    public void RefreshStatsFromSO()
    {
        if (stats != null)
        {
            moveSpeed = stats.speed.GetTotalValue();
            MaxHealth = stats.health.GetTotalValue();
            // If you want to heal the player to full on upgrade:
            CurrentHealth = MaxHealth;
            NotifyHealthChanged();
        }
    }

    private IEnumerator InitialUISync()
    {
        // Give the UI one frame to initialize its listeners
        yield return new WaitForEndOfFrame();
        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        if (healthChangedEvent != null)
        {
            healthChangedEvent.RaiseEvent(CurrentHealth, MaxHealth);
        }
    }

    private void Die() => Debug.Log("Player has died.");

    public void SetStrategy(IMovementStrategy strategy) => _currentStrategy = strategy;
    public void SetState(IPlayerState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }
    public IPlayerState GetCurrentState() => _currentState;
}