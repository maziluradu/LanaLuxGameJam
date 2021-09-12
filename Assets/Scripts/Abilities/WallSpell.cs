using System;
using UnityEngine;

[Serializable]
public class WallSpell : Ability
{
    public ElementalWall fireWall;
    public ElementalWall iceWall;
    public ElementalWall windWall;
    public Transform instancesParent;

    public override void Press(AbilityUser user, AbilityTargeting targeting)
    {
        ElementalWall wall;
        switch (user.LastElementalType)
        {
            case ElementalType.Fire:
                wall = fireWall;
                break;
            case ElementalType.Ice:
                wall = iceWall;
                break;
            case ElementalType.Wind:
                wall = windWall;
                break;
            // to do: wall with no element
            default:
                return;
        }

        // get target
        targeting.StartPointTargeting(quickCast = true);

        // instantiate wall
        UnityEngine.Object.Instantiate(wall, targeting.targetPosition, Quaternion.identity, instancesParent);

        PutOnCooldown();
    }
    public override void Hold(AbilityUser user, AbilityTargeting targeting)
    {
        throw new System.NotImplementedException();
    }
    public override void Release(AbilityUser user, AbilityTargeting targeting)
    {
        throw new System.NotImplementedException();
    }
}
