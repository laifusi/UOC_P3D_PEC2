using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] bool automatic = true;
    [SerializeField] float duration = 5;

    private int currentDestination;
    private int previousDestination;
    private float interpValue;

    private void Start()
    {
        transform.position = positions[0].position;
        SwitchDestination();
    }

    private void Update()
    {
        if(automatic)
        {
            transform.position = new Vector3(Mathf.Lerp(positions[previousDestination].position.x, positions[currentDestination].position.x, interpValue),
                                            Mathf.Lerp(positions[previousDestination].position.y, positions[currentDestination].position.y, interpValue),
                                            Mathf.Lerp(positions[previousDestination].position.z, positions[currentDestination].position.z, interpValue));
            interpValue += Time.deltaTime / duration;

            if (interpValue >= 1)
            {
                SwitchDestination();
                interpValue = 0;
            }
        }
    }

    public void SwitchDestination()
    {
        previousDestination = currentDestination;
        currentDestination = (currentDestination + 1) % positions.Length;
    }
}