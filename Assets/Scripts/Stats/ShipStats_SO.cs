using UnityEngine;

[CreateAssetMenu(fileName = "ShipStats", menuName = "ScriptableObjects/ShipStats")]
public class ShipStats_SO : ScriptableObject
{
    public Stat damage;
    public Stat health;
    public Stat speed;

    // Fixes the 'Accept' errors in UpgradeShopUI and PlayerHealth
    public void Accept(IStatVisitor visitor, StatType type)
    {
        switch (type)
        {
            case StatType.Damage: visitor.Visit(damage); break;
            case StatType.Health: visitor.Visit(health); break;
            case StatType.Speed: visitor.Visit(speed); break;
        }
    }

    // Fixes the 'InitializeNewRun' error in GameManager
    public void InitializeNewRun()
    {
        damage.CurrentLevel = 0;
        health.CurrentLevel = 0;
        speed.CurrentLevel = 0;

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
        Debug.Log("Stats Hard Reset to Level 0");
    }
}