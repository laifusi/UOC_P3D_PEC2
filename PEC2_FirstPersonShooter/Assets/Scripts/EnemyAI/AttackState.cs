using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    EnemyAIController enemyController;
    float currentTimeBetweenShoots = 0;

    public AttackState(EnemyAIController controller)
    {
        enemyController = controller;
    }

    public void EnterState()
    {
        enemyController.SwitchLightColor(Color.red);
    }

    public void UpdateState()
    {
        currentTimeBetweenShoots += Time.deltaTime;
    }

    public void ExitState() { }

    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerStay(Collider col)
    {
        Vector3 lookDirection = col.transform.position - enemyController.transform.position;

        enemyController.transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(lookDirection.x, 0, lookDirection.z));

        if(currentTimeBetweenShoots > enemyController.TimeBetweenShoots)
        {
            currentTimeBetweenShoots = 0;
            //Hurt player
            col.gameObject.GetComponent<Health>().GetHurt(enemyController.DamageForce);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        enemyController.ChangeToState(enemyController.AlertState);
    }

    public void GetHit() { }
}
