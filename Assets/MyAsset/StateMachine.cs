using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateMachine : MonoBehaviour
{
    protected Enemy enemy;
    protected NavMeshAgent agent;
    protected Transform player;

    public StateMachine(Enemy enemy, NavMeshAgent agent, Transform player)
    {
        this.enemy = enemy;
        this.agent = agent;
        this.player = player;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}

