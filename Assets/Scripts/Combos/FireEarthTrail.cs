using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FireEarthTrail : MonoBehaviour
{
    public float dmgPerSecond = 10f;
    public float lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerStay(Collider other)
    {
        var unit = other.GetComponent<CombatUnit>();
        if (unit != null)
            unit.Damage(dmgPerSecond * Time.fixedDeltaTime);
    }
}
