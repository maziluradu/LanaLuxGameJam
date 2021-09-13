using UnityEngine;

public class IceIceEffect : Projectile
{
    public float pushStrength = 10f;

    private void OnTriggerStay(Collider other)
    {
        var unit = other.GetComponent<CombatUnit>();
        if (unit != null && unit.IsAlive && !unit.isPlayer)
        {
            unit.transform.position += pushStrength * Time.fixedDeltaTime * transform.forward;
        }
    }
}
