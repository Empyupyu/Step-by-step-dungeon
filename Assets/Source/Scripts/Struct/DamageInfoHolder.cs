public struct DamageInfoHolder
{
    public string AttakingName { get; private set; }
    public string DamagingName { get; private set; }
    public IDamageble DamagingUnit { get; private set; }
    public IDamageble AttackingUnit { get; private set; }
    public int Damage { get; private set; }

    public DamageInfoHolder(string attackingName, string damagingName, IDamageble attackingUnit, IDamageble damagingUnit, int damage)
    {
        AttakingName = attackingName;
        DamagingName = damagingName;
        AttackingUnit = attackingUnit;
        DamagingUnit = damagingUnit;
        Damage = damage;
    }
}