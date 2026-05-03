using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Variables 
    private Transform player;
    public GameObject enemyPrefab;
    public float spawnDistance = 5f;
    public float spawnInterval;
    
    [SerializeField] private GameEntityFactory factory;



    // Enemy Factory Reference
    // public EnemyFactory factory;

    public void SpawnEnemies(WaveManager waveManager, int enemiesPerWave)
    {
        Debug.Log("SpawnEnemies CALLED: " + enemiesPerWave);

        if (player == null)
        {
            player = GameManager.Instance.Player;
        }

        if (player == null)
        {
            Debug.LogError("PLAYER IS NULL");
            return;
        }

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy(waveManager);
        }
    }


    EnemyType GetRandomEnemyType()
    {
        int count = System.Enum.GetValues(typeof(EnemyType)).Length;
        return (EnemyType)Random.Range(0, count);
    }

    void SpawnEnemy(WaveManager waveManager)
    {
        Debug.Log("Spawning enemy...");
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 offset = randomDir * spawnDistance;
        Vector3 spawnPos = player.position + (Vector3)offset;
        spawnPos.z = 0f;

        EnemyType type = GetRandomEnemyType();
        factory.CreateEnemy(type, spawnPos, waveManager);
        //Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        //factory.CreateEnemy(spawnPos);
    }
}