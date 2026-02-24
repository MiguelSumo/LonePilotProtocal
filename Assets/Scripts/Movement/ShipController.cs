using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float moveSpeed = 7f;

    [Header("Shooting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.15f;
    private float nextFireTime;

    private IMovementStrategy _currentStrategy;

    void Start()
    {
        // Initialize with the WASD + Mouse Aiming strategy
        _currentStrategy = new HybridMovementStrategy();
    }

    void Update()
    {
        // Execute the strategy
        _currentStrategy?.Move(transform, moveSpeed);
        HandleShooting();
    }
    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            BulletPool.Instance.GetBullet(firePoint.position, firePoint.rotation);
        }
    }

    public void SetStrategy(IMovementStrategy strategy)
    {
        _currentStrategy = strategy;
    }
}
