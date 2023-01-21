using System.Collections;
using System.Collections.Generic;
using System.Data;  
using UnityEngine;

public class MeleeBaseState : State
{
    // how long this state should be active for before moving on
    public float Duration;

    protected Animator _animator;

    // bool to check whether or not the next attack in the sequence should be played or not
    protected bool _shouldCombo;

    // attack index in the sequence of attack
    protected int _attackIndex;

    // кешированный хит текущей атаки
    protected Collider2D hitCollider;

    // кешированные уже задетые объекты, чтобы избежать повторных хитов
    private List<Collider2D> _collidersDamaged;

    // эффект удара возникающей при атаке врага
    // private GameObject _hitEffectPrefab;

    // private ParticleSystem _hitFXParticles;

    private float _attackPressedTimer = 0;


    public override void OnEnter(StateMachine stateMachine)
    {
        base.OnEnter(stateMachine);
        _animator = GetComponent<Animator>();
        _collidersDamaged = new List<Collider2D>();
        hitCollider = GetComponent<ComboCharacter>().hitBox;
        // _hitEffectPrefab = GetComponent<ComboCharacter>().hitEffect;
        // _hitFXParticles = GetComponent<ComboCharacter>().HitFX;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _attackPressedTimer -= Time.deltaTime;

        if (_animator.GetFloat("Weapon.Active") > 0f)
        {
            Attack();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            _attackPressedTimer = 2;
        }

        if (_animator.GetFloat("AttackWindow.Open") > 0f && _attackPressedTimer > 0)
        {
            _shouldCombo = true;
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
    }

    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);

        for (int i = 0; i < colliderCount; i++)
        {
            if (_collidersDamaged.Contains(collidersToDamage[i]) == false)
            {
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();
                
                // проверяем коллайдеры только с правильным Team Component
                if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
                {
                    // инстантиирование эффекта нужно добавить в скрипт enemy  а не здесь 
                    // GameObject.Instantiate(_hitEffectPrefab, collidersToDamage[i].transform);
                    
                    // _hitFXParticles.Play();
                    collidersToDamage[i].GetComponent<Enemy>().TakeDamage(1);
                    Debug.Log("Enemy Has Taken:" + _attackIndex + "Damage");
                    _collidersDamaged.Add(collidersToDamage[i]);
                }
            }
        }
    }
    
}
