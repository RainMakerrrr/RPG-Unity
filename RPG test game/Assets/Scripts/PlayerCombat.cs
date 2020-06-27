using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{


    [SerializeField] private LayerMask _enemiesMask;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private StaminaBar _staminaBar;
    private float _attackRange = 0.7f;
    private float _enemyDamage;
    private Animator _animator;
    private CharacterStats _characterStats;
    private float _attackRate = 2.0f;
    private float _nextAttackTime = 0.0f;
    public bool isPlayerInBlocked = false;
    public bool isPlayerDIed = false;

    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _characterStats = GetComponent<CharacterStats>();
        _healthBar.SetMaxHealth(_characterStats.MaxHealth);
        _staminaBar.SetMaxStamina(_characterStats.MaxStamina);
    }


    private void Update()
    {
        
        
        
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0) && _characterStats.CurrentStamina >= 5f)
            {
                Attack();
                _characterStats.CurrentStamina -= 5f;
                _staminaBar.SetStamina(_characterStats.CurrentStamina);
                _nextAttackTime = Time.time + 1f / _attackRate;
            }
        }
        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetMouseButtonDown(1) && _characterStats.CurrentStamina >=5f)
            {
                SecondAttack();
                _characterStats.CurrentStamina -= 5f;
                _staminaBar.SetStamina(_characterStats.CurrentStamina);
                _nextAttackTime = Time.time + 1f / _attackRate;
            }
        }


        if (Input.GetKey(KeyCode.E))
        {

            _animator.SetBool("isDefending", true);
            isPlayerInBlocked = true;

        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            _animator.SetBool("isDefending", false);
            isPlayerInBlocked = false;
        }

    }

    public void TakeDamage(float damage)
    {
        _characterStats.CurrentHealth -= damage;
        _healthBar.SetHealth(_characterStats.CurrentHealth);
        if (_characterStats.CurrentHealth <= 0)
        {
            _animator.SetBool("isDead", true);
            isPlayerDIed = true;
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("isAttack");
        
        Collider[] enemies = Physics.OverlapSphere(_targetPoint.position, _attackRange, _enemiesMask);
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_characterStats.PlayerDamage + 10f);
        }

    }
    private void SecondAttack()
    {
        _animator.SetTrigger("isSecondAttack");

        Collider[] enemies = Physics.OverlapSphere(_targetPoint.position, _attackRange, _enemiesMask);
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_characterStats.PlayerDamage + 15f);
        }
    }
}
