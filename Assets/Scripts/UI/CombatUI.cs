using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    public UIStatsModifier ui;
    public AbilityUser user;

    private void Update()
    {
        ui.SetSkillCooldown(0, 100 - 100 * user.ability1.CooldownPercent);
        ui.SetSkillCooldown(1, 100 - 100 * user.ability2.CooldownPercent);
        ui.SetSkillCooldown(2, 100 - 100 * user.ability3.CooldownPercent);
        ui.SetSkillCooldown(3, 100 - 100 * user.ability4.CooldownPercent);
    }
}