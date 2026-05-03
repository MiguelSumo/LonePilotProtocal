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

    private void Awake()
    {
        enemyFactory = GetComponent<EnemyFactory>();
    }

    /// <summary>
    /// Creates a bullet and injects the current upgraded damage stat.
    /// Uses .CalculatedValue from the visitor-ready Stat class.
    /// </summary>
    public void CreateBullet(Vector3 position, Quaternion rotation)
    {
        // Pull the Bullet component directly from the Pool
        Bullet bullet = BulletPool.Instance.GetBullet(position, rotation);

        if (bullet != null && playerStats != null)
        {
            // FIX: Changed .GetTotalValue() to .CalculatedValue to match your Stat class
            bullet.SetDamage(playerStats.damage.CalculatedValue);
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
        if (enemyFactory != null)
        {
            enemyFactory.CreateEnemy(type, spawnPos, waveManager);
        }
    }

    public void InitializePlayer(ShipController player)
    {
        if (playerStats != null)
        {
            // Sync player with the SO if needed via factory initialization
            player.RefreshStatsFromSO();
        }
    }
}