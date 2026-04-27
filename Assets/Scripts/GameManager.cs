using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // This is a Property, so we can't put a [Header] on it.
    public Transform Player { get; private set; }

    // These are Fields, so [Header] works perfectly here.
    [Header("Systems to Initialize")]
    [SerializeField] private GameObject bulletPool;
    [SerializeField] private GameObject asteroidManager;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject powerUpFactory;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SystemStartupSequence());
    }

    private IEnumerator SystemStartupSequence()
    {
        yield return new WaitForSeconds(0.2f);

        if (bulletPool != null) bulletPool.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        if (asteroidManager != null) asteroidManager.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        if (powerUpFactory != null) powerUpFactory.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        if (enemySpawner != null) enemySpawner.SetActive(true);

        Debug.Log("Initialization Complete.");
    }

    public void RegisterPlayer(Transform playerTransform)
    {
        Player = playerTransform;
    }
}