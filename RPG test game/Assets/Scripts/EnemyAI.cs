using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Transform _targetPoint;

    [SerializeField] private GameObject _player;
    private Animator _animator;
    private NavMeshAgent _agent;
    private State _currentState;
    private float _damage;
    private float _attackRange = 0.5f;
    private float _maxHealth = 100f;
    public float CurrentHealth;
    public bool isEnemyAttacked = false;


    


    private void Start()
    {
        CurrentHealth = _maxHealth;
        
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerCombat>().gameObject;
        _currentState = new Idle(this.gameObject, _agent, _animator, _player);

        
        

    }

    //public void GetHit()
    //{
    //    //StartCoroutine(TakeDamage(_player.GetComponent<CharacterStats>().PlayerDamage));
    //    Debug.Log("GetHit");
    //}

   
    public bool GetHit()
    {
        Debug.Log("GetHit");
        return true;
        
    }

    public void AttackingPlayer()
    {
        StartCoroutine(AttackingDamageWithDelay());
    }

    private IEnumerator AttackingDamageWithDelay()
    {
        yield return new WaitForSeconds(1.5f);

        Collider[] players = Physics.OverlapSphere(_targetPoint.position, _attackRange, _playerMask);
        foreach (var player in players)
        {
            if (player.GetComponent<PlayerCombat>().isPlayerInBlocked == false)
            {
                player.GetComponent<PlayerCombat>().TakeDamage(_damage);
            }
        }
    }
    private void Update()
    {
        _currentState =  _currentState.Process();

        _damage = UnityEngine.Random.Range(5,9);

    }
  
}
