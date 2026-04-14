using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PowerUp
{
    public float speedMultiplier = 2f;
    private ShipController shipController;

    public override void Collect(GameObject player)
    {
        shipController = player.GetComponent<ShipController>();
    }

    protected override void ApplyEffect(GameObject player)
    {
        if (shipController != null)
            shipController.moveSpeed *= speedMultiplier;
    }

    protected override void RemoveEffect(GameObject player)
    {
        if (shipController != null)
            shipController.moveSpeed /= speedMultiplier;
    }
}