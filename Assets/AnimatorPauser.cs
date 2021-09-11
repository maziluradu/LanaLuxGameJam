using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPauser : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().SetFloat("PauseTime", Random.Range(0, 3.0f));
    }
}
