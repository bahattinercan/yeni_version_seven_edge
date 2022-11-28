using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    [SerializeField] private Joystick joystick;
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private Transform groundRaycastT;
    [SerializeField] private LayerMask groundLayer;

    [Header("Speeds")]
    private float moveSpeed;    

    [SerializeField] private float walkSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float jumpAmount = 20;
    private Vector3 moveDirection;

    private Rigidbody rb;
    private RaycastHit hit;
    private Camera mainCamera;
    private float currentAngle;
    private float currentAngleVelocity;
    private const float rotationSmoothTime = 0.15f;

    private Vector3 baseColliderCenter;
    private float baseColliderHeight;
    private Vector3 crouchColliderCenter;
    private float crouchColliderHeight;
    private Vector3 slideColliderCenter;
    private float slideColliderHeight;

    private bool onTheAir = false;
    private bool isCrouching = false;
    private bool isSliding = false;

    public static PlayerController Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        
        // coliider calculations
        baseColliderCenter = _collider.center;
        baseColliderHeight = _collider.height;
        crouchColliderCenter = baseColliderCenter / 2;
        crouchColliderHeight = baseColliderHeight / 2;
        slideColliderCenter = baseColliderCenter / 3;
        slideColliderHeight = baseColliderHeight / 3;
    }

    private void FixedUpdate()
    {
        moveDirection = GetMoveDirection();
        Slide();
        Crouch();
        Jump();
        Walking();
        GroundRaycast();
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C) && !onTheAir && !isSliding && !isCrouching)
        {
            isCrouching = true;
            _collider.center = crouchColliderCenter;
            _collider.height = crouchColliderHeight;
            moveSpeed = crouchSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.C) && isCrouching)
        {
            isCrouching = false;
            _collider.center = baseColliderCenter;
            _collider.height = baseColliderHeight;
            moveSpeed = walkSpeed;
        }
    }

    private void Slide()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !onTheAir && !isCrouching && !isSliding && IsRunning())
        {
            isSliding = true;
            _collider.center = slideColliderCenter;
            _collider.height = slideColliderHeight;
            moveSpeed = slideSpeed;
            Invoke("StopSlide", slideTime);
        }
    }
    private void StopSlide()
    {
        isSliding = false;
        _collider.center = baseColliderCenter;
        _collider.height = baseColliderHeight;
        moveSpeed = slideSpeed;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCrouching && !onTheAir && !isSliding)
        {
            rb.velocity += jumpAmount * Vector3.up;
        }
    }

    private void Walking()
    {
        if (IsRunning() && !isCrouching && !isSliding)
        {
            moveSpeed = walkSpeed;
        }

        if (IsRunning())
        {
            var targetRotation = Quaternion.LookRotation(moveDirection);
            var targetAngle = targetRotation.eulerAngles.y;
            targetAngle += mainCamera.transform.eulerAngles.y;

            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.eulerAngles = new Vector3(0, currentAngle, 0);

            rb.velocity = new Vector3(transform.forward.x * moveSpeed, rb.velocity.y, transform.forward.z * moveSpeed);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void GroundRaycast()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(groundRaycastT.position, groundRaycastT.TransformDirection(Vector3.down), out hit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawRay(groundRaycastT.position, groundRaycastT.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            onTheAir = true;
        }
        else
        {
            Debug.DrawRay(groundRaycastT.position, groundRaycastT.TransformDirection(Vector3.down) * 1000, Color.white);
            onTheAir = false;
        }
    }

    public Vector3 GetMoveDirection()
    {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;
        Vector3 direction = new Vector3(x, 0, z);

        return direction;
    }

    public bool IsRunning()
    {
        if (GetMoveDirection() == Vector3.zero)
            return false;
        return true;
    }
}