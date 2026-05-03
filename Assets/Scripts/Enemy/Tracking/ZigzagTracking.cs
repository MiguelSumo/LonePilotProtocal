using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZigzagTracking : ITrackingStrategy
{
    [SerializeField] private float zigzagAmplitude = 2f;   // How wide it moves
    [SerializeField] private float zigzagFrequency = 3f;   // How fast it wiggles

    private float offset;

    public ZigzagTracking()
    {
        offset = Random.Range(0f, 100f);
    }


    public void Move(Enemy enemy, Transform target, float moveSpeed)
    {
        

        if (target == null)
            return;

        Vector2 toTarget = (target.position - enemy.transform.position).normalized;

        // Get perpendicular direction (sideways)
        Vector2 perpendicular = new Vector2(-toTarget.y, toTarget.x);

        // Create oscillation
        float zigzag = Mathf.Sin((Time.time + offset) * zigzagFrequency) * zigzagAmplitude;

        // Slight zigzag influence (prevents extreme lane locking)
        Vector2 movement = toTarget + (perpendicular * zigzag * 0.3f);

        movement.Normalize();

        // stronger separation (fix stacking)
        Vector2 separation = Vector2.zero;
        Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.transform.position, 0.6f);

        foreach (var h in hits)
        {
            if (h.gameObject == enemy.gameObject)
                continue;

            Vector2 diff = enemy.transform.position - h.transform.position;
            float dist = Mathf.Max(diff.magnitude, 0.01f);

            separation += diff.normalized * (1f / dist);
        }

        separation *= 2.0f;

        // Combine movement + separation
        Vector2 finalVelocity = movement + separation * 1.5f;

        enemy.RB.velocity = finalVelocity.normalized * moveSpeed;
    }
}
