using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public float speed = 1f;
    public float lifetime = 10f;
    public float damage = 10f;

    public bool destroyOnHit = false;

    [Header("Explosion effect")]
    public bool useExplostion = false;
    public Explosion explosion;
    public Transform explosionParent;

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
        var unit = other.GetComponent<CombatUnit>();
        if (unit != null && unit.IsAlive)
        {
            unit.Damage(damage);

            if (useExplostion && explosion != null)
                Instantiate(explosion, transform.position, transform.rotation, explosionParent);

            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
