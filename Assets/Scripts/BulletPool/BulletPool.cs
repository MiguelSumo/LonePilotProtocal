using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [SerializeField] private Bullet bulletPrefab; // Changed from GameObject to Bullet
    [SerializeField] private int defaultCapacity = 30;
    [SerializeField] private int maxSize = 100;

    private ObjectPool<Bullet> _pool; // Changed from GameObject to Bullet

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        _pool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(bulletPrefab, transform),
            actionOnGet: bullet => bullet.gameObject.SetActive(true),
            actionOnRelease: bullet => bullet.gameObject.SetActive(false),
            actionOnDestroy: bullet => Destroy(bullet.gameObject),
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    // This now returns 'Bullet', matching what your Factory expects
    public Bullet GetBullet(Vector3 position, Quaternion rotation)
    {
        Bullet bullet = _pool.Get();
        bullet.transform.SetPositionAndRotation(position, rotation);
        return bullet;
    }

    // Update this to accept 'Bullet'
    public void ReturnBullet(Bullet bullet)
    {
        if (!bullet.gameObject.activeSelf) return;
        _pool.Release(bullet);
    }
}