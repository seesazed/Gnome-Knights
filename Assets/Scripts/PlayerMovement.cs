using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float crouchSpeed = 1.5f;
    public float slideSpeed = 10f;
    public float crouchHeight = 0.5f;
    public float standingHeight = 2f;

    [Header("Look Settings")]
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;

    private Rigidbody rb;
    private Camera playerCamera;
    private float currentHeight;
    private bool isCrouching = false;
    private bool isSliding = false;
    private bool isSprinting = false;

    private float rotationX = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        currentHeight = standingHeight;

        // Lock the cursor to the screen center and hide it when playing
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        HandleActions();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Determine speed based on states
        float currentSpeed = isCrouching ? crouchSpeed : (isSprinting ? sprintSpeed : walkSpeed);

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        if (isSliding)
        {
            moveDirection *= slideSpeed;
        }
        else
        {
            rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
        }
    }

    private void HandleCameraRotation()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * lookSpeedX);
    }

    private void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Sprint
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        if (Input.GetKeyDown(KeyCode.C)) // Crouch
        {
            ToggleCrouch();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouching) // Slide
        {
            StartSliding();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && isSliding) // Stop sliding
        {
            StopSliding();
        }
    }

    private void ToggleCrouch()
    {
        if (isCrouching)
        {
            StopCrouch();
        }
        else
        {
            StartCrouch();
        }
    }

    private void StartCrouch()
    {
        isCrouching = true;
        currentHeight = crouchHeight;
        playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, currentHeight, playerCamera.transform.localPosition.z);
    }

    private void StopCrouch()
    {
        isCrouching = false;
        currentHeight = standingHeight;
        playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, currentHeight, playerCamera.transform.localPosition.z);
    }

    private void StartSliding()
    {
        if (!isCrouching) return; // Can only slide when crouching

        isSliding = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Cancel out any vertical velocity (jumping or falling)
    }

    private void StopSliding()
    {
        isSliding = false;
    }
}

