using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    EnemyAIController enemyController;  // EnemyAIController
    float currentTimeBetweenShoots = 0; // Time that passed since last shot

    /// <summary>
    /// Constructor of the AttackState
    /// </summary>
    /// <param name="controller">EnemyAIController</param>
    public AttackState(EnemyAIController controller)
    {
        enemyController = controller;
    }

    /// <summary>
    /// Method for the actions when entering the state: change the colour of the light to red
    /// </summary>
    public void EnterState()
    {
        enemyController.SwitchLightColor(Color.red);
    }

    /// <summary>
    /// Update method of the state
    /// Change the amount of time since last shoot
    /// </summary>
    public void UpdateState()
    {
        currentTimeBetweenShoots += Time.deltaTime;
    }

    public void ExitState() { }

    public void OnTriggerEnter(Collider col) { }

    /// <summary>
    /// OnTriggerStay Method: define the direction to look at and shoot if the time is right
    /// Apply damage to the player
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerStay(Collider col)
    {
        Vector3 lookDirection = col.transform.position - enemyController.transform.position;

        enemyController.transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(lookDirection.x, 0, lookDirection.z));

        if(currentTimeBetweenShoots > enemyController.TimeBetweenShoots)
        {
            currentTimeBetweenShoots = 0;
            //Hurt player
            if(enemyController != null)
                col.gameObject.GetComponent<Health>()?.GetHurt(enemyController.DamageForce);
        }
    }

    /// <summary>
    /// OnTriggerExit Method: Go back to AlertState when the player is out of sight
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerExit(Collider col)
    {
        enemyController.ChangeToState(enemyController.AlertState);
    }

    public void GetHit() { }
}
