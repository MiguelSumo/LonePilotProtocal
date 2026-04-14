using System;
using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour
{
    public event Action OnCollected;

    [Header("Power Up Settings")]
    public float duration = 0f;

    public abstract void Collect(GameObject player);
    protected virtual void ApplyEffect(GameObject player) { }
    protected virtual void RemoveEffect(GameObject player) { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
            OnCollected?.Invoke();

            if (duration > 0f)
            {
                GameObject player = other.gameObject;
                player.GetComponent<MonoBehaviour>().StartCoroutine(EffectTimer(player));
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator EffectTimer(GameObject player)
    {
        ApplyEffect(player);
        yield return new WaitForSeconds(duration);
        RemoveEffect(player);
    }
}