using UnityEngine;

// This enum connects the Shop buttons to the right Stat
public enum StatType { Damage, Health, Speed }

public interface IStatVisitor
{
    void Visit(Stat stat);
}

public class UpgradeVisitor : IStatVisitor
{
    public void Visit(Stat stat)
    {
        stat.CurrentLevel++;
        Debug.Log("Stat upgraded! New Level: " + stat.CurrentLevel);
    }
}

public class ResetVisitor : IStatVisitor
{
    public void Visit(Stat stat)
    {
        stat.CurrentLevel = 0;
    }
}