public interface IDamageble
{
    public int Health { get; }

    public void SetHealth(int value);
    public void ApplyDamage(DamageInfoHolder damage);
}