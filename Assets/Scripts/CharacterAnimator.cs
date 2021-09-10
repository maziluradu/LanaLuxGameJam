using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    public string idleId = "Idle";
    public string walkId = "Walk";

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
