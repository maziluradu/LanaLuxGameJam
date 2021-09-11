using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(CombatUnit))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    public float destroyDelay = 5f;
    public GameObject target;

    [HideInInspector]
    public UnityEvent<float> onAIDamaged = new UnityEvent<float>();
    [HideInInspector]
    public UnityEvent<GameObject> onAIDealDamage = new UnityEvent<GameObject>();

    private CharacterAnimator animator;
    private CombatUnit combatUnit;
    private NavMeshAgent navMeshAgent;

    public CombatUnit CombatUnit => combatUnit;

    public void OnEnable()
    {
        animator = GetComponent<CharacterAnimator>();
        combatUnit = GetComponent<CombatUnit>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        combatUnit.OnDamage += HandleOnDamage;
        combatUnit.OnDeath += HandleOnDeath;
    }
    public void OnDisable()
    {
        combatUnit.OnDamage -= HandleOnDamage;
        combatUnit.OnDeath -= HandleOnDeath;
    }

    public void Update()
    {
        if (target != null)
        {
            MoveTo(target.transform.position);
            animator.Walk();
        }
        else
        {
            animator.Idle();
        }
    }

    public void MoveTo(Vector3 position)
    {
        this.navMeshAgent.SetDestination(position);
    }

    private void HandleOnDamage(DamageEventData eventData)
    {
        animator.Damage();
    }
    private void HandleOnDeath(DeathEventData eventData)
    {
        animator.Die();
        target = null;
        navMeshAgent.isStopped = true;
        Destroy(gameObject, destroyDelay);
    }
}
