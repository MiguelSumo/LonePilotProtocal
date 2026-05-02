using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public static LootSpawner Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private GameObject scrapPrefab;

    [Header("Asteroid Drop Settings")]
    [Range(0, 100)]
    [SerializeField] private float dropChance = 10f; // Defaulted to 10%
    [SerializeField] private int minDrop = 1;
    [SerializeField] private int maxDrop = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void DropLoot(Vector3 position)
    {
        // Rolls a number between 0 and 100. 
        // If the number is 10 or less, it spawns.
        if (Random.Range(0f, 100f) <= dropChance)
        {
            int amount = Random.Range(minDrop, maxDrop + 1);

            for (int i = 0; i < amount; i++)
            {
                Instantiate(scrapPrefab, position, Quaternion.identity);
            }
        }
    }
}