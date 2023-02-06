using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Patrol))]
public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float _attackCooldownTime;
    [SerializeField] private float _range;
    [SerializeField] private float _colliderDistance;   
    [SerializeField] private int _damage;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Ninja _enemy;
    [SerializeField] private Player _player;
    
    private Vector2 _colliderOffset;
    private Animator _animator;
    private Patrol _patrol;
    private Coroutine _attackCooldownCoroutine;
    private int _meleeAttack = Animator.StringToHash("MeleeAttack");
    private bool _isAlive = true;
    private bool _isAbleToAttack = true;

    private void OnEnable()
    {
        _enemy.Died += OnDied;
    }
    
    private void OnDisable()
    {
        _enemy.Died -= OnDied;
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _patrol = GetComponentInParent<Patrol>();
    }
    
    private void Update()
    {
        if (PlayerInSight())
        {
            if (_isAbleToAttack && _attackCooldownCoroutine == null) 
            {
                _animator.SetTrigger(_meleeAttack);
                _attackCooldownCoroutine = StartCoroutine(CooldownAttack(_attackCooldownTime));
            }
        }

        if (_isAlive)
        {
             _patrol.enabled = !PlayerInSight();
        }
        else
        {
            _patrol.enabled = false;
        }
    }

    private void OnDied()
    {
        _isAlive = false;
    }

    private IEnumerator CooldownAttack(float cooldownTime)
    {
        _isAbleToAttack = false;
        yield return new WaitForSeconds(cooldownTime);
        _isAbleToAttack = true;
        _attackCooldownCoroutine = null;
    }

    private bool PlayerInSight()
    {
        Vector3 size = new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z);
        _colliderOffset = _boxCollider.bounds.center + transform.right * _range * Mathf.Sign(transform.localScale.x) *_colliderDistance;
        RaycastHit2D hit = Physics2D.BoxCast(_colliderOffset, size, 0, Vector2.left, 0, _playerLayer);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                _player = player;
            }
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z);
        Vector2 colliderOffset = _boxCollider.bounds.center + transform.right * _range * Mathf.Sign(transform.localScale.x) * _colliderDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(colliderOffset, size); 
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {   
            _player.TakeDamage(_damage);
        }
    }
}
 