using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PowerUp
{
    public float healAmount = 25f;

    public override void Collect(GameObject player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(healAmount);
        }
    }
}