using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{
    [Min(0)]
    public float maxHp = 100f;

    [Header("Read only")]
    public float hp = 100f;

    private void Start()
    {
        hp = maxHp;
    }

    public void Damage(float dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
