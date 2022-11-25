using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject loseCanvas;

    private static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Health.OnDeath += ManagePlayerDeath;
    }

    public void StartLevel()
    {
        loseCanvas.SetActive(false);
    }

    public void Play()
    {
        menuCanvas.SetActive(false);
    }

    public void Menu()
    {
        menuCanvas.SetActive(true);
        loseCanvas.SetActive(false);
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
