using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalWall : MonoBehaviour
{
    public float lifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
