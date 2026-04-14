using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : IPlayerState
{
    public void EnterState(ShipController ship)
    {
        ship.moveSpeed = ship.defaultMoveSpeed;
        ship.fireRate = ship.defaultFireRate;
        ship.IsShielded = false;
        ship.IsInvincible = false;
    }

    public void UpdateState(ShipController ship) { }

    public void ExitState(ShipController ship) { }
}
