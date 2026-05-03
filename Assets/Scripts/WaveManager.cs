using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Needed for the timer text

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public int currentWave = 0;
    [SerializeField] private float timeBetweenWaves = 20.0f;
    [SerializeField] private float endWaveBuffer = 4.0f;
    [SerializeField] private IntEventChannelSO waveEvent;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI shopTimerText; // Drag your Timer Text here

    [Header("References")]
    public EnemySpawner spawner;

    private List<Enemy> aliveEnemies = new List<Enemy>();

    void Start()
    {
        StartCoroutine(StartFirstWave());
    }

    private IEnumerator StartFirstWave()
    {
        while (GameManager.Instance.Player == null) yield return null;
        yield return new WaitForSeconds(2f);
        StartWave();
    }

    public void StartWave()
    {
        Time.timeScale = 1f;
        if (GameManager.Instance != null) GameManager.Instance.HideShop();

        int enemiesPerWave = Mathf.Min(2 + (currentWave / 2), 12);
        if (waveEvent != null) waveEvent.RaiseEvent(currentWave);

        EnemyDamageMultiplier = 1f + (currentWave * 0.13f);
        EnemyHealthMultiplier = 1f + (currentWave * 0.05f);
        currentWave += 1;

        if (spawner != null) spawner.SpawnEnemies(this, enemiesPerWave);
    }

    public void SkipShopTimer()
    {
        StopAllCoroutines();
        StartWave();
    }

    private void EndWave()
    {
        StartCoroutine(WaveTransitionSequence());
    }

    private IEnumerator WaveTransitionSequence()
    {
        yield return new WaitForSeconds(endWaveBuffer);
        Time.timeScale = 0f;
        if (GameManager.Instance != null) GameManager.Instance.ShowShop();

        // Start the countdown
        StartCoroutine(ShopTimerCoroutine());
    }

    private IEnumerator ShopTimerCoroutine()
    {
        float timeRemaining = timeBetweenWaves;

        while (timeRemaining > 0)
        {
            if (shopTimerText != null)
            {
                // Rounds to a whole number for a clean look
                shopTimerText.text = $"NEXT WAVE IN: {Mathf.CeilToInt(timeRemaining)}s";
            }

            // Must use Realtime because Time.timeScale is 0!
            yield return new WaitForSecondsRealtime(1f);
            timeRemaining -= 1f;
        }

        StartWave();
    }

    // --- Mediator Methods ---
    public static float EnemyHealthMultiplier = 1f;
    public static float EnemyDamageMultiplier = 1f;

    public void RegisterEnemy(Enemy enemy)
    {
        if (!aliveEnemies.Contains(enemy)) aliveEnemies.Add(enemy);
    }

    public void OnEnemyDied(Enemy enemy)
    {
        if (aliveEnemies.Contains(enemy)) aliveEnemies.Remove(enemy);
        if (aliveEnemies.Count <= 0) EndWave();
    }
}