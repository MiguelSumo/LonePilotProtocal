using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private AsteroidPool pool;
    public float spawnInterval = 1.5f;
    public float spawnDistance = 12f; // Added a variable for easy adjustment

    void Start() => InvokeRepeating(nameof(SpawnFromRandomAngle), 0f, spawnInterval);

    void SpawnFromRandomAngle()
    {
        float angle = Random.Range(0, Mathf.PI * 2);

        // 1. Calculate spawn relative to the spawner's current position (The Camera)
        Vector3 spawnPos = new Vector3(
            transform.position.x + Mathf.Cos(angle) * spawnDistance,
            transform.position.y + Mathf.Sin(angle) * spawnDistance,
            0
        );

        // 2. Calculate the "Opposite" side of the screen
        // We take the spawn position relative to the camera and flip it
        Vector3 relativeSpawnPos = spawnPos - transform.position;
        Vector3 relativeTargetPos = -relativeSpawnPos;

        // 3. Add a random offset so they fly across the screen, not just through the dead center
        Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        Vector3 worldTargetPos = transform.position + relativeTargetPos + randomOffset;

        // 4. Final direction calculation
        Vector3 direction = (worldTargetPos - spawnPos).normalized;

        if (pool != null)
        {
            pool.GetAsteroid(spawnPos, direction);
        }
    }

    void Update()
    {
        // Keeps the spawner centered on the view
        transform.position = Camera.main.transform.position;
    }
}