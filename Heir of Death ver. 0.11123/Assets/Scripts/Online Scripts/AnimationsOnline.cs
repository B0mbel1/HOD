using Unity.Netcode;
using UnityEngine;

public class AnimiationsOnline : NetworkBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("jumpIdle", true);
        }
        else
        {
            animator.SetBool("jumpIdle", false);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", true);
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }

        if (Input.GetKey(KeyCode.Slash))
        {
            animator.SetBool("isEE", true);
        }
        else
        {
            animator.SetBool("isEE", false);
        }
    }
}
