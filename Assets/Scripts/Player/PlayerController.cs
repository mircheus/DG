using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{ 
    [Header("Layer Masks")] 
    [SerializeField] private LayerMask _groundLayer;
    
    [Header("Components")] 
    private Rigidbody2D _rigidbody;

    [Header("Movement Variables")] 
    [SerializeField] private float _movementAcceleration = 50f;
    [SerializeField] private float _maxMoveSpeed = 10f;
    [SerializeField] private float _linearDrag = 7f;

    [Header("Ground Checker")] 
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _circleRadius = 0.5f;

    [Header("Jump Variables")] 
    [SerializeField] private float _jumpForce = 12f;   
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 14f;
    [SerializeField] private float _lowJumpFallMultiplier = 5f;

    [Header("Wall Slide")]
    [SerializeField] private BoxCollider2D _wallCheckerCollider;
    [SerializeField] private float _wallSlidingSpeed;
        
    [Header("Wall Jump")] 
    [SerializeField] private float _xWallForce;
    [SerializeField] private float _yWallForce;
    [SerializeField] private float _wallJumpDuration;
    private WallChecker _wallChecker;
    
    [SerializeField] private ContactFilter2D _groundContactFilter;

    [Header("Jump Shoot Statter")] 
    [SerializeField] private float _jumpStatterForce;
    [SerializeField] private PlayerShooting _player;

    [Header("Dash")] 
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashDuration;
    [SerializeField] private bool _isDashed = false;
    [SerializeField] private float _yStabilizer = 0.5f;

    [Header("Debug variables")]
    [SerializeField] private bool _wallSliding;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isAbleToMove = true;
    [SerializeField] private float _horizontalDirectionRaw;
    
    private bool ChangingDirection => (_rigidbody.velocity.x > 0f && _horizontalDirectionRaw < 0f) ||
                                      (_rigidbody.velocity.x < 0f && _horizontalDirectionRaw > 0f);
    private bool _isAbleToJump => Input.GetKeyDown(KeyCode.Space) && IsGrounded();
    
    public bool IsPlayerGrounded => _isGrounded;
    public float HorizontalInputDirection => _horizontalDirectionRaw;
    
        
    public event UnityAction WallJumped;
    public event UnityAction WallSliding;
    public event UnityAction Jumped;
    public event UnityAction Dashed;
    
    private void OnEnable()
    {
        _player.Shooted += JumpShootStatter;
    }

    private void OnDisable()
    {
        _player.Shooted -= JumpShootStatter;
    }
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerShooting>();
        _wallChecker = GetComponentInChildren<WallChecker>();
    }

    private void Update()
    {
        _horizontalDirectionRaw = GetInputRaw().x;
        _isGrounded = IsGrounded();

        if (_isAbleToJump)
        {
            Jump();
        }

        if (IsGrounded())
        {
            _isDashed = false;
            Crouch();
        }
        
        if (IsTouchingWall() && !IsGrounded())
        {
            _wallSliding = true;
            WallSliding?.Invoke();
        }
        else
        {
            _wallSliding = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded() == false && _isDashed == false)
        {
            Dash();
        }
        
        if (_wallSliding && Input.GetKeyDown(KeyCode.Space))
        {
            WallJump();
        }
    }
    
    private void FixedUpdate()
    {
        Move();
        
        if (IsGrounded())
        {
            ApplyGroundLinearDrag();
        }
        else if (_isDashed)
        {
            ApplyDashMultiplier();
        }
        else
        {
            ApplyAirLinearDrag();
            ApplyFallMultiplier();
        }

        WallSlide();
    }
    
    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        Jumped?.Invoke();
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _isAbleToMove = false;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            _isAbleToMove = true;
        }
    }

    private void ApplyFallMultiplier()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.gravityScale = _fallMultiplier;
        }
        else if (_rigidbody.velocity.y > 0 && Input.GetKey(KeyCode.Space) == false)
        {
            _rigidbody.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            _rigidbody.gravityScale = 1f;
        }
    }

    private void ApplyDashMultiplier()
    {
        _rigidbody.gravityScale = 0f;
    }

    private void ApplyAirLinearDrag()
    {
        _rigidbody.drag = _airLinearDrag;
    }

    private Vector2 GetInputRaw()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Move()
    {
        if (_isAbleToMove == false)
        {
            return;
        }
        
        _rigidbody.AddForce(new Vector2(_horizontalDirectionRaw, 0f) * _movementAcceleration);

        if (Mathf.Abs(_rigidbody.velocity.x) > _maxMoveSpeed) 
        {
            _rigidbody.velocity =
                new Vector2(Mathf.Sign(_rigidbody.velocity.x) * _maxMoveSpeed, _rigidbody.velocity.y);
        }
    }
    
    private void ApplyGroundLinearDrag() 
    {
        float minHorizontalDirectionValue = 0.4f;
        
        if (Mathf.Abs(_horizontalDirectionRaw) < minHorizontalDirectionValue || ChangingDirection)
        {
            _rigidbody.drag = _linearDrag;
        }
        else
        {
            _rigidbody.drag = 0f; 
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, _circleRadius, _groundLayer);
    }

    private bool IsTouchingWall()
    {
        return _wallCheckerCollider.IsTouching(_groundContactFilter);
    }

    private void WallSlide()
    {
        if (_wallSliding)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Clamp(_rigidbody.velocity.y, -_wallSlidingSpeed, float.MaxValue));
        }
    }

    private void WallJump()
    {
        WallJumped?.Invoke();
        StopCoroutine(DisableMovement(0f));
        StartCoroutine(DisableMovement(_wallJumpDuration));
        _rigidbody.velocity = Vector2.zero;
        var direction = _wallChecker.GetDirection();
        _rigidbody.AddForce(new Vector2(_xWallForce * direction, _yWallForce), ForceMode2D.Impulse);
    }

    private void Dash()
    {
        StopCoroutine(DisableMovement(0f));
        StartCoroutine(DisableMovement(_dashDuration));
        _rigidbody.AddForce(new Vector2(_horizontalDirectionRaw, _yStabilizer) * _dashForce, ForceMode2D.Impulse);
        _isDashed = true;
        Dashed?.Invoke();
    }

    private IEnumerator DisableMovement(float time)
    {
        _isAbleToMove = false;
        yield return new WaitForSeconds(time);
        _isAbleToMove = true;
        _isDashed = false;
    }
    

    private void JumpShootStatter()
    {
        if (_isGrounded == false)
        {
            _rigidbody.AddForce(Vector2.up * _jumpStatterForce, ForceMode2D.Impulse);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(_groundChecker.position, _circleRadius);
    }
}
