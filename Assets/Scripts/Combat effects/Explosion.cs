using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : MonoBehaviour
{
    public float sizeMultiplier = 2f;
    [Min(0)]
    public float duration = 1f;
    public float damage = 100f;
    public float lifetime = 10f;
    public bool createExplosionOnDeath = false;

    private readonly List<CombatUnit> hits = new List<CombatUnit>();
    private Vector3 originalSize = Vector3.one;
    private float timer = 0;

    private void Start()
    {
        originalSize = transform.localScale;
        Destroy(gameObject, lifetime);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        var multiplier = Mathf.Lerp(1, sizeMultiplier, Mathf.Clamp01(timer / duration));
        transform.localScale = multiplier * originalSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<CombatUnit>();
        if (unit != null && unit.IsAlive)
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
                instance.transform.localScale = originalSize;
            }
        }
    }
}
