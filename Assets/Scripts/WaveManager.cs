using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Variables
    public int currentWave = 0;
    public static float EnemyHealthMultiplier = 1f;
    public static float EnemyDamageMultiplier = 1f;



    private int enemiesPerWave;
    [SerializeField] private float timeBetweenWaves = 15.0f;
    [SerializeField] private IntEventChannelSO waveEvent;


    private List<Enemy> aliveEnemies = new List<Enemy>();


    public EnemySpawner spawner;

    void Start()
    {
        StartCoroutine(StartFirstWave());
    }

    private IEnumerator StartFirstWave()
    {
        // Wait until player exists in the scene
        while (GameManager.Instance.Player == null)
        {
            yield return null;
        }

        // Optional: small delay so everything loads cleanly
        yield return new WaitForSeconds(2f);

        StartWave();
    }

    public void StartWave()
    {
        enemiesPerWave = Mathf.Min(2 + (currentWave / 2), 12);
        Debug.Log($"Starting Wave {currentWave}");
        waveEvent.RaiseEvent(currentWave);
        EnemyDamageMultiplier = 1f + (currentWave * 0.13f);
        EnemyHealthMultiplier = 1f + (currentWave * 0.05f);
        currentWave +=1;
        spawner.SpawnEnemies(this, enemiesPerWave); // pass mediator
    }

    private void EndWave()
    {
        Debug.Log($"Wave {currentWave} Complete");

        Invoke(nameof(StartWave), timeBetweenWaves);
    }

    // -------------------
    // MEDIATOR METHODS
    // -------------------

    public void RegisterEnemy(Enemy enemy)
    {
        aliveEnemies.Add(enemy);
    }

    public void OnEnemyDied(Enemy enemy)
    {
        aliveEnemies.Remove(enemy);

        if (aliveEnemies.Count <= 0)
        {
            EndWave();
        }
    }



}
