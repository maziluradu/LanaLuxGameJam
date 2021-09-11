using System;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    [Min(0)]
    public float maxHp = 100f;

    [Header("Read only")]
    public float hp = 100f;

    public event Action<DamageEventData> OnDamage;
    public event Action<DeathEventData> OnDeath;

    private void Start()
    {
        hp = maxHp;
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

        // to do: Armor effects here
        var actualDmg = dmg;

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
}
