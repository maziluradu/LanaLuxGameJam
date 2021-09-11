using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsModifier : MonoBehaviour
{
    public RectMask2D Health;
    public RectMask2D Mana;

    public List<RectMask2D> Skills = new List<RectMask2D>();

    public void Update()
    {
        foreach (RectMask2D skill in Skills)
        {
            float skillCooldownLocalScale = skill.GetComponent<RectTransform>().rect.height;

            if (skill.padding.w > skillCooldownLocalScale)
            {
                skill.padding = new Vector4(skill.padding.x, skill.padding.y, skill.padding.z, skillCooldownLocalScale);
                continue;
            }

            if (skill.padding.w < skillCooldownLocalScale)
            {
                skill.padding = new Vector4(skill.padding.x, skill.padding.y, skill.padding.z, skill.padding.w + Time.deltaTime * 5);
            }
        }
    }

    public void SetHealth(float health)
    {
        if (Health != null)
        {
            Health.padding = new Vector4(
                Health.padding.x,
                Health.padding.y,
                health * (Health.GetComponent<RectTransform>().rect.width / 100),
                Health.padding.w
            );
        }
    }

    public void SetMana(float mana)
    {
        if (Mana != null)
        {
            Mana.padding = new Vector4(
                Mana.padding.x,
                Mana.padding.y,
                mana * (Mana.GetComponent<RectTransform>().rect.width / 100),
                Mana.padding.w
            );
        }
    }

    public void SetSkillCooldown(int skill, float cooldown = 100.0f)
    {
        RectMask2D _skill = Skills[skill];

        if (_skill) {
            _skill.padding = new Vector4(
                _skill.padding.x,
                _skill.padding.y,
                _skill.padding.z,
                _skill.GetComponent<RectTransform>().rect.height - (cooldown * (_skill.GetComponent<RectTransform>().rect.height / 100))
            );
        }
    }

    public bool IsSkillOnCooldown(int skill)
    {
        RectMask2D _skill = Skills[skill];
        float skillCooldownLocalScale = _skill.GetComponent<RectTransform>().rect.height;

        return _skill.padding.w < skillCooldownLocalScale;
    }
}
