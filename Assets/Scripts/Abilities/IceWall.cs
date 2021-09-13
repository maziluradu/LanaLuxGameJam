using UnityEngine;

public class IceWall : ElementalWall
{
    public IceFireEffect puddle;
    public Animator animator;
    public string meltTriggerId = "Melt";

    protected override void Start()
    {
        base.Start();
        ElementalType = ElementalType.Ice;
    }
    
    public void Melt()
    {
        animator.SetTrigger(meltTriggerId);
        Instantiate(puddle, transform.position, transform.rotation, transform.parent);

        // disable colliders
        var colliders = GetComponents<Collider>();
        if (colliders != null)
            foreach (var collider in colliders)
                collider.enabled = false;
    }
}
