using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject loseCanvas;
    [SerializeField] GameObject winCanvas;

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
        GameWinner.OnGameWon += ManageWin;
    }

    public void StartLevel()
    {
        loseCanvas.SetActive(false);
        winCanvas.SetActive(false);
    }

    public void Play()
    {
        menuCanvas.SetActive(false);
    }

    public void Menu()
    {
        menuCanvas.SetActive(true);
        loseCanvas.SetActive(false);
        winCanvas.SetActive(false);
    }

    private void ManagePlayerDeath()
    {
        loseCanvas.SetActive(true);
    }

    private void ManageWin()
    {
        winCanvas.SetActive(true);
    }

    private void OnDestroy()
    {
        Health.OnDeath -= ManagePlayerDeath;
    }
}
