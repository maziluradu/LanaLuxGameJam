using System;
using UnityEngine;

[Serializable]
public abstract class Ability
{
    public KeyCode button = KeyCode.Alpha1;

    public bool quickCast = false;
    public bool onCooldown = false;

    public abstract void Use(AbilityUser user, Vector3 origin, AbilityTargeting targeting);
}
