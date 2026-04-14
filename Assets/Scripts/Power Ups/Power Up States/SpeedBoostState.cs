using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostState : IPlayerState
{
    private float speedMultiplier;

    public SpeedBoostState(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void EnterState(ShipController ship)
    {
        ship.moveSpeed = ship.defaultMoveSpeed * speedMultiplier;
    }

    public void UpdateState(ShipController ship) { }

    public void ExitState(ShipController ship)
    {
        ship.moveSpeed = ship.defaultMoveSpeed;
    }
}