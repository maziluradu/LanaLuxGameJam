using System;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    [Min(0)]
    public float maxHp = 100f;

    [Header("Read only")]
    public float hp = 100f;

    public event Action<DeathEventData> OnDeath;

    private void Start()
    {
        hp = maxHp;
    }

    public void Damage(float dmg)
    {
        // prepare event in case of death
        var deathEvent = new DeathEventData
        {
            victim = gameObject,
            killer = null, // to do if needed
            hpBeforeDeath = hp,
            dmgReceived = dmg
        };

        hp -= dmg;

        if (hp <= 0)
        {
            OnDeath?.Invoke(deathEvent);
            Die();
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

        OnDeath?.Invoke(deathEvent);
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
