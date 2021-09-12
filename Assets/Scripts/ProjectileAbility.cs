using System;
using UnityEngine;

[Serializable]
public class ProjectileAbility : Ability
{
    public Projectile projectile;
    public Transform parent;

    public override void Press(AbilityUser user, AbilityTargeting targeting)
    {
        if (targeting.isTargeting || quickCast)
        {
            if (quickCast)
                targeting.StartArrowTargeting(user.character.transform, quickCast = true);

            Trigger(user, targeting);

            targeting.StopTargeting();

            PutOnCooldown();
        }
        else
        {
            targeting.StartArrowTargeting(user.character.transform, quickCast = false);
        }
    }
    public override void Hold(AbilityUser user, AbilityTargeting targeting)
    {
        // automatic fire
        Press(user, targeting);
    }
    public override void Release(AbilityUser user, AbilityTargeting targeting)
    {
        // to do
    }

    protected virtual void Trigger(AbilityUser user, AbilityTargeting targeting)
    {
        var rotation = Quaternion.LookRotation(targeting.targetDirection);
        UnityEngine.Object.Instantiate(projectile, user.abilitiesSource.position, rotation, parent);
    }
}
