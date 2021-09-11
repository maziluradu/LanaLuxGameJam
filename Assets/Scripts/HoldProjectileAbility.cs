using System;
using UnityEngine;

[Serializable]
public class HoldProjectileAbility : ProjectileAbility
{
    [Tooltip("How long to hold for max effect")]
    public float holdDuration = 2f;
    [Header("Max hold multiplier")]
    public float damageMultiplier = 3f;
    public float sizeMultiplier = 3f;
    public float speedMultiplier = 1f;

    private float holdTimer = 0f;

    public override void Press(AbilityUser user, AbilityTargeting targeting)
    {
        if (targeting.isTargeting)
        {
            Trigger(user, targeting);

            targeting.StopTargeting();

            // put on cooldown
            Timer = 0;
            onCooldown = true;
        }
        else
        {
            targeting.StartArrowTargeting(user.character.transform, quickCast = false);
            holdTimer = 0f;
        }
    }
    public override void Hold(AbilityUser user, AbilityTargeting targeting)
    { }
    public override void Release(AbilityUser user, AbilityTargeting targeting)
    { }

    public override void UpdateTimers(float deltaTime)
    {
        base.UpdateTimers(deltaTime);
        holdTimer += deltaTime;
    }

    protected override void Trigger(AbilityUser user, AbilityTargeting targeting)
    {
        var instance = UnityEngine.Object.Instantiate(projectile, user.abilitiesSource.position, Quaternion.identity, parent);
        instance.direction = targeting.targetDirection;

        var holdPercentage = Mathf.Clamp01(holdTimer / holdDuration);
        instance.damage *= Mathf.Lerp(1, damageMultiplier, holdPercentage);
        instance.speed *= Mathf.Lerp(1, speedMultiplier, holdPercentage);
        instance.transform.localScale *= Mathf.Lerp(1, sizeMultiplier, holdPercentage);
    }
    
}
