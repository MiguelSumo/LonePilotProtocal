using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Stats System")]
    [SerializeField] private ShipStats_SO stats;

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
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;

    // These link directly to PlayerHealth to ensure one source of truth
    public float CurrentHealth => GetComponent<PlayerHealth>().GetCurrentHealth();
    public float MaxHealth => GetComponent<PlayerHealth>().GetMaxHealth();

    public bool IsShielded { get; set; }
    public bool IsInvincible { get; set; }

    private IMovementStrategy _currentStrategy;
    private IPlayerState _currentState;

    void Start()
    {
        // Pull movement values and trigger Health Sync in PlayerHealth
        RefreshStatsFromSO();

        defaultMoveSpeed = moveSpeed;
        defaultFireRate = fireRate;

        _currentStrategy = new HybridMovementStrategy();
        SetState(new NormalState());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (!isPaused)
        {
            _currentStrategy?.Move(transform, moveSpeed);
            _currentState?.UpdateState(this);
            HandleShooting();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            gameplayUI.SetActive(false);

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySound(AudioManager.Instance.pauseSound);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            gameplayUI.SetActive(true);

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySound(AudioManager.Instance.unPauseSound);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            factory.CreateBullet(firePoint.position, firePoint.rotation);

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound(AudioManager.Instance.bulletSound);
            }
        }
    }

    // Call this from the Upgrade Shop
    public void RefreshStatsFromSO()
    {
        if (stats != null)
        {
            moveSpeed = stats.speed.GetTotalValue();
            defaultMoveSpeed = moveSpeed;

            // This triggers the SyncWithSO logic in PlayerHealth to keep stats in sync
            PlayerHealth healthScript = GetComponent<PlayerHealth>();
            if (healthScript != null)
            {
                healthScript.SyncWithSO();
            }
        }
    }

    public void SetStrategy(IMovementStrategy strategy) => _currentStrategy = strategy;

    public void SetState(IPlayerState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public IPlayerState GetCurrentState() => _currentState;
}