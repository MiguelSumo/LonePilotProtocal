using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Variables
    public int currentWave = 1;
    
    [SerializeField]private int enemiesPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 15.0f;

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
        currentWave++;
        Debug.Log($"Starting Wave {currentWave}");

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
