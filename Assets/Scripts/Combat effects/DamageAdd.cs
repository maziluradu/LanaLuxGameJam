using System;

[Serializable]
public class DamageAdd : DamageModifier
{
    public float Value { get; set; } = 0f;

    public DamageAdd(float value)
    {
        Value = value;
    }

    public override float Apply(CombatUnit unit, float damage)
    {
        return damage + Value;
    }
}
