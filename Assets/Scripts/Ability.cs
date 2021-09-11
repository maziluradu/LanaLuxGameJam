using System;
using UnityEngine;

[Serializable]
public abstract class Ability
{
    public KeyCode button = KeyCode.Alpha1;

    public bool quickCast = false;
    public bool onCooldown = false;
    [Min(0)]
    public float cooldown = 1f;

    public float Timer { get; protected set; }
    public float CooldownPercent => (cooldown == 0) ? 1 : Mathf.Clamp(Timer / cooldown, 0, 1);

    public Ability()
    {
        Timer = cooldown;
    }

    public abstract void Use(AbilityUser user, Vector3 origin, AbilityTargeting targeting);

    public void UpdateCooldown(float deltaTime)
    {
        Timer += deltaTime;
        onCooldown = Timer < cooldown;
    }
}
