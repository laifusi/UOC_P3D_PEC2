using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    EnemyAIController enemyController;  // EnemyAIController
    float currentRotationTime = 0;      // Float that defines the time the enemy has been rotating

    /// <summary>
    /// Constructor of the AlertState
    /// </summary>
    /// <param name="controller">EnemyAIController</param>
    public AlertState(EnemyAIController controller)
    {
        enemyController = controller;
    }

    /// <summary>
    /// Method for the actions when entering the state: change the colour of the light to yellow
    /// </summary>
    public void EnterState()
    {
        enemyController.SwitchLightColor(Color.yellow);
    }

    /// <summary>
    /// Update method of the state
    /// Rotate and check whether the rotation is finished or not.
    /// If it is, change to Patrol
    /// If it's not, check if the player is in sight
    /// If it is, change to Attack
    /// </summary>
    public void UpdateState()
    {
        enemyController.RotateEnemy();
        if(currentRotationTime > enemyController.RotationTime)
        {
            currentRotationTime = 0;
            enemyController.ChangeToState(enemyController.PatrolState);
        }
        else
        {
            if(enemyController.CanSeePlayer())
            {
                enemyController.ChangeToState(enemyController.AttackState);
            }

            currentRotationTime += Time.deltaTime;
        }
    }

    public void ExitState(){}

    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerStay(Collider col) { }

    public void OnTriggerExit(Collider col) { }

    /// <summary>
    /// Method to react to attacks: change to attack state
    /// </summary>
    public void GetHit()
    {
        enemyController.ChangeToState(enemyController.AttackState);
    }
}
