using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform Player { get; private set; }

    [Header("Data Persistence")]
    [SerializeField] private ShipStats_SO playerStats; // Drag ShipStats_SO here

    [Header("Core Systems")]
    [SerializeField] private GameObject bulletPool;
    [SerializeField] private GameObject asteroidManager;
    [SerializeField] private GameObject enemySpawner;

    [Header("New Spawners & Managers")]
    [SerializeField] private GameObject gameFactory;
    [SerializeField] private GameObject lootSpawner;
    [SerializeField] private GameObject scoreManager;
    [SerializeField] private GameObject powerUpSpawner;

    [Header("UI Control")]
    [SerializeField] private GameObject gameUI;    
    [SerializeField] private GameObject shopPanel; 

    [Header("Wave Reference")]
    [SerializeField] private WaveManager waveManager; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // --- THE RESET FIX ---
        // We reset here because Awake runs before any Start() methods.
        // This guarantees the ShipController reads Level 0 stats.
        if (playerStats != null)
        {
            playerStats.InitializeNewRun();
        }
    }

    private void Start()
    {
        if (gameUI != null) gameUI.SetActive(true);
        if (shopPanel != null) shopPanel.SetActive(false);

        StartCoroutine(SystemStartupSequence());
    }

    private IEnumerator SystemStartupSequence()
    {
        if (gameFactory != null) gameFactory.SetActive(true);
        if (scoreManager != null) scoreManager.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        if (bulletPool != null) bulletPool.SetActive(true);
        if (lootSpawner != null) lootSpawner.SetActive(true);
        if (powerUpSpawner != null) powerUpSpawner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        if (asteroidManager != null) asteroidManager.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        if (enemySpawner != null) enemySpawner.SetActive(true);

        Debug.Log("Solo Pilot Protocol: Systems Nominal.");
    }

    public void ShowShop()
    {
        if (gameUI != null) gameUI.SetActive(false); 
        if (shopPanel != null) shopPanel.SetActive(true);  
        Debug.Log("GameManager: HUD hidden, Shop opened.");
    }

    public void HideShop()
    {
        if (shopPanel != null) shopPanel.SetActive(false); 
        if (gameUI != null) gameUI.SetActive(true);  
        Debug.Log("GameManager: Shop hidden, HUD restored.");
    }

    public void OnContinueButtonClicked()
    {
        if (waveManager != null)
        {
            waveManager.SkipShopTimer();
        }
        else
        {
            Debug.LogWarning("GameManager: WaveManager reference missing!");
        }
    }

    public void RegisterPlayer(Transform playerTransform)
    {
        Player = playerTransform;
    }
}