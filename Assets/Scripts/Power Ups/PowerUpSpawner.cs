using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    // Singleton instance so enemies can find it easily
    public static PowerUpSpawner Instance { get; private set; }

    [Header("Dependencies")]
    [SerializeField] private PowerUpFactory factory;

    [Header("Drop Settings")]
    [Range(0f, 100f)]
    [SerializeField] private float dropChance = 25f; // 25% chance
    [SerializeField] private int maxActivePickups = 5;

    private int activePickups = 0;

    private void Awake()
    {
        // Simple singleton setup for the spawner
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void RequestPowerUpDrop(Vector3 spawnPosition)
    {
        // 1. Check if we've reached the maximum allowed on screen
        if (activePickups >= maxActivePickups) return;

        // 2. Roll the 25% chance
        float roll = Random.Range(0f, 100f);
        if (roll <= dropChance)
        {
            SpawnPowerUp(spawnPosition);
        }
    }

    private void SpawnPowerUp(Vector3 position)
    {
        // Pick a random type from your Enum
        PowerUpType type = (PowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);

        GameObject pickup = factory.CreatePowerUp(type, position);

        if (pickup != null)
        {
            activePickups++;

            // Link to the collection event to free up a slot
            PowerUp powerUpComponent = pickup.GetComponent<PowerUp>();
            if (powerUpComponent != null)
            {
                powerUpComponent.OnCollected += () => activePickups--;
            }
        }
    }
}