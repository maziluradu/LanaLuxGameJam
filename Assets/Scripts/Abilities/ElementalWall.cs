using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalWall : MonoBehaviour
{
    public float lifetime = 10f;
    [SerializeField] protected ElementalType _elementalType;

    public ElementalType ElementalType
    {
        get => _elementalType;
        protected set => _elementalType = value;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
