using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldState : IPlayerState
{
    private ShieldVisual _shieldVisual;

    public ShieldState(ShieldVisual shieldVisual)
    {
        _shieldVisual = shieldVisual;
    }

    public void EnterState(ShipController ship)
    {
        ship.IsShielded = true;
        _shieldVisual.ActivateShield();
    }

    public void UpdateState(ShipController ship) { }

    public void ExitState(ShipController ship)
    {
        ship.IsShielded = false;
        _shieldVisual.DeactivateShield();
    }
}