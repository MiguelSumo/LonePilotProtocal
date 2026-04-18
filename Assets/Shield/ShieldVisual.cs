using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldVisual : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _renderer;

    private static readonly int ShieldActive = Animator.StringToHash("shieldActive");
    private static readonly int ShieldLow    = Animator.StringToHash("shieldLow");
    private static readonly int ShieldHit    = Animator.StringToHash("shieldHit");
    private static readonly int ShieldBroken = Animator.StringToHash("shieldBroken");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void ActivateShield()
    {
        _renderer.enabled = true;
        _animator.SetBool(ShieldActive, true);
    }

    public void DeactivateShield()
    {
        _animator.SetBool(ShieldActive, false);
        // The Collapse anim plays, then an Animation Event calls Hide() at the end
    }

    public void OnShieldHit()
    {
        _animator.SetTrigger(ShieldHit);
    }

    public void SetShieldLow(bool isLow)
    {
        _animator.SetBool(ShieldLow, isLow);
    }

    public void OnShieldBroken()
    {
        _animator.SetBool(ShieldActive, false);
        _animator.SetTrigger(ShieldBroken);
    }

    // Called via Animation Event on the last frame of Collapse
    public void Hide()
    {
        _renderer.enabled = false;
    }
}