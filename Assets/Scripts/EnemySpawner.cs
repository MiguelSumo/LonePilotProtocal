using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Variables 
    private Transform player;
    public GameObject enemyPrefab;
    public float spawnDistance = 5f;

    // Enemy Factory Reference
    public EnemyFactory factory;

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
        int rand = Random.Range(0, 100);

        if (rand < 40) return EnemyType.BasicBlue;
        if (rand < 70) return EnemyType.BasicRed;
        if (rand < 90) return EnemyType.ZigZagBlue;

        return EnemyType.ZigZagRed;
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
    }
}