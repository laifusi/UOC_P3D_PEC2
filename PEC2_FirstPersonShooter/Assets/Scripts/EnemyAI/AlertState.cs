using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    EnemyAIController enemyController;
    float currentRotationTime = 0;

    public AlertState(EnemyAIController controller)
    {
        enemyController = controller;
    }

    public void EnterState()
    {
        enemyController.SwitchLightColor(Color.yellow);
    }

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

    public void GetHit()
    {
        enemyController.ChangeToState(enemyController.AttackState);
    }
}
