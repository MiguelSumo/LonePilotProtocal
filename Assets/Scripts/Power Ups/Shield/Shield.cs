using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
    private ShipController _shipController;
    private ShieldVisual _shieldVisual;

    public override void Collect(GameObject player)
    {
        ShipController shipController = player.GetComponent<ShipController>();
        ShieldVisual shieldVisual = player.GetComponentInChildren<ShieldVisual>();

        if (shipController != null)
            shipController.SetState(new ShieldState(shieldVisual));
    }

    protected override void ApplyEffect(GameObject player) { }
    protected override void RemoveEffect(GameObject player) { }

}