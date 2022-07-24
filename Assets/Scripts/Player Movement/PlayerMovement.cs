using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityAsync;
using System.Threading.Tasks;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    [SerializeField] float stamina = 100.0f;
    float movementMultiplier = 10f;

    [Header("Speed Settings")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float crouchSpeed = 2f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float sprintCost = 0.5f;
    public bool canSprint = true;
    public bool sprinting;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float walkJumpForce = 5f;
    public float crouchJumpForce = 1f;
    public bool canJump = true;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    private float horizontalMovement;
    private float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] float groundDistance = 0.2f;
    public bool isGrounded { get; private set; }

    [Header("Crouching and Sliding")]
    [SerializeField] float slideForce = 40f;
    [SerializeField] float slideCost = 10f;
    public bool canCrouch = true;
    public bool canSlide = true;
    public bool isCrouching { get; private set; }
    public bool runCrouch { get; private set; }

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<float>();
        //return context.ReadValue<float>();
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        verticalMovement = context.ReadValue<float>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if(canSprint && context.started && stamina >= sprintCost)
        {
            sprinting = !sprinting;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if(canCrouch && context.started)
        {
            runCrouch = true;
        }
    }

    public async void OnSlide(InputAction.CallbackContext context)
    {
        if(canSlide && context.started && stamina >= slideCost)
        {
            //OnCrouch();
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            //rb.AddForce(rb.velocity.normalized * slideForce, ForceMode.Impulse);
            stamina -= slideCost;

            if (verticalMovement > 0 || (verticalMovement == 0 && horizontalMovement == 0))
            {
                canCrouch = false;
                canJump = false;
                LerpCrouch(0.5f, new Vector3(transform.localScale.x, 0.5f, transform.localScale.z));
                rb.AddForce(orientation.forward * slideForce, ForceMode.Impulse);
                await Task.Delay(300);
                LerpCrouch(0.5f, new Vector3(transform.localScale.x, 1, transform.localScale.z));
                canCrouch = true;
                canJump = true;
            } else
            {
                rb.AddForce(rb.velocity.normalized * slideForce, ForceMode.Impulse);
            }
            //OnCrouch();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(isGrounded && canJump && context.started)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
        }

        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        runCrouch = false;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ground);

        if (runCrouch)
        {
            Crouch();
        }

        if (isGrounded && !isCrouching)
        {
            canSlide = true;
        } else
        {
            canSlide = false;
        }

        if(stamina < sprintCost || (horizontalMovement == 0 && verticalMovement == 0))
        {
            sprinting = false;
        }

        MyInput();
        ControlDrag();
        ControlSpeed();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void Crouch()
    {
        if (isCrouching)
        {
            LerpCrouch(0.5f, new Vector3(transform.localScale.x, 1, transform.localScale.z)); //Return to normal height
            jumpForce = walkJumpForce;
            canSprint = true;
            canSlide = true;
            isCrouching = false;
            runCrouch = false;
        }
        else
        {
            LerpCrouch(0.5f, new Vector3(transform.localScale.x, 0.5f, transform.localScale.z)); //Crouch to half height
            jumpForce = crouchJumpForce;
            canSprint = false;
            canSlide = false;
            isCrouching = true;
            runCrouch = false;
        }
    }

    void MyInput()
    {
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void ControlSpeed()
    {
        if(sprinting && !isCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            stamina -= sprintCost * Time.deltaTime;
        } else if (isCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
        } else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            //Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, walkFOV, fovChangeTime / Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        } else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        } else if (isGrounded && OnSlope()) {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        } else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    async void LerpCrouch(float lerpDuration, Vector3 newVector)
    {
        float timeElapsed = 0;

        while(timeElapsed < lerpDuration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newVector, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            await Await.NextUpdate();
        }

        transform.localScale = newVector;
    }

    public float GetStamina()
    {
        return stamina;
    }
}
