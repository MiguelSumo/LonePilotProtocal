using UnityEngine;

public class GameEntityFactory : MonoBehaviour, IGameEntityFactory
{
    [Header("Player Stats Integration")]
    [SerializeField] private ShipStats_SO playerStats;

    [Header("Prefabs")]
    [SerializeField] private Enemy enemyPrefab;

    [Header("Asteroid Settings")]
    [SerializeField] private Sprite[] rockSprites;

    [Header("Pools")]
    [SerializeField] private AsteroidPool asteroidPool;

    private EnemyFactory enemyFactory;

    /// <summary>
    /// Creates a bullet and injects the current upgraded damage stat.
    /// This is high-performance because it avoids GetComponent by pulling the script directly from the pool.
    /// </summary>
    public void CreateBullet(Vector3 position, Quaternion rotation)
    {
        // Pull the Bullet component directly from the Pool
        Bullet bullet = BulletPool.Instance.GetBullet(position, rotation);

        if (bullet != null && playerStats != null)
        {
            // Inject the damage value calculated from the ScriptableObject level
            bullet.SetDamage(playerStats.damage.GetTotalValue());
        }
    }

    public void CreateAsteroid(Vector3 position, Vector3 direction)
    {
        Asteroid asteroid = asteroidPool.GetAsteroidFromPool();

        Sprite randomRock = rockSprites[Random.Range(0, rockSprites.Length)];
        float speed = Random.Range(3f, 6f);

        asteroid.transform.position = position;
        asteroid.Initialize(randomRock, speed, direction, asteroidPool);
    }

    public void CreateEnemy(EnemyType type, Vector3 spawnPos, WaveManager waveManager)
    {
        // Assuming enemyFactory is initialized via Awake or DI
        if (enemyFactory != null)
        {
            enemyFactory.CreateEnemy(type, spawnPos, waveManager);
        }
    }

    // Optional: Call this from the ShipController to ensure player is synced with SO stats
    public void InitializePlayer(ShipController player)
    {
        if (playerStats != null)
        {
            // Logic to sync player with the SO can go here if needed
        }
    }
}