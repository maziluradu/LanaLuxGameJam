using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(CombatUnit))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    public float attackDistance = 1.5f;
    public float destroyDelay = 5f;
    public float damage = 5.0f;
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
            if (Vector3.Distance(transform.position, target.transform.position) > attackDistance)
            {
                navMeshAgent.isStopped = false;
                MoveTo(target.transform.position);
                animator.Walk();
            } else
            {
                navMeshAgent.isStopped = true;
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                animator.Attack();
            }
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

    public void DealDamageToTarget()
    {
        var combatUnitController = target.GetComponent<CombatUnit>();
        if (combatUnitController && combatUnitController.Hp > 0)
        {
            combatUnitController.Damage(damage);
        }
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
