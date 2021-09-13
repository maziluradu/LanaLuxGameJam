using UnityEngine;

public class WindWindEffect : MonoBehaviour
{
    public float speed = 10f;
    public float distance = 3f;
    public int count = 3;

    protected bool isTriggered = false;
    protected Vector3 direction = Vector3.zero;

    private void Update()
    {
        if (isTriggered)
            transform.position += Time.deltaTime * speed * direction;
    }

    public void Trigger(Transform projectile)
    {
        isTriggered = true;
        direction = projectile.forward;
    }
}
