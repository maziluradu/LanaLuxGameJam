using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : MonoBehaviour
{
    public float sizeMultiplier = 2f;
    [Min(0)]
    public float duration = 1f;
    public float damage = 100f;
    public float colliderLifetime = 10f;
    public float lifetime = 10f;
    public bool createExplosionOnDeath = false;
    public bool createExplosionOnMarked = false;
    
    private readonly List<CombatUnit> hits = new List<CombatUnit>();
    private float originalRadius = 1.0f;
    private float timer = 0;
    private new SphereCollider collider;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
        originalRadius = collider.radius;
        Destroy(gameObject, lifetime);
        Destroy(this, colliderLifetime);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        var multiplier = Mathf.Lerp(1, sizeMultiplier, Mathf.Clamp01(timer / duration));
        collider.radius = multiplier * originalRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<CombatUnit>();
        if (unit != null && unit.IsAlive && !unit.isPlayer)
        {
            // unit already hit, we can exit
            if (hits.Contains(unit))
                return;
            // add to list so we don't double hit
            hits.Add(unit);

            // apply damage
            unit.Damage(damage);

            // if unit was kill, create another explosion
            if (createExplosionOnDeath && unit.IsDead)
            {
                var instance = Instantiate(this, unit.transform.position, unit.transform.rotation, gameObject.transform.parent);
                instance.GetComponent<SphereCollider>().radius = originalRadius;
            }

            // create explosion on marked units
            var mark = unit.GetComponent<FireMark>();
            if (createExplosionOnMarked && mark != null)
            {
                var instance = Instantiate(this, unit.transform.position, unit.transform.rotation, gameObject.transform.parent);
                instance.GetComponent<SphereCollider>().radius = originalRadius;
                Destroy(mark);
            }
        }
    }
}
