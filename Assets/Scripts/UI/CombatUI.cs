using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatUI : MonoBehaviour
{
    public UIStatsModifier ui;
    public AbilityUser user;
    public CombatUnit target;

    [Space(20)]
    public UnityEvent<float> onTargetDamaged = new UnityEvent<float>();
    public UnityEvent onTargetDied = new UnityEvent();

    private void Start()
    {
        if (target)
        {
            target.OnDamage += HandleOnDamage;
            target.OnDeath += HandleOnDeath;
        }
    }

    private void HandleOnDeath(DeathEventData obj)
    {
        onTargetDied.Invoke();
    }

    private void HandleOnDamage(DamageEventData obj)
    {
        onTargetDamaged.Invoke(100 - obj.hpAfter);
    }

    private void Update()
    {
        ui.SetSkillCooldown(0, 100 - 100 * user.fireBallSpell.CooldownPercent);
        ui.SetSkillCooldown(1, 100 - 100 * user.iceBallSpell.CooldownPercent);
        ui.SetSkillCooldown(2, 100 - 100 * user.windBallSpell.CooldownPercent);
    }
}
