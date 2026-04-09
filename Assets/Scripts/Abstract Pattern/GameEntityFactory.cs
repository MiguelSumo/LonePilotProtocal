using UnityEngine;

public class GameEntityFactory : MonoBehaviour, IGameEntityFactory
{
    [Header("Prefabs")]
    [SerializeField] private Enemy enemyPrefab;

    // [SerializeField] private GameObject bulletPrefab;

    [Header("Asteroid Settings")]
    [SerializeField] private Sprite[] rockSprites;

    [Header("Pools")]
    [SerializeField] private AsteroidPool asteroidPool;

    public void CreateAsteroid(Vector3 position, Vector3 direction)
    {
        Asteroid asteroid = asteroidPool.GetAsteroidFromPool();

        Sprite randomRock = rockSprites[Random.Range(0, rockSprites.Length)];
        float speed = Random.Range(3f, 6f);

        asteroid.transform.position = position;
        asteroid.Initialize(randomRock, speed, direction, asteroidPool);

        //return asteroid;
    }

    public void CreateEnemy(Vector3 position)
    {
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }


    public void CreateBullet(Vector3 position, Quaternion rotation)
    {
        BulletPool.Instance.GetBullet(position, rotation);
    }
}