using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    // inspector variables
    [SerializeField] private string damageId = "Damage";
    [SerializeField] private string dieId = "Die";
    [SerializeField] private string idleId = "Idle";
    [SerializeField] private string walkId = "Walk";

    private Animator animator;
    private bool idle = true;
    private bool walk = false;

    private int framesIdle = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (idle)
            framesIdle++;

        animator.SetBool(idleId, framesIdle > 0 ? idle : false);
        animator.SetBool(walkId, walk);
    }

    public void Damage()
    {
        animator.SetTrigger(damageId);
    }
    public void Die()
    {
        animator.SetBool(dieId, true);
    }
    public void Idle()
    {
        if (walk)
            framesIdle = 0;

        idle = true;
        walk = false;
    }
    public void Walk()
    {
        idle = false;
        walk = true;
    }
}
