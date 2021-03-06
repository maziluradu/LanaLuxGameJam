using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    public float lifetime = 10f;
    public float damage = 10f;

    public bool destroyOnHit = false;

    public GameObject impactPrefab;

    public event Action<CombatUnit> OnPreDmg;
    public event Action<CombatUnit> OnPostDmg;
    public event Action<ElementalWall> OnWallHit;

    private float actualLifetime = 0f;

    protected virtual void Update()
    {
        transform.position += Time.deltaTime * speed * transform.forward.normalized;

        // check lifetime
        actualLifetime += Time.deltaTime;
        if (actualLifetime >= lifetime)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // handle wall hit
        var wall = other.GetComponent<ElementalWall>();
        if (wall != null)
            OnWallHit?.Invoke(wall);

        // handle combat unit hit
        var unit = other.GetComponent<CombatUnit>();
        if (unit != null && unit.IsAlive && !unit.isPlayer)
        {
            // event before damage
            OnPreDmg?.Invoke(unit);

            // apply damage
            unit.Damage(damage);

            // event after damage
            OnPostDmg?.Invoke(unit);

            // destroy on first unit hit
            if (destroyOnHit)
            {
                if (impactPrefab)
                {
                    Destroy(
                        Instantiate(impactPrefab, transform.position, Quaternion.identity),
                        1
                    );
                }

                Destroy(gameObject);
            }
        }
    }
}
