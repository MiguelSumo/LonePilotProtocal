using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTracking : ITrackingStrategy
{
    public void Move(Enemy enemy, Transform target, float moveSpeed)
    {
        if (target == null)
            return;

        Vector2 direction = (enemy.Target.position - enemy.transform.position).normalized;

        enemy.RB.velocity = direction * enemy.moveSpeed;
    }

}
