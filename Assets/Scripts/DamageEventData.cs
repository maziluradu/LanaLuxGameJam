using System;
using UnityEngine;

[Serializable]
public class DamageEventData
{
    public GameObject victim;
    public GameObject attacker;
    public float hpBefore;
    public float hpAfter;
    public float dmgRequested;
    public float dmgReceived;
}
