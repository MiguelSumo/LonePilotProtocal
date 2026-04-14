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

    // -- Damage Fields --
    [SerializeField] private int killScore = GameScoreValues.EnemyKillScore;
    [SerializeField] private int damageScore = GameScoreValues.EnemyDamageScore;
    [SerializeField] private IntEventChannelSO scoreEvent;
    public Team Team => Team.Enemy;

    //Death Variables
    [SerializeField] GameObject deathParticlePrefab;
    bool isDead = false;

    //Wave manager local reference
    private WaveManager waveManager;



    private float health = GameScoreValues.EnemyHealthMax;

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

   

    // Ensures enemy is always facing the player

    public void FaceTarget()
    {
        if (Target == null) return;

        Vector2 direction =
            (Target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    // RigidBody 
    public Rigidbody2D RB { get; private set; }

    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //Animation 
    public Animator animator;


    // Tracking Strategy Methods

    public ITrackingStrategy GetTrackingStrategy()
    {
        return trackingStrategy;
    }

    public enum TrackingType
    {
        Simple,
        ZigZag,
        //Predictive
    }
    private ITrackingStrategy CreateStrategy(TrackingType type)
    {
        switch (type)
        {
            case TrackingType.Simple:
                return new SimpleTracking();

            case TrackingType.ZigZag:
                return new ZigzagTracking();

           // case TrackingType.Predictive:
            //    return new PredictiveTracking();

            default:
                return new SimpleTracking();

        }

    }

    public void SetTrackingType(TrackingType type)
    {
        trackingType = type;
        trackingStrategy = CreateStrategy(trackingType);
    }

    // Damage and Death Methods


    public void TakeDamage(DamageInfo damageInfo)
    {
        if (isDead) return;
        health -= damageInfo.Amount;


        switch (damageInfo.Type)
        {
            case DamageType.Bullet:
                AudioManager.Instance.PlaySound(AudioManager.Instance.enemyHitSound);
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
            scoreEvent.RaiseEvent(killScore); // kill score 
        }
    }

    private void AsteroidDamage(DamageInfo damageInfo)
    {
        Debug.Log($"Asteroid hit me");
    }


    private void Die(DamageInfo damageInfo)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.enemyDeath);
        isDead = true;

        waveManager.OnEnemyDied(this); // Updates Wave Manager

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            if (damageable.Team == Team.Enemy) return; // enemy shouldn't damage other enemies
            var damageInfo = new DamageInfo(GameScoreValues.EnemyDamageScore, Team.Enemy, DamageType.Enemy);
            damageable.TakeDamage(damageInfo);
        }
    }


    // Allow Wave Manager to Reference Enemy



    public void Initialize(WaveManager manager)
    {
        waveManager = manager;
        manager.RegisterEnemy(this); 
    }

}
