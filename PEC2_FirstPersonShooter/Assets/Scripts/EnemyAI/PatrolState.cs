using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    EnemyAIController enemyController;
    int nextWayPoint = 0;

    public PatrolState(EnemyAIController controller)
    {
        enemyController = controller;
    }

    public void EnterState()
    {
        enemyController.SwitchLightColor(Color.green);
        enemyController.SetDestination(enemyController.WayPoints[nextWayPoint]);
    }

    public void UpdateState()
    {
        if(enemyController.ReachedDestination())
        {
            //nextWayPoint = (nextWayPoint + 1) % enemyController.WayPoints.Length;
            nextWayPoint += 1;
            if (nextWayPoint >= enemyController.WayPoints.Length)
                nextWayPoint = 0;
            enemyController.SetDestination(enemyController.WayPoints[nextWayPoint]);
        }
    }

    public void ExitState()
    {
        enemyController.StopNavMeshAgent();
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            enemyController.ChangeToState(enemyController.AlertState);
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            enemyController.ChangeToState(enemyController.AlertState);
        }
    }

    public void OnTriggerExit(Collider col) {}

    public void GetHit()
    {
        enemyController.ChangeToState(enemyController.AttackState);
    }
}
