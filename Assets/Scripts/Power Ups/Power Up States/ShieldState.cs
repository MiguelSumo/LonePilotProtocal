using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldState : IPlayerState
{
    public void EnterState(ShipController ship)
    {
        ship.IsShielded = true;
    }

    public void UpdateState(ShipController ship) { }

    public void ExitState(ShipController ship)
    {
        ship.IsShielded = false;
    }
}