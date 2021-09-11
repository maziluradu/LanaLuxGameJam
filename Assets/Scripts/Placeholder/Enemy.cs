using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterAnimator))]
public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    private CharacterController controller;
    private CharacterAnimator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<CharacterAnimator>();
    }
    private void Update()
    {
        animator.Walk();
        controller.Move(Time.deltaTime * speed * (controller.transform.forward + Vector3.down));
    }
}
