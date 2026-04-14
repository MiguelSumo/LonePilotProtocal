using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RapidFire : PowerUp
{
    public float fireRateMultiplier = 0.3f;
    private ShipController shipController;

    public override void Collect(GameObject player)
    {
        shipController = player.GetComponent<ShipController>();
    }

    protected override void ApplyEffect(GameObject player)
    {
        if (shipController != null)
            shipController.SetState(new RapidFireState(fireRateMultiplier));
    }

    protected override void RemoveEffect(GameObject player)
    {
        if (shipController != null)
            shipController.SetState(new NormalState());
    }
}