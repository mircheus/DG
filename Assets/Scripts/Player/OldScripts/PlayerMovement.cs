using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _wallJumpForce;
    [SerializeField] private float _wallJumpLerp;
    [SerializeField] private Transform _groundChecker;
    [Range(0, 1)] [SerializeField] private float _circleRadius;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _fallMultiplier;
    [SerializeField] private float _lowJumpFallMultilpier;

    [Header("Wall colliders")] 
    [SerializeField] private float _rightOffset;
    [SerializeField] private float _leftOffset;

    [Header("Wall slide variables")] 
    [SerializeField] private float _slideSpeed;
    // [Range(0, 1)] 
    // [SerializeField] private float _cutJumpHeight;

    [Header("Wall jump variables")] 
    [SerializeField] private bool _wallJumped;

    // [Header("ShootSystem")] [SerializeField]
    // private PlayerShooting _player;

    // wall checkers for wall sliding and 
    private Vector2 _rightWallChecker;
    private Vector2 _leftWallChecker;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private Vector2 _standColliderSize;
    private Vector2 _crouchColliderSize;
    
    // movement variables
    private float _horizontalMovementRaw;
    private float _verticalMovementRaw;
    private float _horizontalMovement;
    private float _verticalMovement;

    // public UnityEvent _turnedLeft;
    // public UnityEvent _turnedRight;
    
    // bool variables
    [Header("Debug variables")]
    [SerializeField] private bool _isGrounded;
    private bool _isCrouching;
    [SerializeField] private bool _isAbleToMove = true;

    [SerializeField] private bool _isOnWall;
    [SerializeField] private Vector2 _directionRaw;
    private Vector2 _direction;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();

        _rightWallChecker = new Vector2(_rightOffset, 0);
        _leftWallChecker = new Vector2(_leftOffset, 0);
        _standColliderSize = _boxCollider.size;
        _crouchColliderSize = new Vector2(_standColliderSize.x, _standColliderSize.y / 2);
    }

    private void Update()
    {
        _horizontalMovementRaw = Input.GetAxisRaw("Horizontal");
        _verticalMovementRaw = Input.GetAxisRaw("Vertical");
        _directionRaw = new Vector2(_horizontalMovementRaw, _verticalMovementRaw);
        
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        _direction = new Vector2(_horizontalMovement, _verticalMovement);
        
        // Crouch();
        Jump();
        WallJump();
        
        // Move();
        _isGrounded = IsGrounded();
        _isOnWall = IsOnWall();
    }

    private void FixedUpdate()
    {
        // if (IsOnWall() && !IsGrounded() && _horizontalMovement != 0)
        if (_isOnWall && !_isGrounded && _horizontalMovementRaw != 0)
        {
            WallSlide();
            _wallJumped = false;
        }
        
        if (!_isCrouching)
        {
            Walk(_directionRaw);
        }
        // Move();

        if (!IsGrounded())
        {
            ApplyFallMultiplier();
        }
    }

    // мой вариант
    private void Move()
    {
        transform.Translate(Vector2.right * _horizontalMovementRaw * _movementSpeed * Time.deltaTime);
    }
    
    // Mix And Jam
    private void Walk(Vector2 direction)
    {
        if (!_isAbleToMove)
        {
            return;
        }

        if (_isGrounded)
        {
            _wallJumped = false;
        }
        
        // if (!wallJumped)
        // {
        //     rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        // }
        // else
        // {
        //     rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        // }

        _rigidbody.velocity = new Vector2(direction.x * _movementSpeed, _rigidbody.velocity.y);
        
        // if (!_wallJumped)
        // {
        //     _rigidbody.velocity = new Vector2(direction.x * _movementSpeed, _rigidbody.velocity.y);
        // }
        // else
        // {
        //     Vector2 jumpVelocity = new Vector2(direction.x * _movementSpeed, _rigidbody.velocity.y);
        //     _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, jumpVelocity, _wallJumpLerp * Time.deltaTime);
        // }
    }

    private void Crouch()
    {
        if (Math.Abs(_directionRaw.y - (-1)) < 0.1f)
        {
            _boxCollider.size = _crouchColliderSize;
            _isCrouching = true;
        }
        else
        {
            _boxCollider.size = _standColliderSize;
            _isCrouching = false;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.velocity = Vector2.up * _jumpForce;
        }

        // if (Input.GetKeyDown(KeyCode.Space) && _isOnWall && !_isGrounded)
        // {
        //     _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, new Vector2(_directionRaw.x * _movementSpeed, _rigidbody.velocity.y), .5f * Time.deltaTime);
        //     // _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, Vector2.up * _jumpForce, _wallJumpLerp * Time.deltaTime);
        // }

        // if (Input.GetKeyUp(KeyCode.Space))
        // {
        //     if (_rigidbody.velocity.y > 0)
        //     {
        //         _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * _cutJumpHeight);
        //     }
        // }
    }

    private void WallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnWall && !_isGrounded && _horizontalMovementRaw != 0)
        {
            StopCoroutine(DisableMovement(0));
            StartCoroutine(DisableMovement(.1f));

            Vector2 wallJumpDirection = Math.Abs(_horizontalMovementRaw - 1) < 0.1f ? Vector2.left : Vector2.right;
            Debug.Log("Walljumped!");
            _rigidbody.velocity = Vector2.up * 10 + wallJumpDirection * _wallJumpForce;
            // _rigidbody.velocity = Vector2.up * 10;

            _wallJumped = true;
        }
    }
    
    private void WallSlide()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -_slideSpeed);
    }

    private void ApplyFallMultiplier()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.gravityScale = _fallMultiplier;
        }
        else if(_rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            _rigidbody.gravityScale = _lowJumpFallMultilpier;
        }
        else
        {
            _rigidbody.gravityScale = 1f;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, _circleRadius, _groundLayer);
    }

    private bool IsOnWall()
    {
        bool isTouchingRightWall = Physics2D.OverlapCircle((Vector2)transform.position + _rightWallChecker, _circleRadius, _groundLayer);
        bool isTouchingLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + _leftWallChecker, _circleRadius, _groundLayer);

        return isTouchingLeftWall || isTouchingRightWall;
    }

    private IEnumerator DisableMovement(float time)
    {
        _isAbleToMove = false;
        yield return new WaitForSeconds(time);
        _isAbleToMove = true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(_groundChecker.position, _circleRadius);
        
        _rightWallChecker = new Vector2(_rightOffset, 0);
        _leftWallChecker = new Vector2(_leftOffset, 0);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere((Vector2)transform.position + _rightWallChecker, _circleRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + _leftWallChecker, _circleRadius);
    }
}
