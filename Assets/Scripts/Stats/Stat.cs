using UnityEngine;

[System.Serializable]
public class Stat
{
    [Header("Values")]
    public int BaseValue;
    public int ValuePerLevel;
    public int CurrentLevel;

    [Header("Economy")]
    public int BaseCost = 100;
    public int CostPerLevel = 50;

    // --- Performance Logic ---

    // The total value used by the ShipController
    public float CalculatedValue => BaseValue + (ValuePerLevel * CurrentLevel);

    // This specifically fixes the error in ShipController.cs
    public float GetTotalValue()
    {
        return CalculatedValue;
    }

    // --- UI & Shop Logic ---

    // Used to display the "Bonus" part (e.g., the +5 in "7 + 5")
    public float GetBonusValue()
    {
        return ValuePerLevel * CurrentLevel;
    }

    // Calculates how much the NEXT level will cost
    public int GetUpgradeCost()
    {
        return BaseCost + (CostPerLevel * CurrentLevel);
    }
}