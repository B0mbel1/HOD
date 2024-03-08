using Unity.Netcode;
using UnityEngine;

public class RifleAnimationsOnline : NetworkBehaviour
{
    Animator animator;
    //public Camera mainCamera; // Pole, w którym bêdziemy mogli przypisaæ kamerê
    public Transform cameraTransform;
    public float convertedRotation;
    public int roundedRotation;
    float xRotation;

    void Start()
    {
        animator= GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;


        // Get the rotation around the x-axis of the camera
        float rotationX = cameraTransform.rotation.eulerAngles.x;
       // float mouseY = Input.GetAxisRaw("Mouse Y");
        //xRotation -= mouseY;
        // Convert the rotation to float values with range -90 to 90
        convertedRotation = NormalizeRotation(rotationX, -90f, 90f);
       // convertedRotation = NormalizeRotation(xRotation, -30f, 90f);
        roundedRotation = Mathf.RoundToInt(convertedRotation);

        animator.SetFloat("camera", roundedRotation);


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
    float NormalizeRotation(float rotation, float min, float max)
    {
        rotation = rotation % 360f;
        if (rotation < 0)
            rotation += 360f;

        if (rotation > 180f)
            rotation -= 360f;

        return Mathf.Clamp(rotation, min, max);
    }
}
