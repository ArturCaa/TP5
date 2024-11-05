using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE,      
        PURSUE,    
        ATTACK,    
    };

    public enum EVENT
    {
        ENTER,     
        UPDATE,    
        EXIT       
    };

    public STATE name;                    
    protected EVENT stage;                
    protected GameObject npc;            
    protected Animator anim;              
    protected Transform player;           
    protected State nextState;            
    protected NavMeshAgent agent;         

    protected float followDistance = 10f;
    protected float stopDistance = 2f;
    protected float attackDistance = 1f;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        player = _player;
        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

 
    public bool CanSeePlayer()
    {
        float distanceToPlayer = Vector3.Distance(npc.transform.position, player.position);

        if (distanceToPlayer <= followDistance)
        {
            RaycastHit hit;
            Vector3 directionToPlayer = (player.position - npc.transform.position).normalized;

            if (Physics.Raycast(npc.transform.position, directionToPlayer, out hit, followDistance))
            {
                if (hit.transform == player)
                {
                    return true; 
                }
            }
        }
        return false; 
    }


    public bool CanAttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(npc.transform.position, player.position);
        return distanceToPlayer <= attackDistance;
    }
}

public class Idle : State
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
        : base(_npc, _agent, _anim, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        anim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}

public class Pursue : State
{
    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
        : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        anim.SetTrigger("isFollowing");
        agent.isStopped = false;
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);

        if (CanAttackPlayer())
        {
            nextState = new Attack(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if (!CanSeePlayer())
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isFollowing");
        agent.ResetPath();
        base.Exit();
    }
}

public class Attack : State
{
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
        : base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        anim.SetTrigger("isAttacking");
        agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        if (!CanAttackPlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isAttacking");
        base.Exit();
    }
}

