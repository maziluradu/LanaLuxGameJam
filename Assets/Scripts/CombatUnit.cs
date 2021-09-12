using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    [Min(0)]
    [InspectorName("Max HP")]
    [SerializeField] protected float _maxHp = 100f;

    [Header("Read only")]
    [SerializeField] protected float _hp = 100f;

    public event Action<DamageEventData> OnDamage;
    public event Action<DeathEventData> OnDeath;

    public readonly List<DamageModifier> modifiers = new List<DamageModifier>();

    #region Public properties
    public float MaxHp
    {
        get => _maxHp;
        set
        {
            _maxHp = Mathf.Max(0, value);
            _hp = Mathf.Min(_hp, _maxHp);
        }
    }
    public float Hp
    {
        get => _hp;
        set => _hp = Mathf.Clamp(value, 0, _maxHp);
    }
    public bool IsAlive => Hp > 0;
    public bool IsDead => Hp == 0;
    #endregion

    private void Start()
    {
        // use checks from properties to set these
        MaxHp = _maxHp;
        Hp = _maxHp;
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
            hpBefore = Hp,
            dmgRequested = dmg
        };
        // prepare event in case of death
        var deathEvent = new DeathEventData
        {
            victim = gameObject,
            killer = null, // to do if needed
            hpBeforeDeath = Hp,
            dmgReceived = dmg
        };

        var actualDmg = ApplyDamageModifiers(dmg);
        Hp -= actualDmg;
        
        // trigger damage event
        if (actualDmg > 0)
        {
            damageEvent.hpAfter = Hp;
            damageEvent.dmgReceived = actualDmg;

            OnDamage?.Invoke(damageEvent);
        }
        // trigger death event
        if (Hp <= 0)
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
            hpBeforeDeath = Hp,
            dmgReceived = Hp
        };

        Hp = 0;
        OnDeath?.Invoke(deathEvent);
    }

    private float ApplyDamageModifiers(float dmg)
    {
        foreach (var modifier in modifiers)
            dmg = modifier.Apply(this, dmg);
        return dmg;
    }
}
