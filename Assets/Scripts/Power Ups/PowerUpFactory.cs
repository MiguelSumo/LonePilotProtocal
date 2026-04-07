using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFactory : MonoBehaviour
{
    [Header("Power Up Prefabs")]
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject speedPrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject rapidFirePrefab;
    [SerializeField] private GameObject invincibilityPrefab;

    public GameObject CreatePowerUp(PowerUpType type, Vector3 position)
    {
        GameObject prefab = type switch
        {
            PowerUpType.Health => healthPrefab,
            PowerUpType.Speed => speedPrefab,
            PowerUpType.Shield => shieldPrefab,
            PowerUpType.RapidFire => rapidFirePrefab,
            PowerUpType.Invincibility => invincibilityPrefab,
            _ => null
        };

        if (prefab == null)
        {
            Debug.LogWarning("No prefab assigned for type: " + type);
            return null;
        }

        return Instantiate(prefab, position, Quaternion.identity);
    }
}