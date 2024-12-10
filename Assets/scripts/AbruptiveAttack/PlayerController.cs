using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Door Settings")]
    public DoorOpen doorOpenScript;

    [Header("Player Movement Settings")]
    public float movementSpeed = 4f;
    public float mouseSensitivityX = 200f; // X축 감도
    public float mouseSensitivityY = 200f; // Y축 감도
    public float jumpForce = 5f;

    [Header("Camera Settings")]
    public Transform cameraTransform;

    [Header("Footstep Settings")]
    public AudioSource audioSource;
    public AudioClip[] footStepSounds;
    public float footStepInterval = 0.5f;

    [Header("Interaction Settings")]
    public float interactionDistance = 3f;

    [Header("Wall Detection Settings")]
    public float wallDetectionDistance = 1f;
    public LayerMask wallLayer;

    private Rigidbody rb;
    private float xRotation = 0f;
    private float footStepTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
            return;
        }

        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
        }
    }

    void Update()
    {
        HandleMouseLook();
        HandleJump();
        HandleFootSteps();
        HandleInteraction();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        if (!IsWallInFront(direction))
        {
            rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            Debug.Log("Wall detected. Player movement stopped.");
        }
    }

    private bool IsWallInFront(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction.normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, wallDetectionDistance, wallLayer))
        {
            Debug.DrawRay(transform.position, direction.normalized * wallDetectionDistance, Color.red);
            return true;
        }

        Debug.DrawRay(transform.position, direction.normalized * wallDetectionDistance, Color.green);
        return false;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void HandleFootSteps()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isMoving = horizontal != 0 || vertical != 0;

        if (isMoving && IsGrounded())
        {
            footStepTimer += Time.deltaTime;
            if (footStepTimer >= footStepInterval)
            {
                PlayRandomFootstep();
                footStepTimer = 0f;
            }
        }
        else
        {
            footStepTimer = 0f;
        }
    }

    private void PlayRandomFootstep()
    {
        if (footStepSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, footStepSounds.Length);
            AudioClip footStepClip = footStepSounds[randomIndex];
            audioSource.PlayOneShot(footStepClip);
        }
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Key"))
                {
                    if (doorOpenScript != null)
                    {
                        doorOpenScript.CollectKey(hit.collider.gameObject);
                        Debug.Log("Key collected and DoorOpen script updated.");
                    }
                    else
                    {
                        Debug.LogWarning("DoorOpen script is not assigned in the Inspector.");
                    }

                    Destroy(hit.collider.gameObject);
                    Debug.Log("Key object destroyed.");
                }
            }
        }
    }
}
