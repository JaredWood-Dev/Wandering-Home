using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    
    
    private enum JumpKeyState
    {
        Pressed,
        Held,
        Released,
        Off
    }
    [Header("Jumping")]
    private JumpKeyState jumpState = JumpKeyState.Off;
    public float jumpPower;
    public bool isGrounded;
    public LayerMask groundLayers;
    public float arialMovementModifer = 0.25f;

    [Header("Jump Buffering")] 
    public float jumpBufferTime = 0.2f;
    public bool isJumpBuffered;
    private float _jumpBufferTimer;

    [Header("Coyote Time")] 
    public float coyoteTime = 0.2f;
    private float _coyoteTimer;

    private Rigidbody2D _rb;
    private SpriteRenderer _s;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private float _movement;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _s = GetComponent<SpriteRenderer>();

        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        _movement = _moveAction.ReadValue<Vector2>().x;

        if (_movement > 0)
        {
            _s.flipX = false;
        }
        if (_movement < 0)
        {
            _s.flipX = true;
        }

        if (_jumpAction.WasPressedThisFrame())
        {
            jumpState = JumpKeyState.Pressed;
        }

        if (_jumpAction.WasReleasedThisFrame())
        {
            jumpState = JumpKeyState.Released;
        }
    }

    void FixedUpdate()
    {
        float speedDifference = Mathf.Abs(movementSpeed - Mathf.Abs(_rb.linearVelocityX));
        float neededAcceleration = speedDifference / Time.fixedDeltaTime;
        Vector2 force = Vector2.right * (neededAcceleration * _movement);
        
        if (_movement != 0)
        {
            if (!isGrounded)
            {
                force *= arialMovementModifer;
            }
            _rb.AddForce(force);
        }
        
        //Jumping State Machine
        if (jumpState == JumpKeyState.Pressed)
        {
            //when first pressed
            if (isGrounded || _coyoteTimer < coyoteTime)
            {
                Jump();
            }
            else
            {
                isJumpBuffered = true;
                _jumpBufferTimer = 0;
            }

            jumpState = JumpKeyState.Held;
        }
        if (jumpState == JumpKeyState.Held)
        {
            //when held down
        }
        if (jumpState == JumpKeyState.Released)
        {
            //when released

            jumpState = JumpKeyState.Off;
        }
        
        //Grounded check
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayers);
        Debug.DrawLine(transform.position, (Vector3.down *  1.1f) + transform.position, Color.blue);
        if (ray.collider)
        {
            isGrounded = true;
            if (isJumpBuffered)
            {
                Jump();
            }

            _coyoteTimer = 0;
        }
        else
        {
            isGrounded = false;
        }

        _jumpBufferTimer += Time.deltaTime;
        _coyoteTimer += Time.deltaTime;

        if (_jumpBufferTimer > jumpBufferTime)
        {
            isJumpBuffered = false;
        }
    }

    void Jump()
    {
        _rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isGrounded = false;
    }
}
