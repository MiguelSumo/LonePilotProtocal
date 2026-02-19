using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridMovementStrategy : IMovementStrategy
{
    public void Move(Transform shipTransform, float speed)
    {
        // 1. WASD Movement Logic
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized;
        shipTransform.position += moveDirection * speed * Time.deltaTime;

        // 2. Mouse Rotation (Steering) Logic
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Standard distance for 2D
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 lookDirection = targetPos - shipTransform.position;

        if (lookDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            // "angle - 90" assumes the front of your ship sprite points UP
            shipTransform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}