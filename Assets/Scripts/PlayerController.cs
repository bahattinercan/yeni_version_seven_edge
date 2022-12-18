using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    [SerializeField] private Joystick joystick;
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private Transform groundRaycastT;
    [SerializeField] private LayerMask groundLayer;

    [Header("Speeds")] private float moveSpeed;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float jumpAmount = 5;
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
    public bool isCrouching = false;
    private bool isSliding = false;
    public bool isRun = false;
    private PlayerTrigger _playerTrigger;

    private Animator anim;

    [SerializeField] private GameObject losePanel;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float time;

    [SerializeField] private List<GameObject> cars;
    public GameObject selectedCar;

    public static PlayerController Instance
    {
        get => instance;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        _playerTrigger = GetComponent<PlayerTrigger>();
        // coliider calculations
        baseColliderCenter = _collider.center;
        baseColliderHeight = _collider.height;
        crouchColliderCenter = baseColliderCenter / 2;
        crouchColliderHeight = baseColliderHeight / 2;
        slideColliderCenter = baseColliderCenter / 3;
        slideColliderHeight = baseColliderHeight / 3;
    }

    public bool isPause = false;

    private void FixedUpdate()
    {
        if (!isPause) TimeMethod();
            
        moveDirection = GetMoveDirection();
        Walking();
        GroundRaycast();
    }

    void TimeMethod()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            timeText.text = Mathf.Round(time).ToString();
        }
        else
        {
            LoseMenu();
        }
    }

    private void LoseMenu()
    {
        losePanel.SetActive(true);
    }

    public void Crouch()
    {
        if (!onTheAir && !isSliding && !isCrouching)
        {
            anim.SetTrigger("isCrouch");
            isCrouching = true;
            _collider.center = crouchColliderCenter;
            _collider.height = crouchColliderHeight;
            moveSpeed = crouchSpeed;
        }
        else if (isCrouching)
        {
            anim.SetTrigger("isStand");
            isCrouching = false;
            _collider.center = baseColliderCenter;
            _collider.height = baseColliderHeight;
            moveSpeed = walkSpeed;
        }
    }

    public void Slide()
    {
        if (!onTheAir && !isCrouching && !isSliding && IsRunning())
        {
            //Add SlideAnim
            isSliding = true;
            _collider.center = slideColliderCenter;
            _collider.height = slideColliderHeight;
            moveSpeed = slideSpeed;
        }
    }

    //Anim Trigger End Call this method
    public void StopSlide()
    {
        isSliding = false;
        _collider.center = baseColliderCenter;
        _collider.height = baseColliderHeight;
        moveSpeed = slideSpeed;
    }

    public void Jump()
    {
        if (!isCrouching && !onTheAir && !isSliding)
        {
            anim.SetTrigger("isJump");
        }
    }

    public void JumpUp()
    {
        rb.velocity += jumpAmount * Vector3.up;
    }

    public void GetVechicle()
    {
        selectedCar.SetActive(false);
        isRun = true;
        cars[1].SetActive(true);
        cars[0].SetActive(false);
    }

    public void OutVechicle()
    {
        selectedCar.SetActive(true);
        isRun = false;
        cars[1].SetActive(false);
        cars[0].SetActive(true);
    }


    private void Walking()
    {
        if (!IsRunning())
        {
            _playerTrigger.Energy(Time.deltaTime / 35f);
        }

        if (IsRunning() && !isCrouching && !isSliding)
        {
            moveSpeed = walkSpeed;
        }

        if (isRun)
        {
            moveSpeed = walkSpeed * 1.5f;
        }

        if (IsRunning())
        {
            var targetRotation = Quaternion.LookRotation(moveDirection);
            var targetAngle = targetRotation.eulerAngles.y;
            targetAngle += mainCamera.transform.eulerAngles.y;

            currentAngle =
                Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.eulerAngles = new Vector3(0, currentAngle, 0);

            rb.velocity = new Vector3(transform.forward.x * moveSpeed, rb.velocity.y, transform.forward.z * moveSpeed);
            _playerTrigger.Energy(Time.deltaTime / (15f - (isRun ? 5f : 0)));
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void GroundRaycast()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(groundRaycastT.position, groundRaycastT.TransformDirection(Vector3.down), out hit,
                Mathf.Infinity, groundLayer))
        {
            Debug.DrawRay(groundRaycastT.position, groundRaycastT.TransformDirection(Vector3.down) * hit.distance,
                Color.yellow);
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

    public bool IsCrouchRunning()
    {
        if (GetMoveDirection() == Vector3.zero)
            return false;
        return true;
    }
}