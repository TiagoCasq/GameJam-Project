using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private InputActionAsset InputActions;
    [SerializeField] private Transform Camera;
    [SerializeField] private float velocity;
    private InputAction moveAction;
    private Vector2 moveDirections;
    private float moveX;
    private float moveY;
    private Vector3 moveAmount;
    
    private Rigidbody rb;
    private Camera playerCam;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        playerCam = GetComponentInChildren<Camera>();
        moveAction = InputSystem.actions.FindAction("Move");

    }

    void Move()
    {

        rb.MovePosition(rb.position + moveAmount * (velocity * Time.fixedDeltaTime));
        
        //rb.linearVelocity = (rb.position + moveAmount * (velocity * Time.fixedDeltaTime)).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camRight = Camera.right;

        camRight.y = 0f;

        moveDirections = moveAction.ReadValue<Vector2>().normalized;

        moveX = moveDirections.x;
        moveY = moveDirections.y;

        Vector3 forwardRelative = moveY * transform.forward;
        Vector3 rightRelative = moveX * camRight;

        Vector3 movement = forwardRelative + rightRelative;

        moveAmount = new Vector3(movement.x, 0, movement.z);  

    }

    private void FixedUpdate()
    {
        Move();
    }


}
