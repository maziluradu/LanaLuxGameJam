using System;

[Serializable]
public class ElementalBallSpell : ProjectileAbility
{
    public ElementalType ElementalType { get; }

    public ElementalBallSpell(ElementalType elementalType)
    {
        ElementalType = elementalType;
    }
}
