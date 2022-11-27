using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    /// <summary>
    /// If the player enters the lava, it dies
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Health player = other.GetComponent<Health>();
        if(player != null)
        {
            player.GetHurt(1000);
        }
    }
}
