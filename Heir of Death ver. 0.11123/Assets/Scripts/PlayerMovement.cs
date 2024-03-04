using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Walking")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    [Header("SlidingVar")]
    public float slideSpeed;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    public float playerHeight;

    [Header("Jumping")]
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("GroundCheck")]
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("SlopeCheck")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    [SerializeField] private List<AudioClip> footStepsSounds = new List<AudioClip>();
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private bool isWalking;
    [SerializeField][Range(0f, 1f)] private float runstepLenghten;
    [SerializeField] private float stepInterval;

    private AudioSource audioSource;
    private float nextStep;
    private float stepCycle;

    private FootStepsSwapper footStepsSwapper;
    private Character character;
    private Rigidbody rigid;
    //private bool isFootstepPlaying = false;
    private bool wasAirborne = false;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        sliding,
        air
    }
    public bool sliding;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        startYScale = transform.localScale.y;

        footStepsSwapper = GetComponent<FootStepsSwapper>();
        stepCycle = 0f;
        nextStep = stepCycle / 2f;
        audioSource = GetComponent<AudioSource>();
        character = GetComponent<Character>();
        rigid = GetComponent<Rigidbody>();

        PlayLandingSound += TestPlayLandingSound;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        MyInput();
        SpeedControl();
        StateHandler();

        ProgressStepCycle(1f);

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (wasAirborne && grounded && rb.velocity.y <= 0)
        {
            PlayLandingSound.Invoke(this,EventArgs.Empty);
        }

        if (!grounded && rb.velocity.y <= 0)
        {
            wasAirborne = true;
        }
        else
        {
            wasAirborne = false;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        if (sliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
            {
                desiredMoveSpeed = slideSpeed;
            }
            else
            {
                desiredMoveSpeed = sprintSpeed;
            }
        }

        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }

        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
            isWalking = true;
            ProgressStepCycle(2f);
        }

        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;
        }

        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMobeSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private IEnumerator SmoothlyLerpMobeSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            time += Time.deltaTime;
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopemoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        rb.useGravity = !OnSlope();
    }

    void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    void Jump()
    {
        PlayJumpSound();
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.5f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopemoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void PlayJumpSound()
    {
        footStepsSwapper.CheckLayer();
        audioSource.clip = jumpSound;
        audioSource.Play();
    }

    public event EventHandler PlayLandingSound;
    private void TestPlayLandingSound(object sender,EventArgs e)
    {
        footStepsSwapper.CheckLayer();
        audioSource.clip = landSound;
        audioSource.Play();
        nextStep = stepCycle * .5f;
    }

    private float lastFootstepTime;

    private void ProgressStepCycle(float speed)
    {
        if (rigid.velocity.sqrMagnitude > 0 && (moveDirection.x != 0 || moveDirection.y != 0))
        {
            stepCycle += (rigid.velocity.sqrMagnitude + (speed * (isWalking ? 1f : runstepLenghten))) * Time.fixedDeltaTime;
        }
        if (!(stepCycle > nextStep))
        {
            return;
        }
        nextStep = stepCycle + stepInterval;
        if (!audioSource.isPlaying)
        {
            PlayFootStepAudio();
        }
    }

    private void PlayFootStepAudio()
    {
        footStepsSwapper.CheckLayer();

        if (!grounded)
        {
            return;
        }
        int a = UnityEngine.Random.Range(1, footStepsSounds.Count);
        audioSource.clip = footStepsSounds[a];
        audioSource.PlayOneShot(audioSource.clip);
        footStepsSounds[a] = footStepsSounds[0];
        footStepsSounds[0] = audioSource.clip;
    }

    public void SwapFootsteps(FootStepCollection collection)
    {
        footStepsSounds.Clear();
        for (int i = 0; i < collection.footstepSound.Count; i++)
        {
            footStepsSounds.Add(collection.footstepSound[i]);
        }
        jumpSound = collection.jumpSound;
        landSound = collection.landSound;
    }
}
