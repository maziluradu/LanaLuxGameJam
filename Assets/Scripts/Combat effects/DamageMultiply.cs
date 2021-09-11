using System;

[Serializable]
public class DamageMultiply : DamageModifier
{
    public float Value { get; set; } = 1f;

    public DamageMultiply(float value)
    {
        Value = value;
    }

    public override float Apply(CombatUnit unit, float damage)
    {
        return damage * Value;
    }
}
