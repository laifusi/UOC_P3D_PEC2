using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeDetector : MonoBehaviour
{
    [SerializeField] int levelToGo;

    public static Action<int> OnEnteredLevelChangeDetector;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != null)
        {
            OnEnteredLevelChangeDetector?.Invoke(levelToGo);
        }
    }
}
