using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFireState : IPlayerState
{
    private float fireRateMultiplier;

    public RapidFireState(float multiplier)
    {
        fireRateMultiplier = multiplier;
    }

    public void EnterState(ShipController ship)
    {
        ship.fireRate = ship.defaultFireRate * fireRateMultiplier;
    }

    public void UpdateState(ShipController ship) { }

    public void ExitState(ShipController ship)
    {
        ship.fireRate = ship.defaultFireRate;
    }
}