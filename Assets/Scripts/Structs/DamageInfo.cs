public struct DamageInfo
{
    public float Amount;
    public Team SourceTeam;
    public DamageType Type;

    public DamageInfo(float amount, Team sourceTeam, DamageType type)
    {
        Amount = amount;
        SourceTeam = sourceTeam;
        Type = type;
    }
}
