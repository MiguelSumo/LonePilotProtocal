public interface IDamageable
{
    Team Team { get; }
    void TakeDamage(float damage);
}