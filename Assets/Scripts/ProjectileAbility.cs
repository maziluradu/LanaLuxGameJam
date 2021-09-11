using System;
using UnityEngine;

[Serializable]
public class ProjectileAbility : Ability
{
    public Projectile projectile;
    public Transform parent;

    public override void Use(AbilityUser user, Vector3 origin, AbilityTargeting targeting)
    {
        if (targeting.isTargeting || quickCast)
        {
            if (quickCast)
                targeting.StartArrowTargeting(user.character.transform, quickCast = true);

            var instance = UnityEngine.Object.Instantiate(projectile, origin, Quaternion.identity, parent);
            instance.direction = targeting.targetDirection;

            targeting.StopTargeting();
        }
        else
        {
            targeting.StartArrowTargeting(user.character.transform, quickCast = true);
        }
    }
}
