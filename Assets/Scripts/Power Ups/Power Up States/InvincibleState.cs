using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityState : IPlayerState
{
    public void EnterState(ShipController ship)
    {
        ship.IsInvincible = true;
    }

    public void UpdateState(ShipController ship) { }

    public void ExitState(ShipController ship)
    {
        ship.IsInvincible = false;
    }
}