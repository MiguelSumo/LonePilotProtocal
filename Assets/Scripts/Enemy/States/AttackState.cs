using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        Debug.Log("Entering Attack State");

        // Stop movement immediately
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Update(Enemy enemy)
    {
        if (enemy.Target == null)
            return;

        float distance = Vector2.Distance(
            enemy.transform.position,
            enemy.Target.position
        );

        // If player leaves attack range, go back to chasing
        if (distance > enemy.attackRange)
        {
            enemy.ChangeState(new ChaseState());
        }

        enemy.FaceTarget();
    }

    public void Exit(Enemy enemy)
    {
        Debug.Log("Exiting Attack State");
    }
}
