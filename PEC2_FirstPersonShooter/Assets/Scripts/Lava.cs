using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Health player = other.GetComponent<Health>();
        if(player != null)
        {
            player.GetHurt(1000);
        }
    }
}
