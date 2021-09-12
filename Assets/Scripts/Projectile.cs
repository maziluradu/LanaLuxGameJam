using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public float speed = 1f;
    public float lifetime = 10f;
    public float damage = 10f;

    public bool destroyOnHit = false;

    public event Action<CombatUnit> OnPreDmg;
    public event Action<CombatUnit> OnPostDmg;
    public event Action<ElementalWall> OnWallHit;

    private float actualLifetime = 0f;

    private void Update()
    {
        transform.position += Time.deltaTime * speed * direction.normalized;

        // check lifetime
        actualLifetime += Time.deltaTime;
        if (actualLifetime >= lifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // handle wall hit
        var wall = other.GetComponent<ElementalWall>();
        if (wall != null)
            OnWallHit?.Invoke(wall);

        // handle combat unit hit
        var unit = other.GetComponent<CombatUnit>();
        if (unit != null && unit.IsAlive)
        {
            // event before damage
            OnPreDmg?.Invoke(unit);

            // apply damage
            unit.Damage(damage);

            // event after damage
            OnPostDmg?.Invoke(unit);

            // destroy on first unit hit
            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
