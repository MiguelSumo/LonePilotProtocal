using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PowerUpFactory factory;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private int maxActivePickups = 5;
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(20f, 20f);

    private int activePickups = 0;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            TrySpawn();
        }
    }

    private void TrySpawn()
    {
        if (activePickups >= maxActivePickups) return;

        PowerUpType type = (PowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);
        Vector3 spawnPos = new Vector3(
            transform.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            transform.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            0f
        );

        GameObject pickup = factory.CreatePowerUp(type, spawnPos);
        if (pickup == null) return;

        activePickups++;
        pickup.GetComponent<PowerUp>().OnCollected += () => activePickups--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0f));
    }
}