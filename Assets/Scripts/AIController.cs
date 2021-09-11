using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIController : MonoBehaviour
{
    public GameObject target;
    public float health = 100.0f;

    [HideInInspector]
    public UnityEvent onAIDied = new UnityEvent();
    [HideInInspector]
    public UnityEvent<float> onAIDamaged = new UnityEvent<float>();
    [HideInInspector]
    public UnityEvent<GameObject> onAIDealDamage = new UnityEvent<GameObject>();

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    public void Start()
    {
        this.navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (target != null)
        {
            this.MoveTo(target.transform.position);
        }
    }

    public void Kill(bool fireEvent = true)
    {
        // TODO: Animations

        health = 0.0f;

        if (fireEvent)
        {
            this.onAIDied.Invoke();
        }
    }

    public void Damage(float damage = 20.0f, bool fireEvent = true)
    {
        // TODO: Animations

        health -= damage;

        if (health >= 0.0f)
        {
            this.Kill(fireEvent);
        }
        else
        {
            if (fireEvent)
            {
                this.onAIDamaged.Invoke(damage);
            }
        }
    }

    public void DealDamageTo(GameObject target, bool fireEvent = true)
    {
        // TODO: Animations

        if (fireEvent)
        {
            this.onAIDealDamage.Invoke(target);
        }
    }

    public void MoveTo(Vector3 position)
    {
        this.navMeshAgent.SetDestination(position);
    }
}
