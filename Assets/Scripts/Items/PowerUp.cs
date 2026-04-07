using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public event Action OnCollected;

    public abstract void Collect(GameObject player);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
}
}
