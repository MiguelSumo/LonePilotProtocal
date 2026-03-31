using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 1. Declare the variable without assigning it here
    public Transform player;
    public GameObject enemyPrefab;
    public float spawnDistance = 5f;
    public float spawnInterval;

    private float timer = 0f;

    void Start()
    {
        // 2. Assign the reference here, once the game has started
        if (player == null && GameManager.Instance != null)
        {
            player = GameManager.Instance.Player;
        }
    }

    void Update()
    {
        // 3. Add a "null check" just in case the player isn't found yet
        if (player == null) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Debug.Log("Spawning enemy...");
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 offset = randomDir * spawnDistance;
        Vector3 spawnPos = player.position + (Vector3)offset;
        spawnPos.z = 0f;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}