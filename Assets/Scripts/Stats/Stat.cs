using UnityEngine;
using System;

[Serializable]
public class Stat
{
    public float baseValue;
    public float valuePerLevel;
    public int currentLevel = 0;

    public float GetTotalValue() => baseValue + (valuePerLevel * currentLevel);
    public void Upgrade() => currentLevel++;
}

