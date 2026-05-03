using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTracking : ITrackingStrategy
{
    public float separationRadius = 1f;
    public float separationStrength = 2.5f;

    public void Move(Enemy enemy, Transform target, float moveSpeed)
    {
        if (target == null)
            return;

        Vector2 toTarget = (target.position - enemy.transform.position).normalized;

        // --- separation + avoidance ---
        Vector2 separation = Vector2.zero;
        Vector2 avoidance = Vector2.zero;

        Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.transform.position, separationRadius);

        foreach (var h in hits)
        {
            if (h.gameObject == enemy.gameObject)
                continue;

            Vector2 diff = enemy.transform.position - h.transform.position;
            float dist = Mathf.Max(diff.magnitude, 0.01f);

            // capped repulsion (prevents jitter spikes)
            float strength = Mathf.Clamp(1f / dist, 0f, 1f);

            Vector2 repulse = diff.normalized * strength;
            separation += repulse;

            // --- NEW: side-stepping to avoid getting stuck behind others ---
            Vector2 sideStep = new Vector2(-repulse.y, repulse.x);

            // choose better side toward target
            if (Vector2.Dot(sideStep, toTarget) < Vector2.Dot(-sideStep, toTarget))
                sideStep = -sideStep;

            avoidance += sideStep * 0.5f;
        }

        separation *= separationStrength;

        // combine forces
        Vector2 finalDir = toTarget + separation + avoidance;

        // smoothing (removes jitter)
        Vector2 desiredVelocity = finalDir.normalized * moveSpeed;
        enemy.RB.velocity = Vector2.Lerp(enemy.RB.velocity, desiredVelocity, 0.12f);
    }

}
