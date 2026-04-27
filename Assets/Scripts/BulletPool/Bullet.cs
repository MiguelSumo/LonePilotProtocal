using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 5f;

    private Camera _cam;
    [SerializeField] private Team ownerTeam;

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void OnEnable()
    {
        if (_cam == null) _cam = Camera.main;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (IsOffScreen())
            BulletPool.Instance.ReturnBullet(this);
    }

    private bool IsOffScreen()
    {
        Vector3 viewportPos = _cam.WorldToViewportPoint(transform.position);
        return viewportPos.x < 0f || viewportPos.x > 1f ||
               viewportPos.y < 0f || viewportPos.y > 1f;
    }

    // Inside Bullet.cs
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            if (damageable.Team != ownerTeam)
            {
                // USE THE VARIABLE 'damage', NOT '5f'
                var damageInfo = new DamageInfo(this.damage, ownerTeam, DamageType.Bullet);
                damageable.TakeDamage(damageInfo);
            }
        }
        BulletPool.Instance.ReturnBullet(this);
    }
}