using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float moveSpeed = 7f;
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
    }
}
