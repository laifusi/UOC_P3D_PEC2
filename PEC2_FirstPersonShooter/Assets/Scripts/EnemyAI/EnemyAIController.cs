using System;
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

    [Header("Materials")]
    [SerializeField] private Material fullLifeMaterial;
    [SerializeField] private Material halfLifeMaterial;
    [SerializeField] private Material lowLifeMaterial;
    [SerializeField] private Material fadeMaterial;
    [SerializeField] private Renderer[] enemyRenderers;

    public Transform[] WayPoints => waypoints;
    public float TimeBetweenShoots => timeBetweenShoots;
    public float DamageForce => damageForce;
    public float RotationTime => rotationTime;
    public PatrolState PatrolState { get; private set; }
    public AlertState AlertState { get; private set; }
    public AttackState AttackState { get; private set; }

    public Action<float> OnLifeChange;

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
        OnLifeChange?.Invoke(life);

        currentState.GetHit();

        if (life <= 0)
        {
            StartCoroutine(FadeEnemy());
        }
        else if (life <= 25)
        {
            ChangeMaterial(lowLifeMaterial);
        }
        else if (life <= 50)
        {
            ChangeMaterial(halfLifeMaterial);
        }
    }

    IEnumerator FadeEnemy()
    {
        ChangeMaterial(fadeMaterial);
        Color originalColor = fadeMaterial.color;
        Color c = fadeMaterial.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            fadeMaterial.color = c;
            yield return new WaitForSeconds(.1f);
        }
        fadeMaterial.color = originalColor;
        Destroy(gameObject);
    }

    private void ChangeMaterial(Material material)
    {
        foreach (Renderer rend in enemyRenderers)
        {
            rend.material = material;
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
