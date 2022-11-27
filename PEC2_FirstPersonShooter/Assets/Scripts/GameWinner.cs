using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinner : MonoBehaviour
{
    public static Action OnGameWon;

    /// <summary>
    /// OnTriggerEnter to check if the player won the game
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != null)
        {
            OnGameWon?.Invoke();
        }
    }
}
