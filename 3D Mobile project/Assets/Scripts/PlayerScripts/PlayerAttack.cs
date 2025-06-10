using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Grabs the Animator from this GameObject
    }

    void Update()
    {
        // For mobile (tap) or mouse click
        if (Input.GetMouseButtonDown(0)) // 0 = left click or tap
        {
            animator.SetTrigger("AttackTrigger"); // Trigger the animation
        }
    }
}
