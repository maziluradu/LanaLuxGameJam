using UnityEngine;

public class FireIceEffect : MonoBehaviour
{
    public float lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
