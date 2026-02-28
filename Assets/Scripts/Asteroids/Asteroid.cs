using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float _speed;
    private Vector3 _direction;
    private AsteroidPool _pool;

    // We'll use this to calculate the screen bounds
    private float _despawnPadding = 5f;

    void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

    public void Initialize(Sprite rockSprite, float speed, Vector3 direction, AsteroidPool pool)
    {
        _spriteRenderer.sprite = rockSprite;
        _speed = speed;
        _direction = direction;
        _pool = pool;
        gameObject.SetActive(true);
    }

    void Update()
    {
        // 1. Force Z to 0 (The safe middle ground for 2D)
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        // 2. Move
        transform.position += _direction * _speed * Time.deltaTime;

        // 3. Use that padding variable to clean up the code
        float distance = Vector2.Distance(transform.position, Camera.main.transform.position);

        // This uses the variable and keeps your pool cycling!
        if (distance > 25f + _despawnPadding)
        {
            _pool.ReturnToPool(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure your Player object has the Tag "Player" in the Inspector!
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by asteroid!");

            // Return the asteroid to the pool so it 'dies'
            _pool.ReturnToPool(this);

            // Optional: Trigger an explosion effect here
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(5);
        }
    }
}