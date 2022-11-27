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

    [Header("Drops")]
    [SerializeField] private GameObject[] droppableItems;
    [SerializeField] private float probabilityOfDrop = 70;

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
    private bool shouldntUpdate;

    /// <summary>
    /// Start method where we cache the NavMesAgent, create the states and initialize the first state
    /// </summary>
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        PatrolState = new PatrolState(this);
        AlertState = new AlertState(this);
        AttackState = new AttackState(this);

        ChangeToState(PatrolState);
    }

    /// <summary>
    /// Update method: if the enemy can update we call the state's UpdateState method
    /// </summary>
    void Update()
    {
        if (shouldntUpdate)
            return;

        currentState.UpdateState();
    }

    /// <summary>
    /// Method to receive damage: if it's lower than 0, the enemy fades out and might drop an item
    /// If the life is under 25, we change its material to the lowLifeMaterial
    /// If the life is under 50, we change its material to the halfLifeMaterial
    /// </summary>
    /// <param name="damage"></param>
    public void GetHit(float damage)
    {
        life -= damage;
        OnLifeChange?.Invoke(life);

        currentState.GetHit();

        if (life <= 0)
        {
            bool dropsItem = UnityEngine.Random.Range(0f, 100f) <= probabilityOfDrop;
            if(dropsItem)
            {
                int randomId = UnityEngine.Random.Range(0, droppableItems.Length);
                Instantiate(droppableItems[randomId], transform.position, Quaternion.identity);
            }
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

    /// <summary>
    /// Coroutine to fade the enemy out
    /// We deactivate the collider and change the alpha of the fadeMaterial until it reaches 0
    /// Then we destroy the object
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeEnemy()
    {
        ChangeMaterial(fadeMaterial);
        botMesh.GetComponent<Collider>().enabled = false;
        shouldntUpdate = true;
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

    /// <summary>
    /// Method to change the material of all Renderers of the enemy
    /// </summary>
    /// <param name="material"></param>
    private void ChangeMaterial(Material material)
    {
        foreach (Renderer rend in enemyRenderers)
        {
            rend.material = material;
        }
    }

    /// <summary>
    /// Method to change to a new state.
    /// We call the ExitState method of the current one, change to the next and call its EnterState method.
    /// </summary>
    /// <param name="state"></param>
    public void ChangeToState(IEnemyState state)
    {
        if(currentState != null)
            currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }

    /// <summary>
    /// Method to change the colour of the spotlight
    /// </summary>
    /// <param name="color">Color to switch to</param>
    public void SwitchLightColor(Color color)
    {
        myLight.color = color;
    }

    /// <summary>
    /// Method to change the nav mesh agent's destination
    /// </summary>
    /// <param name="destination"></param>
    public void SetDestination(Transform destination)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(destination.position);
    }

    /// <summary>
    /// Method to check if the agent reached its destination
    /// </summary>
    /// <returns></returns>
    public bool ReachedDestination()
    {
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
    }

    /// <summary>
    /// Method to pause the navmeshagent
    /// </summary>
    public void StopNavMeshAgent()
    {
        navMeshAgent.isStopped = true;
    }

    /// <summary>
    /// Method to rotate the enemy
    /// </summary>
    public void RotateEnemy()
    {
        transform.rotation *= Quaternion.Euler(0f, Time.deltaTime * 360 * 1.0f / rotationTime, 0f);
    }

    /// <summary>
    /// Method to check if the player is in sight
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// OnTriggerEnter: calls the state's trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    /// <summary>
    /// OnTriggerStay: calls the state's trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }

    /// <summary>
    /// OnTriggerExit: calls the state's trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }
}
