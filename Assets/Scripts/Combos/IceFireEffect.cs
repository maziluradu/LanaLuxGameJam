using UnityEngine;
using UnityEngine.AI;

public class IceFireEffect : MonoBehaviour
{
    public Vector2 mapCenter = Vector2.zero;
    public float lifetime = 10f;
    [Range(0, 1)]
    public float slow = 0.5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
        transform.position = new Vector3(mapCenter.x, mapCenter.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<CombatUnit>();
        if (unit != null && unit.IsAlive && !unit.isPlayer)
            unit.GetComponent<NavMeshAgent>().speed *= slow;
    }
    private void OnTriggerExit(Collider other)
    {
        var unit = other.GetComponent<CombatUnit>();
        if (unit != null && unit.IsAlive && !unit.isPlayer)
            unit.GetComponent<NavMeshAgent>().speed /= slow;
    }
}
