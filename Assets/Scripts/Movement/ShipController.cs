using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float defaultMoveSpeed { get; private set; }

    [Header("Shooting")]
    [SerializeField] public Transform firePoint;
    public float fireRate = 0.15f;
    public float defaultFireRate { get; private set; }
    private float nextFireTime;

    public bool IsShielded { get; set; }
    public bool IsInvincible { get; set; }

    private IMovementStrategy _currentStrategy;
    private IPlayerState _currentState; // add this
    [SerializeField] private GameEntityFactory factory;


    void Start()
    {
        defaultMoveSpeed = moveSpeed;
        defaultFireRate = fireRate;

        _currentStrategy = new HybridMovementStrategy();
        SetState(new NormalState());
    }

    void Update()
    {
        _currentStrategy?.Move(transform, moveSpeed);
        _currentState?.UpdateState(this);
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            //BulletPool.Instance.GetBullet(firePoint.position, firePoint.rotation);
            factory.CreateBullet(firePoint.position, firePoint.rotation);
        }
    }

    public void SetStrategy(IMovementStrategy strategy)
    {
        _currentStrategy = strategy;
    }

    public void SetState(IPlayerState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public IPlayerState GetCurrentState()
    {
        return _currentState;
    }
}