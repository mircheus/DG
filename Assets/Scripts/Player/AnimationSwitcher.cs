using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationSwitcher : MonoBehaviour
{
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _handGunPosition;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Player _player;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private int _isMoving = Animator.StringToHash("isMoving");
    private int _isJumping = Animator.StringToHash("isJumping");
    private int _isFiring = Animator.StringToHash("isFiring");
    private int _isCrouching = Animator.StringToHash("isCrouching");
    private int _isGrounded = Animator.StringToHash("isGrounded");
    private int _isDied = Animator.StringToHash("isDied");
    private int _isWallSliding = Animator.StringToHash("isWallSliding");
    private int _isWallJumping = Animator.StringToHash("isWallJumping");
    private float _horizontalMovement;
    
    [Header("Debug variables")]
    [SerializeField] private float _zRotation;
    
    private void OnEnable()
    {
        _playerController.WallJumped += OnWallJumped;
        _playerController.WallSliding += OnWallSlide;
        _player.Died += OnDied;
    }
    
    private void OnDisable()
    {
        _playerController.WallJumped -= OnWallJumped;
        _playerController.WallSliding -= OnWallSlide;
        _player.Died -= OnDied;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        Move();
        Jump();
        Shoot();
        Crouch();
    }
    
    public void TurnCharacter()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    private void Move()
    {
        _animator.SetBool(_isJumping, !IsGrounded());
        _animator.SetBool(_isGrounded, IsGrounded());
        
        if (_horizontalMovement > 0 && IsGrounded()) 
        {
            _animator.SetBool(_isMoving, true);
            TurnCharacterRight();
        }
        else if (_horizontalMovement < 0 && IsGrounded())
        {
            _animator.SetBool(_isMoving, true);
            TurnCharacterLeft();
        }
        else if (_horizontalMovement == 0)
        {
            _animator.SetBool(_isMoving, false);
        }
    }
    
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetBool(_isJumping, true);
        }
    }

    private void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.C) && IsGrounded())
        {
            _animator.SetBool(_isCrouching, true);
        }

        if (Input.GetKeyUp(KeyCode.C) && IsGrounded())
        {
            _animator.SetBool(_isCrouching, false);
        }
    }
    
    
    private void OnWallSlide()
    {
        _animator.SetBool(_isWallSliding, true);
        _animator.SetBool(_isWallJumping, false);
        _animator.SetBool(_isJumping, false);
    }

    private void OnWallJumped()
    {
        _animator.SetBool(_isWallSliding, false);
        _animator.SetBool(_isWallJumping, true);
        TurnCharacter();
    }
    
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger(_isFiring);
        }
    }
    
    
    private void TurnCharacterLeft()
    {
        _spriteRenderer.flipX = true;
        _handGunPosition.position += new Vector3(-0.12f, 0, 0);
    }

    private void TurnCharacterRight()
    {
        _spriteRenderer.flipX = false;
        _handGunPosition.position += new Vector3(0.12f, 0, 0);
    }
    
    private bool IsGrounded()
    {
        if (_playerController.IsPlayerGrounded)
        {
            _animator.SetBool(_isWallSliding, false);
            _animator.SetBool(_isWallJumping, false);
        }
        
        return _playerController.IsPlayerGrounded;
    }

    private void OnDied()
    {
        _animator.SetTrigger(_isDied);
    }
}