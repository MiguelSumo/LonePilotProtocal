using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private Sprite[] rockSprites;
    [SerializeField] private int initialPoolSize = 20;

    private Queue<Asteroid> _pool = new Queue<Asteroid>();

    void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Asteroid obj = Instantiate(asteroidPrefab, transform);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public void GetAsteroid(Vector3 position, Vector3 direction)
    {
        Asteroid asteroid = (_pool.Count > 0) ? _pool.Dequeue() : Instantiate(asteroidPrefab, transform);

        Sprite randomRock = rockSprites[Random.Range(0, rockSprites.Length)];
        float speed = Random.Range(3f, 6f);

        asteroid.transform.position = position;
        // Fix for Error 1: Passing 'this' as the pool argument
        asteroid.Initialize(randomRock, speed, direction, this);
    }

    public void ReturnToPool(Asteroid asteroid)
    {
        asteroid.gameObject.SetActive(false);
        _pool.Enqueue(asteroid);
    }
}