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

    [SerializeField]
    private TrackingType trackingType;

    // 
    [SerializeField] private int killScore = 20;
    [SerializeField] private int damageScore = 5;
    [SerializeField] private IntEventChannelSO scoreEvent;
    public Team Team => Team.Enemy;
    [SerializeField] GameObject deathParticlePrefab;
    bool isDead = false;




    private float health = 100f;

    private void Start()
    {
        trackingStrategy = CreateStrategy(trackingType);
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
        animator = GetComponent<Animator>();

    }


    //Handle setting the tracking strategy

    public enum TrackingType
    {
        Simple,
        ZigZag,
        Predictive
    }
    private ITrackingStrategy CreateStrategy(TrackingType type)
    {
        switch (type)
        {
            case TrackingType.Simple:
                return new SimpleTracking();

            case TrackingType.ZigZag:
                return new ZigzagTracking();

            case TrackingType.Predictive:
                return new PredictiveTracking();

            default:
                return new SimpleTracking();

        }

    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        if (isDead) return;
        health -= damageInfo.Amount;


        switch (damageInfo.Type)
        {
            case DamageType.Bullet:
                BulletDamage(damageInfo);
                break;

            case DamageType.Asteroid:
                AsteroidDamage(damageInfo);
                break;
        }

        if (health <= 0f)
        {
            Die(damageInfo);
        }
    }


    private void BulletDamage(DamageInfo damageInfo)
    {   //update player score if damages the enemy;
        Debug.Log($"Bullet hit me {health}");
        if (health > 0f)
        {
            scoreEvent.RaiseEvent(damageScore); // damage score
        }

        if (health <= 0f)
        {
            scoreEvent.RaiseEvent(killScore); // kills score 
        }
    }

    private void AsteroidDamage(DamageInfo damageInfo)
    {
        Debug.Log($"Asteroid hit me {health}");
    }


    private void Die(DamageInfo damageInfo)
    {
        isDead = true;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

}

    //Animation 
    public Animator animator;


}
