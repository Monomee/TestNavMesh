using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : StateMachine
{
    public Chase(Enemy enemy, NavMeshAgent agent, Transform player) : base(enemy, agent, player) { }
    float distance;
    public override void EnterState()
    {
        Debug.Log("Enter Chase");
    }

    public override void UpdateState()
    {
        Debug.Log("Chase");
        agent.SetDestination(player.position);

        distance = Vector3.Distance(enemy.transform.position, player.position);

        if (distance <= enemy.attackDistance)
        {
            enemy.ChangeState(new Attack(enemy, agent, player));
            return;
        }
        else if (distance > enemy.chaseDistance)
        {
            enemy.ChangeState(new Patrol(enemy, agent, player));
            return;
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit Chase");
        agent.ResetPath();
    }
}
