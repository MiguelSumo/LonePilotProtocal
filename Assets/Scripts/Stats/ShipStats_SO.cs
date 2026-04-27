using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/Ship Stats")]
public class ShipStats_SO : ScriptableObject
{
    public Stat damage;
    public Stat health;
    public Stat speed;

    public void ResetToDefaults()
    {
        damage.currentLevel = 0;
        health.currentLevel = 0;
        speed.currentLevel = 0;
    }
}