using UnityEngine;

public class CurrencyItem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int currencyValue = 10;
    [SerializeField] private float lifetime = 7f;
    [SerializeField] private float launchForce = 2f;

    [Header("Events")]
    [SerializeField] private IntEventChannelSO currencyEventChannel;

    private void Start()
    {
        // Give it a little physical kick so it doesn't just sit still
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            rb.AddForce(randomDir * launchForce, ForceMode2D.Impulse);
        }

        // Cleanup: destroy itself if the player doesn't pick it up
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure your Player object has the "Player" Tag!
        if (other.CompareTag("Player"))
        {
            if (currencyEventChannel != null)
            {
                currencyEventChannel.RaiseEvent(currencyValue);
            }
            else
            {
                Debug.LogWarning($"Currency Event Channel is missing on {gameObject.name}!");
            }

            Destroy(gameObject);
        }
    }
}