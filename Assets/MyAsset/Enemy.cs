using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine currentState;

    public Transform player;
    public float chaseDistance = 7f;
    public float attackDistance = 1.5f;

    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        currentState = new Patrol(this, agent, player);
        currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }
    public void ChangeState(StateMachine newState)
    {
        currentState.ExitState();

        currentState = newState;
        currentState.EnterState();
    }
}
