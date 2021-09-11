using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(CombatUnit))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    public GameObject target;

    [HideInInspector]
    public UnityEvent<float> onAIDamaged = new UnityEvent<float>();
    [HideInInspector]
    public UnityEvent<GameObject> onAIDealDamage = new UnityEvent<GameObject>();

    private CombatUnit combatUnit;
    private NavMeshAgent navMeshAgent;

    public CombatUnit CombatUnit => combatUnit;

    public void OnEnable()
    {
        combatUnit = GetComponent<CombatUnit>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        combatUnit.OnDeath += HandleOnDeath;
    }
    public void OnDisable()
    {
        combatUnit.OnDeath -= HandleOnDeath;
    }

    public void Update()
    {
        if (target != null)
        {
            this.MoveTo(target.transform.position);
        }
    }

    public void MoveTo(Vector3 position)
    {
        this.navMeshAgent.SetDestination(position);
    }

    private void HandleOnDeath(DeathEventData eventData)
    {
        
    }
}
