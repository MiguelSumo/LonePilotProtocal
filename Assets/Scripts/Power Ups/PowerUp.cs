using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public event Action OnCollected;

    [Header("Power Up Settings")]
    public float duration = 0f; // 0 means permanent (like health)

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
                StartCoroutine(EffectTimer(other.gameObject));
            else
                Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator EffectTimer(GameObject player)
    {
        ApplyEffect(player);
        Destroy(gameObject);
        yield return new WaitForSeconds(duration);
        RemoveEffect(player);
    }
}