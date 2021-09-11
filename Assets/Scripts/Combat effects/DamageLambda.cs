using System;

[Serializable]
public class DamageLambda : DamageModifier
{
    public Func<CombatUnit, float, float> Lambda { get; set; }

    public DamageLambda(Func<CombatUnit, float, float> lambda)
    {
        Lambda = lambda;
    }

    public override float Apply(CombatUnit unit, float damage)
    {
        return Lambda(unit, damage);
    }
}
