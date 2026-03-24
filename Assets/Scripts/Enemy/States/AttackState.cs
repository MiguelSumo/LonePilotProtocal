using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{

    //Variables for Attack Animation and lunge
    private float attackCooldown = 1.0f;
    private float lastAttackTime;
    private float lungeDuration = 1.0f;
    private float lungeTimer;

    public void Enter(Enemy enemy)
    {
        Debug.Log("Entering Attack State");

        // Stop movement immediately
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        // Triggger ATTACK ANIMATION
        Debug.Log("Triggering Attack Animation");
        enemy.animator.SetTrigger("Attack");
        lastAttackTime = Time.time;

    }

    public void Update(Enemy enemy)
    {
        if (enemy.Target == null)
            return;

        enemy.FaceTarget();

        // Handle lunge movement window
        if (lungeTimer > 0)
        {
            lungeTimer -= Time.deltaTime;
        }
        else
        {
            enemy.RB.velocity = Vector2.zero;
        }

        float distance = Vector2.Distance(
            enemy.transform.position,
            enemy.Target.position
        );

        // Repeat attack
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack(enemy);
            lastAttackTime = Time.time;
        }

        // Exit condition
        if (distance > enemy.attackRange)
        {
            enemy.ChangeState(new ChaseState());
        }
    }

    private void Attack(Enemy enemy)
    {
        Debug.Log("ATTACK!");

        // Trigger animation
        enemy.animator.ResetTrigger("Attack");
        enemy.animator.SetTrigger("Attack");

        // Lunge
        Vector2 direction = (enemy.Target.position - enemy.transform.position).normalized;

        enemy.RB.velocity = direction * 4f; // STRONG, consistent movement
        lungeTimer = lungeDuration;
    }

    public void Exit(Enemy enemy)
    {
        Debug.Log("Exiting Attack State");
    }
}
