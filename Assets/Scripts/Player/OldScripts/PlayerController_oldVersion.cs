using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

// 1. Tween tween = transform.DOPath(_waypoints, 5.0f, PathType.Linear,PathMode.Sidescroller2D).SetOptions(true).SetLookAt(0.01f); магические числа. 
// 2. _spriteRenderer = GetComponent<SpriteRenderer>(); _animator = GetComponent<Animator>(); не хватает RequireComponent. 

// 3. collision.gameObject.CompareTag("Ground") по тегу не лучшая проверка, либо GetComponent/TryGetComponent либо через физику и слои. 
// 4. if(other.gameObject.TryGetComponent<Coin>(out Coin coin)) { Debug.Log("Coin picked up"); _audioSource.Play(); } звуковой эффект, лучше создавать самой монетке, + можно эффект подбора добавить.

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController_old : MonoBehaviour
{
   [SerializeField] private float _movementSpeed = 3.0f;
   [SerializeField] private float _jumpForce = 3.0f;
   [SerializeField] private float _fallMultiplier = 3.0f;
   [SerializeField] private float _lowJumpMultiplier = 3.0f;
   [SerializeField] private LayerMask _groundLayer;
   [SerializeField] private Transform _groundChecker;
   
   private Animator _animator;
   private Rigidbody2D _rigidbody2D;
   private SpriteRenderer _spriteRenderer;
   private float _horizontalMovement;
   private bool _isGrounded = true;
   private bool _justJumped = false;

   private readonly int _isJumping = Animator.StringToHash("isJumping");
   private readonly int _isMoving = Animator.StringToHash("isMoving");
   
   private void Start()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _spriteRenderer = GetComponent<SpriteRenderer>();
      _animator = GetComponent<Animator>();
   }

   private void Update()
   {
      _horizontalMovement = Input.GetAxisRaw("Horizontal");
      MovePlayerViaTranslate();
      // Jump();

      if (Input.GetKeyDown(KeyCode.Space))
      {
         _justJumped = true;
      }
      

   }
   
   private void FixedUpdate()
   {
      if (_justJumped)
      {
         // _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
         //_rigidbody2D.velocity = Vector2.up * _jumpForce;
         _justJumped = false;
         _rigidbody2D.velocity = Vector2.up * _jumpForce;
         
         if (_rigidbody2D.velocity.y < 0)
         {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
         }
         else if (_rigidbody2D.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
         {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
         }
      }  
   }
   
   private void OnCollisionEnter2D(Collision2D collision)
   {
      // if (collision.gameObject.CompareTag("Ground"))
      // {
      //    _isGrounded = true;
      //    _animator.SetBool(_isJumping, false);
      // }
      //
      // if (collision.gameObject.TryGetComponent<TilemapCollider2D>(out TilemapCollider2D collider2D))
      // {
      //    _isGrounded = true;
      //    _animator.SetBool(_isJumping, false);
      // }
      //
      // if (collision.gameObject.layer == _ground)
      // {
      //    _isGrounded = true;
      //    _animator.SetBool(_isJumping, false);
      // }
   }

   private bool IsGround()
   {
      return _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, 0.5f, _groundLayer);
   }
   
   private void MovePlayerViaTranslate()
   {
      transform.Translate(Vector2.right * _horizontalMovement * _movementSpeed * Time.deltaTime);
        
      if (_horizontalMovement > 0)
      {
         _animator.SetBool(_isMoving, true);
         _spriteRenderer.flipX = false;
      }
      else if (_horizontalMovement < 0)
      {
         _animator.SetBool(_isMoving, true);
         _spriteRenderer.flipX = true;
      }
      else if (_horizontalMovement == 0)
      {
         _animator.SetBool(_isMoving, false);
      }

      if (IsGround())
      {
         _animator.SetBool(_isJumping, false);
      }
      else
      {
         _animator.SetBool(_isJumping, true);
      }
   }

   private void Jump()
   {
      if (Input.GetKeyDown(KeyCode.Space) && IsGround())
      {
         _animator.SetBool(_isJumping, true);
         _isGrounded = false;
         _justJumped = true;
      }
   }
}
