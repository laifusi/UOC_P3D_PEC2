using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinner : MonoBehaviour
{
    public static Action OnGameWon;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != null)
        {
            OnGameWon?.Invoke();
        }
    }
}
