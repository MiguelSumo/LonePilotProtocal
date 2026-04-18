using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldState : IPlayerState
{
    private ShieldVisual _shieldVisual;
    private float _shieldHP;
    private float _maxShieldHP;
    private const float LowThreshold = 0.3f;
    private bool _isLow = false;

    public ShieldState(ShieldVisual shieldVisual, float shieldHP = 50f)
    {
        _shieldVisual = shieldVisual;
        _shieldHP = shieldHP;
        _maxShieldHP = shieldHP;
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
    }

    public void AbsorbHit(ShipController ship, float damage)
    {
        _shieldHP -= damage;
        _shieldVisual.OnShieldHit();

        // Check if shield just went low
        if (!_isLow && _shieldHP <= _maxShieldHP * LowThreshold)
        {
            _isLow = true;
            _shieldVisual.SetShieldLow(true);
        }

        // Shield is broken
        if (_shieldHP <= 0f)
        {
            _shieldVisual.OnShieldBroken();
            ship.SetState(new NormalState());
        }
    }
}