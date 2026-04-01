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
                var damageInfo = new DamageInfo(damage, ownerTeam, DamageType.Bullet);
                damageable.TakeDamage(damageInfo);
            }
        }


        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Here returned to Pool");
            BulletPool.Instance.ReturnBullet(gameObject);
        }

        //returning the object to the pool
        BulletPool.Instance.ReturnBullet(gameObject);


    }
}