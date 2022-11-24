using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject loseCanvas;

    private void Start()
    {
        loseCanvas.SetActive(false);
        Health.OnDeath += ManagePlayerDeath;
    }

    private void ManagePlayerDeath()
    {
        loseCanvas.SetActive(true);
    }

    private void OnDestroy()
    {
        Health.OnDeath -= ManagePlayerDeath;
    }
}
