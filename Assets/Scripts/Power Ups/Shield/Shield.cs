using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
    private ShipController _shipController;
    private ShieldVisual _shieldVisual;

    public override void Collect(GameObject player)
    {
        _shipController = player.GetComponent<ShipController>();
        _shieldVisual = player.GetComponentInChildren<ShieldVisual>();
    }

    protected override void ApplyEffect(GameObject player)
    {
        if (_shipController != null)
            _shipController.SetState(new ShieldState(_shieldVisual));
    }

    protected override void RemoveEffect(GameObject player)
    {
        if (_shipController != null)
            _shipController.SetState(new NormalState());
    }
}