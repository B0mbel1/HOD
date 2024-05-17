using UnityEngine;

public class RifleAnimations : MonoBehaviour
{
    Animator animator;
    
    void Start()
    {
        animator= GetComponent<Animator>();
        
    }

    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool shooting = animator.GetBool("shooting");
        bool isSprinting = animator.GetBool("isSprinting");
        bool isCrouching = animator.GetBool("isCrouching");
        bool Backward = animator.GetBool("Backward");
        bool Jump = animator.GetBool("Jump");
        bool ForwardLeft = animator.GetBool("ForwardLeft");
        bool ForwardRight = animator.GetBool("ForwardRight");
        bool Left = animator.GetBool("Left");
        bool Right = animator.GetBool("Right");
        bool WtC = animator.GetBool("WtC");
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool sprintPressed = Input.GetKey("left shift");
        bool crouchPressed = Input.GetKey("left ctrl");
        bool jumpPressed = Input.GetKey("space");
        bool altPressed = Input.GetKey("left alt");
        bool LPMPressed = Input.GetKey("mouse 0");

        if (!isRunning && forwardPressed)
        {
            animator.SetBool("isRunning", true);
        }

        if (isRunning && !forwardPressed)
        {
            animator.SetBool("isRunning", false);
        }

        if (!shooting && LPMPressed)
        {
           animator.SetBool("shooting", true);
        }

        if (shooting && !LPMPressed)
        {
            animator.SetBool("shooting", false);
        }

        if (!isCrouching && crouchPressed)
        {
            animator.SetBool("isCrouching", true);
        }

        if (isCrouching && !crouchPressed)
        {
            animator.SetBool("isCrouching", false);
        }

        if (!isSprinting && (forwardPressed && sprintPressed))
        {
            animator.SetBool("isSprinting", true);
        }

        if (isSprinting && (!forwardPressed && !sprintPressed))
        {
            animator.SetBool("isSprinting", false);
        }

        if (!isSprinting && (leftPressed && sprintPressed))
        {
            animator.SetBool("isSprinting", true);
        }

        if (isSprinting && (!leftPressed && !sprintPressed))
        {
            animator.SetBool("isSprinting", false);
        }

        if (!isSprinting && (rightPressed && sprintPressed))
        {
            animator.SetBool("isSprinting", true);
        }

        if (isSprinting && (!rightPressed && !sprintPressed))
        {
            animator.SetBool("isSprinting", false);
        }

        if (!isCrouching && (forwardPressed && crouchPressed))
        {
            animator.SetBool("isCrouching", true);
        }

        if (isCrouching && (!forwardPressed && !crouchPressed))
        {
            animator.SetBool("isCrouching", false);
        }

        if (isSprinting && (!WtC && altPressed))
        {
            animator.SetBool("WtC", true);
        }

        if (isSprinting && (WtC && !altPressed))
        {
            animator.SetBool("WtC", false);
        }

        if (!Backward && backwardPressed)
        {
            animator.SetBool("Backward", true);
        }

        if (Backward && !backwardPressed)
        {
            animator.SetBool("Backward", false);
        }

        if (!isSprinting && (backwardPressed && sprintPressed))
        {
            animator.SetBool("isSprinting", true);
        }

        if (isSprinting && (!backwardPressed && !sprintPressed))
        {
            animator.SetBool("isSprinting", false);
        }

        if (!ForwardLeft  && leftPressed)
        {
            animator.SetBool("ForwardLeft", true);
        }

        if (ForwardLeft && !leftPressed)
        {
            animator.SetBool("ForwardLeft", false);
        }

        if (!ForwardRight && rightPressed)
        {
            animator.SetBool("ForwardRight", true);
        }

        if (ForwardRight && !rightPressed)
        {
            animator.SetBool("ForwardRight", false);
        }

        if (!Left && leftPressed)
        {
            animator.SetBool("Left", true);
        }

        if (Left && !leftPressed)
        {
            animator.SetBool("Left", false);
        }

        if (!Right && rightPressed)
        {
            animator.SetBool("Right", true);
        }

        if (Right && !rightPressed)
        {
            animator.SetBool("Right", false);
        }

        if (!Jump && jumpPressed)
        {
            animator.SetBool("Jump", true);
        }

        if (Jump && !jumpPressed)
        {
            animator.SetBool("Jump", false);
        }
    }
}
