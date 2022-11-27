using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaActivator : MonoBehaviour
{
    [SerializeField] Renderer lava;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != null)
        {
            lava.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Health>() != null)
        {
            lava.enabled = true;
        }
    }
}
