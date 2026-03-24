using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class ChaseState : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        Debug.Log("Entering Chase State");
    }

    public void Update(Enemy enemy)
    {
        // Safety check
        if (enemy.Target == null)
            return;

        //  Move using strategy
        enemy.GetTrackingStrategy().Move(
            enemy,
            enemy.Target,
            enemy.moveSpeed
        );

        enemy.FaceTarget();

        //  Check distance to switch to attack
        float distance = Vector2.Distance(
            enemy.transform.position,
            enemy.Target.position
        );
        
        if (distance <= enemy.attackRange)
        {
            enemy.ChangeState(new AttackState());
        }
        
    }

    public void Exit(Enemy enemy)
    {
        Debug.Log("Exiting Chase State");
    }
}
