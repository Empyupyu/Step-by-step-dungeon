using Supyrb;

public class DamageSystem : GameSystem
{
    public override void OnAwake()
    {
        Signals.Get<ApplyDamageSingal>().AddListener(ApplyDamage); 
    }

    private void ApplyDamage(DamageInfoHolder damageInfo)
    {
        var result = damageInfo.DamagingUnit.Health - damageInfo.Damage;
        damageInfo.DamagingUnit.SetHealth(result >= 0 ? result : 0);

        Signals.Get<InfoSignal>().Dispatch($"{damageInfo.AttakingName}({damageInfo.AttackingUnit.Health}) �������" +
            $" {damageInfo.DamagingName}({damageInfo.DamagingUnit.Health}) �������� {damageInfo.Damage} �����!");
    }
}