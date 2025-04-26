using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : StateMachine
{
    public Attack(Enemy enemy, NavMeshAgent agent, Transform player) : base(enemy, agent, player) { }
    public override void EnterState()
    {
        Debug.Log("Enter Attack");
        agent.ResetPath();
    }

    public override void UpdateState()
    {
        Debug.Log("Attack");
        enemy.transform.LookAt(player);

        if (Vector3.Distance(enemy.transform.position, player.position) > enemy.attackDistance)
        {
            enemy.ChangeState(new Chase(enemy, agent, player));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit Chase");
    }
}
