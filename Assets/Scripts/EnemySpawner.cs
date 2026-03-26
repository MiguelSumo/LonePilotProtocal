using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player = GameManager.Instance.Player;
    public GameObject enemyPrefab; // drag your enemy prefab here
    public float spawnDistance = 5f;
    public float spawnInterval; // seconds

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime; // add time since last frame

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // reset timer
        }
    }



    
    void SpawnEnemy()
    {
        Debug.Log("Spawning enemy...");
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 offset = randomDir * spawnDistance;
        Vector3 spawnPos = player.position + (Vector3)offset;
        spawnPos.z = 0f; // important for 2D

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
    
}

