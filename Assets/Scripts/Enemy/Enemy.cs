using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    // --- Movement & Combat Settings ---
    public float moveSpeed = 3.0f;
    public float attackRange = 1.5f;

    // --- Target ---
    public Transform Target { get; private set; }

    // --- State ---
    private IEnemyState currentState;

    // --- Strategy ---
    private ITrackingStrategy trackingStrategy;

    // 
    [SerializeField] private int killScore = 20;
    [SerializeField] private int damageScore = 5;
    [SerializeField] private IntEventChannelSO scoreEvent;
    public Team Team => Team.Enemy;


    private float health = 100f;

    private void Start()
    {

        // Set default movement strategy
        trackingStrategy = new SimpleTracking();

        // Start in Chase state
        ChangeState(new ChaseState());
    }

    private void Update()
    {
        if (Target == null && GameManager.Instance.Player != null)
        {
            Target = GameManager.Instance.Player;
        }
        currentState?.Update(this);
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public ITrackingStrategy GetTrackingStrategy()
    {
        return trackingStrategy;
    }

    public void FaceTarget()
    {
        if (Target == null) return;

        Vector2 direction =
            (Target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public Rigidbody2D RB { get; private set; }

    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {  
        health -= damage;
        if(health > 0f) {
            scoreEvent.RaiseEvent(damageScore); // score points the player gets if it damages the enemy
        }

        if (health <= 0f)
        {
            scoreEvent.RaiseEvent(killScore); // score points the player gets if it kills the enemy
            Destroy(gameObject);
        }
    }

}
