using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictiveTracking : ITrackingStrategy
{
    private float predictionTime = 1.0f;

    public void Move(Enemy enemy, Transform target, float moveSpeed)
    {
        if (enemy.Target == null)
            return;

        Rigidbody2D targetRB = enemy.Target.GetComponent<Rigidbody2D>();

        Vector2 predictedPosition = enemy.Target.position;

        if (targetRB != null)
        {
            predictedPosition += targetRB.velocity * predictionTime;
        }

        Vector2 direction =
            (predictedPosition - (Vector2)enemy.transform.position).normalized;

        enemy.RB.velocity = direction * enemy.moveSpeed;
    }
}
