using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationSwitcher_TEST : MonoBehaviour
{
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _handGunPosition;
    [SerializeField] private PlayerController _playerController;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private int _isMoving = Animator.StringToHash("isMoving");
    private int _isJumping = Animator.StringToHash("isJumping");
    private int _isWallJumpingTRIGGER = Animator.StringToHash("isWallJumpingTRIG");
    private int _isWallSliding = Animator.StringToHash("isWallSliding");
    private int _isWallJumpingBOOL = Animator.StringToHash("isWallJumping");
    // private int _isShooting = Animator.StringToHash("isShooting");
    // private int _isAttacking = Animator.StringToHash("isAttacking");
    private int _isFiring = Animator.StringToHash("isFiring");
    // private int _isFiringBool = Animator.StringToHash("isFiringBool");
    private float _horizontalMovement;
    // private Coroutine _handDisabler;

    [Header("Debug variables")]
    [SerializeField] private float _zRotation;

    private void OnEnable()
    {
        _playerController.WallJumped += OnWallJumped;
        _playerController.WallSliding += OnWallSlide;
    }

    private void OnDisable()
    {
        _playerController.WallJumped -= OnWallJumped;
        _playerController.WallSliding -= OnWallSlide;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // _handGun.SetActive(false);
        // _handGunSprite.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        Move();
        Jump();
        Shoot();
        // CheckSpriteSide();
        // Attack();
    }

    private void Move()
    {
        // _animator.SetBool(_isJumping, !IsGrounded());
        
        if (IsGrounded() == false)
        {
            _animator.Play("Player_jump");
        }
        
        if (_horizontalMovement > 0 && IsGrounded()) 
        {
            // _animator.SetBool(_isMoving, true);
            _animator.Play("Player_run");
            TurnCharacterRight();
        }
        else if (_horizontalMovement < 0 && IsGrounded())
        {
            // _animator.SetBool(_isMoving, true);
            _animator.Play("Player_run");
            TurnCharacterLeft();
        }
        else if (_horizontalMovement == 0)
        {
            _animator.Play("Player_idle");
            // _animator.SetBool(_isMoving, false);
        }
    }
    
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // _animator.SetBool(_isJumping, true);
            // _animator.Play("Player_jump");
            // CheckSpriteSide();\
        }
    }

    private void OnWallJumped()
    {
        // _animator.SetBool(_isWallJumpingBOOL, true);
        Debug.Log("WallJumped_ANIMATION_PLAY"); 
        _animator.Play("Player_jump");
        // _animator.SetTrigger(_isWallJumpingTRIGGER);
        // _animator.SetBool(_isJumping, true);
    }

    private void OnWallSlide()
    {
        // _animator.SetTrigger(_isWallSliding);
    }
    
    // используется в Kinx и Knightside
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && _horizontalMovement == 0)
        {
            // CheckSpriteSide(); 
            _animator.Play("Player_shoot");
            // _animator.SetTrigger(_isFiring);
        }
        else if (Input.GetMouseButton(0) && _horizontalMovement != 0)
        {
            _animator.Play("Player_run_n_shoot");
        }
    }
    
    private void TurnCharacterLeft()
    {
        _spriteRenderer.flipX = true;
        _handGunPosition.position += new Vector3(-0.12f, 0, 0);
        // Debug.Log($"x:{_handGunPosition.position.x} y:{_handGunPosition.position.y} z:{_handGunPosition.position.z}");
    }

    private void TurnCharacterRight()
    {
        _spriteRenderer.flipX = false;
        _handGunPosition.position += new Vector3(0.12f, 0, 0);
        // Debug.Log($"x:{_handGunPosition.position.x} y:{_handGunPosition.position.y} z:{_handGunPosition.position.z}");
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, 0.5f, _groundLayer);
    }
}