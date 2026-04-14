using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void EnterState(ShipController ship);
    void UpdateState(ShipController ship);
    void ExitState(ShipController ship);
}