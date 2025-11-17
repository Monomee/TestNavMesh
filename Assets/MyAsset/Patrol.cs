using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : StateMachine
{
    Vector3 point;
    public Patrol(Enemy enemy, NavMeshAgent agent, Transform player) : base(enemy, agent, player) { }
    public override void EnterState()
    {
        Debug.Log("Enter Patrol");
        //GoToNextPoint();
    }

    public override void UpdateState()
    {
        if (agent.pathPending)
        {
            return;
        }
        if (Vector3.Distance(enemy.transform.position, player.position) <= enemy.chaseDistance)
        {
            enemy.ChangeState(new Chase(enemy, agent, player));
            return;
      
        }

        PatrolWithTarget();
        //PatrolWithoutTarget();
    }

    public override void ExitState()
    {
        Debug.Log("Exit Patrol");
        agent.ResetPath();
    }

    
    void GoToNextPoint(Vector3 desiredPosition, bool chooseAnotherArea = false)
    {
        Vector3 chosenPoint = desiredPosition;
        if (chooseAnotherArea)
        {
            chosenPoint = RandomPointNavMesh.RandomPointInArea(desiredPosition);
        }
        
        if (RandomPointNavMesh.instance.GetPointOnNavmesh(chosenPoint, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.red, 10.0f);
       
            agent.SetDestination(point);  
        }
        else
        {
            Debug.LogError("KHONG TIM THAY");
            GoToNextPoint(desiredPosition, true);
        }
    }
    void GoToNextPointWithoutTarget()
    {
        if (RandomPointNavMesh.instance.GetRandomPoint(transform.position, RandomPointNavMesh.instance.range, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            agent.SetDestination(point);
        }
        else
        {
            Debug.LogError("KHONG TIM THAY");
        }
    }

    void PatrolWithTarget()
    {
        //if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        //{
        //    GoToNextPoint(player.position);
        //}
        //else
        //{
        //    GoToNextPoint();
        //}   

        if (agent.remainingDistance < 0.5f)
        {
            GoToNextPoint(player.position);
        }
    }
    void PatrolWithoutTarget()
    {
        if (agent.remainingDistance < 0.5f)
        {
            GoToNextPointWithoutTarget();
        }
    }
}
