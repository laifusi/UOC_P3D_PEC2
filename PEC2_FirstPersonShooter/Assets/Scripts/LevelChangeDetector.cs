using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeDetector : MonoBehaviour
{
    [SerializeField] int levelToGo;

    public static Action<int> OnEnteredLevelChangeDetector;

    /// <summary>
    /// If the player enters the trigger, we switch to the next level
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != null)
        {
            OnEnteredLevelChangeDetector?.Invoke(levelToGo);
        }
    }
}
