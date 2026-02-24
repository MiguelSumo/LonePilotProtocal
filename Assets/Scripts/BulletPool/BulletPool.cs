using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int defaultCapacity = 30;
    [SerializeField] private int maxSize = 100;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        _pool = new ObjectPool<GameObject>(
            createFunc:      () => Instantiate(bulletPrefab, transform),
            actionOnGet:     bullet => bullet.SetActive(true),
            actionOnRelease: bullet => bullet.SetActive(false),
            actionOnDestroy: bullet => Destroy(bullet),
            collectionCheck: true,   // warns you in editor if you return a bullet twice
            defaultCapacity: defaultCapacity,
            maxSize:         maxSize
        );
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = _pool.Get();
        bullet.transform.SetPositionAndRotation(position, rotation);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        _pool.Release(bullet);
    }
}