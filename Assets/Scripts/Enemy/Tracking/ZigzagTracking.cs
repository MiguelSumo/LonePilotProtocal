using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZigzagTracking : ITrackingStrategy
{
    [SerializeField] private float zigzagAmplitude = 2f;   // How wide it moves
    [SerializeField] private float zigzagFrequency = 3f;   // How fast it wiggles

    public void Move(Enemy enemy, Transform target, float moveSpeed)
    {
        

        if (target == null)
            return;

        Vector2 toTarget = (enemy.Target.position - enemy.transform.position).normalized;

        // Get perpendicular direction (sideways)
        Vector2 perpendicular = new Vector2(-toTarget.y, toTarget.x);

        // Create oscillation
        float zigzag = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;

        Vector2 finalDirection = (toTarget + perpendicular * zigzag).normalized;

        enemy.RB.velocity = finalDirection * enemy.moveSpeed;
    }
}
