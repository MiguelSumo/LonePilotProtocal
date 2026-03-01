using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 5f;


    private Camera _cam;

    [SerializeField] private Team ownerTeam;


    private void Awake()
    {
        _cam = Camera.main;
    }

    private void OnEnable()
    {
        // Re-grab camera if it was somehow lost between uses
        if (_cam == null) _cam = Camera.main;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (IsOffScreen())
            BulletPool.Instance.ReturnBullet(gameObject);
    }

    private bool IsOffScreen()
    {
        Vector3 viewportPos = _cam.WorldToViewportPoint(transform.position);
        return viewportPos.x < 0f || viewportPos.x > 1f ||
               viewportPos.y < 0f || viewportPos.y > 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            if (damageable.Team != ownerTeam)
            {
                damageable.TakeDamage(damage);
                //Destroy(gameObject);  //removed because bullet pooling is used below here
            }
        }

        if (other.CompareTag("Enemy"))
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }   

        
    }
}