using System;
using UnityEngine;

[Serializable]
public class DeathEventData
{
    public GameObject victim;
    public GameObject killer;
    public float hpBeforeDeath;
    public float dmgReceived;
}
