using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;



public class State
{
   
    public enum STATE { IDLE,WALK, CHASE, ATTACK, HURT, DIE }
    public enum EVENT { ENTER, UPDATE, EXIT }

    public STATE CurrentState;
    protected EVENT CurrentStage;
    protected GameObject enemy;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected GameObject player;
    protected State nextState;

    private float _visibleDistance = 10f;
    private float _stoppingDistance = 3f;
    private float _attackRange = 2.9f;
    
    public State(GameObject _enemy, NavMeshAgent _agent, Animator _animator, GameObject _player)
    {
        enemy = _enemy;
        agent = _agent;
        animator = _animator;
        player = _player;
        CurrentStage = EVENT.ENTER;
    }

    public virtual void Enter() { CurrentStage = EVENT.UPDATE;   }
    public virtual void Update() { CurrentStage = EVENT.UPDATE;  }
    public virtual void Exit() { CurrentStage = EVENT.EXIT; }

    public State Process()
    {
        if (CurrentStage == EVENT.ENTER) Enter();
        if (CurrentStage == EVENT.UPDATE) Update();
        if (CurrentStage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    
    public bool CanStopAfterWalk()
    {
        Vector3 direction = enemy.transform.position - enemy.GetComponent<Enemy>().StartPosition;

        if (direction.magnitude <= _stoppingDistance) return true;
        return false;
    }
    public bool CanSeePlayer()
    {
        Vector3 direction = player.transform.position - enemy.transform.position;
        if (direction.magnitude <= _visibleDistance) return true;

        return false;
        
    }
    public bool CanAttackPlayer()
    {
        
        Vector3 direction = player.transform.position - enemy.transform.position;
        if (direction.magnitude <= _attackRange) return true;

        return false;
    }
    
    
    public bool isPlayerinBlock()
    {
        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
        //if(playerCombat.CurrentState == PlayerCombat.PlayerState.BLOCK)
        //{
        //    return true;
        //}
        return false;
    }
}
public class Idle : State
{
    public Idle(GameObject _enemy, NavMeshAgent _agent, Animator _animator, GameObject _player) : base(_enemy, _agent, _animator, _player)
    {
        CurrentState = STATE.IDLE;
        agent.isStopped = true;
        agent.speed = 0f;
    }

    public override void Enter()
    {
        animator.SetTrigger("isIdle");
        base.Enter();
    }
    public override void Update()
    {
        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();

        if (CanSeePlayer())
        {
            nextState = new Chase(enemy, agent, animator, player);
            CurrentStage = EVENT.EXIT;
        }
        enemy.GetComponent<Enemy>().OnTakingDamage += () =>
        {
            nextState = new Hurt(enemy, agent, animator, player);
            CurrentStage = EVENT.EXIT;
        };

        //if(playerCombat.isPlayerDIed == true)
        //{
        //    nextState = new Walk(enemy, agent, animator, player);
        //    CurrentStage = EVENT.EXIT;
        //}
        //if (IsEnemyGetHurt())
        //{
        //    nextState = new Hurt(enemy, agent, animator, player);
        //    CurrentStage = EVENT.EXIT;
        //}

    }
    public override void Exit()
    {
        animator.ResetTrigger("isIdle");
        base.Exit();
    }
}

public class Walk : State
{
    public Walk(GameObject _enemy, NavMeshAgent _agent, Animator _animator, GameObject _player) : base(_enemy, _agent, _animator, _player)
    {
        CurrentState = STATE.WALK;
        agent.speed = 2f;
        agent.isStopped = false;
        
    }
    public override void Enter()
    {
        animator.SetTrigger("isWalk");
        base.Enter();
    }
    public override void Update()
    {
        agent.SetDestination(enemy.GetComponent<Enemy>().StartPosition);
        if(agent.hasPath)
        {
            if (CanStopAfterWalk())
            {
                nextState = new Idle(enemy, agent, animator, player);
                CurrentStage = EVENT.EXIT;
            }
        }
        
        

    }
    public override void Exit()
    {
        animator.ResetTrigger("isWalk");
        base.Exit();
    }
}
public class Chase : State
{
    public Chase(GameObject _enemy, NavMeshAgent _agent, Animator _animator, GameObject _player) : base(_enemy, _agent, _animator, _player)
    {
        CurrentState = STATE.CHASE;
        agent.speed = 5f;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        animator.SetTrigger("isChasing");
        base.Enter();
    }
    public override void Update()
    {
        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();

        agent.SetDestination(player.transform.position);
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(enemy, agent, animator, player);
                CurrentStage = EVENT.EXIT;
            }
            else if (!CanSeePlayer() || playerCombat.isPlayerDIed == true)
            {
                nextState = new Walk(enemy, agent, animator, player);
                CurrentStage = EVENT.EXIT;
            }
            enemy.GetComponent<Enemy>().OnTakingDamage += () =>
            {
                nextState = new Hurt(enemy, agent, animator, player);
                CurrentStage = EVENT.EXIT;
            };
        }
        //if (IsEnemyGetHurt())
        //{
        //    nextState = new Hurt(enemy, agent, animator, player);
        //    CurrentStage = EVENT.EXIT;
        //}
    }
    public override void Exit()
    {
        animator.ResetTrigger("isChasing");
        base.Exit();
    }
}

public class Attack : State
{
    public float RotationSpeed = 2f;
    

    public Attack(GameObject _enemy, NavMeshAgent _agent, Animator _animator, GameObject _player) : base(_enemy, _agent, _animator, _player)
    {
        CurrentState = STATE.ATTACK;
        agent.isStopped = true;
        agent.speed = 0;
        
    }
    public override void Enter()
    {

        Task.Delay(4000);
        animator.SetTrigger("isAttack");
           

        
        base.Enter();
        
    }
    public override void Update()
    {

        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
      
        Vector3 direction = player.transform.position - enemy.transform.position;
        direction.y = 0f;

        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, 
                                   Quaternion.LookRotation(direction),
                                   RotationSpeed * Time.deltaTime);

        

        if (!CanAttackPlayer())
        {
            nextState = new Chase(enemy, agent, animator, player);
            CurrentStage = EVENT.EXIT; 
        }
        if (playerCombat.isPlayerDIed == true)
        {
            nextState = new Walk(enemy, agent, animator, player);
            CurrentStage = EVENT.EXIT;
        }

        enemy.GetComponent<Enemy>().OnTakingDamage += () =>
        {
            nextState = new Hurt(enemy, agent, animator, player);
            CurrentStage = EVENT.EXIT;
        };


        //if (IsEnemyGetHurt())
        //{
        //    nextState = new Hurt(enemy, agent, animator, player);
        //    CurrentStage = EVENT.EXIT;
        //}

    }
    public override void Exit()
    {

        Task.Delay(4000);
       animator.ResetTrigger("isAttack");
            

        
        base.Exit();
    }

}

public class Hurt : State
{
    public Hurt(GameObject _enemy, NavMeshAgent _agent, Animator _animator, GameObject _player) : base(_enemy, _agent, _animator, _player)
    {
        CurrentState = STATE.HURT;
        agent.speed = 0f;
        agent.isStopped = true;
        UnityEngine.Debug.Log("state is hurt");

    }
    public override void Enter()
    {
        animator.SetTrigger("isHurt");
        base.Enter();
    }
    public override void Update()
    {
        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();

        if (CanAttackPlayer())
        {
            nextState = new Attack(enemy, agent, animator, player);
            CurrentStage = EVENT.EXIT;
        }
        if (playerCombat.isPlayerDIed == true)
        {
            nextState = new Walk(enemy, agent, animator, player);
            CurrentStage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        animator.SetTrigger("isHurt");
        base.Exit();
    }
}