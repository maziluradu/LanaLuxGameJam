using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    [Min(0)]
    public float maxHp = 100f;

    [Header("Read only")]
    public float hp = 100f;

    public event Action<DamageEventData> OnDamage;
    public event Action<DeathEventData> OnDeath;

    public readonly List<DamageModifier> modifiers = new List<DamageModifier>();

    private void Start()
    {
        hp = maxHp;
        modifiers.Add(new DamageMultiply(2));
    }
    private void Update()
    {
        var expired = new List<DamageModifier>();

        // update timers for modifiers and save expired ones
        foreach (var modifier in modifiers)
        {
            modifier.UpdateTimers(Time.deltaTime);
            if (modifier.Expired)
                expired.Add(modifier);
        }
        
        // remove expired modifiers
        foreach (var modifier in expired)
            modifiers.Remove(modifier);
    }

    public void Damage(float dmg)
    {
        // prepare event for damage
        var damageEvent = new DamageEventData
        {
            victim = gameObject,
            attacker = null, // to do if needed
            hpBefore = hp,
            dmgRequested = dmg
        };
        // prepare event in case of death
        var deathEvent = new DeathEventData
        {
            victim = gameObject,
            killer = null, // to do if needed
            hpBeforeDeath = hp,
            dmgReceived = dmg
        };

        var actualDmg = ApplyDamageModifiers(dmg);
        hp -= actualDmg;
        
        // trigger damage event
        if (actualDmg > 0)
        {
            damageEvent.hpAfter = hp;
            damageEvent.dmgReceived = actualDmg;

            OnDamage?.Invoke(damageEvent);
        }
        // trigger death event
        if (hp <= 0)
        {
            OnDeath?.Invoke(deathEvent);
        }
    }
    public void Kill()
    {
        // prepare event in case of death
        var deathEvent = new DeathEventData
        {
            victim = gameObject,
            killer = null, // to do if needed
            hpBeforeDeath = hp,
            dmgReceived = hp
        };

        hp = 0;
        OnDeath?.Invoke(deathEvent);
    }

    private float ApplyDamageModifiers(float dmg)
    {
        foreach (var modifier in modifiers)
            dmg = modifier.Apply(this, dmg);
        return dmg;
    }
}
