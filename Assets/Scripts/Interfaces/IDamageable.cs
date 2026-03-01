public interface IDamageable
{
    Team Team { get; }
    void TakeDamage(DamageInfo damageInfo);
}