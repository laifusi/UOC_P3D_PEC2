using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    EnemyAIController enemyController;  // EnemyAIController
    int nextWayPoint = 0;               // Integer that defines the next way point

    /// <summary>
    /// Constructor of the PatrolState
    /// </summary>
    /// <param name="controller">EnemyAIController</param>
    public PatrolState(EnemyAIController controller)
    {
        enemyController = controller;
    }

    /// <summary>
    /// Method for the actions when entering the state: change the colour of the light to green
    /// Set the first destination
    /// </summary>
    public void EnterState()
    {
        enemyController.SwitchLightColor(Color.green);
        enemyController.SetDestination(enemyController.WayPoints[nextWayPoint]);
    }

    /// <summary>
    /// Update method of the state
    /// Check if we reached the current destination
    /// If we did, change to the next one
    /// </summary>
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

    /// <summary>
    /// Exit State: stop the nav mesh agent
    /// </summary>
    public void ExitState()
    {
        enemyController.StopNavMeshAgent();
    }

    /// <summary>
    /// OnTriggerEnter method to change to the alert state when the player activates it
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            enemyController.ChangeToState(enemyController.AlertState);
        }
    }
    
    /// <summary>
    /// If the player stays inside the trigger, continue in the alert state
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            enemyController.ChangeToState(enemyController.AlertState);
        }
    }

    public void OnTriggerExit(Collider col) {}

    /// <summary>
    /// Method to react to attacks: change to attack state
    /// </summary>
    public void GetHit()
    {
        enemyController.ChangeToState(enemyController.AttackState);
    }
}
