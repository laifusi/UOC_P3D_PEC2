using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{

    [SerializeField] Light myLight;
    [SerializeField] float timeBetweenShoots = 1;
    [SerializeField] float damageForce = 10;
    [SerializeField] float rotationTime = 3;
    [SerializeField] float shootHeight = 0.5f;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float life = 100;
    [SerializeField] Transform botMesh;

    public Transform[] WayPoints => waypoints;
    public float TimeBetweenShoots => timeBetweenShoots;
    public float DamageForce => damageForce;
    public float RotationTime => rotationTime;
    public PatrolState PatrolState { get; private set; }
    public AlertState AlertState { get; private set; }
    public AttackState AttackState { get; private set; }

    private IEnemyState currentState;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        PatrolState = new PatrolState(this);
        AlertState = new AlertState(this);
        AttackState = new AttackState(this);

        ChangeToState(PatrolState);
    }

    void Update()
    {
        currentState.UpdateState();
    }

    public void GetHit(float damage)
    {
        life -= damage;
        currentState.GetHit();


        if (life < 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeToState(IEnemyState state)
    {
        if(currentState != null)
            currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }

    public void SwitchLightColor(Color color)
    {
        myLight.color = color;
    }

    public void SetDestination(Transform destination)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(destination.position);
    }

    public bool ReachedDestination()
    {
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
    }

    public void StopNavMeshAgent()
    {
        navMeshAgent.isStopped = true;
    }

    public void RotateEnemy()
    {
        transform.rotation *= Quaternion.Euler(0f, Time.deltaTime * 360 * 1.0f / rotationTime, 0f);
    }

    public bool CanSeePlayer()
    {
        RaycastHit hit;
        Debug.DrawRay(new Vector3(botMesh.position.x, botMesh.position.y, botMesh.position.z), botMesh.forward * 100f);
        if (Physics.Raycast(new Ray(new Vector3(botMesh.position.x, botMesh.position.y, botMesh.position.z), botMesh.forward * 100f), out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }
}