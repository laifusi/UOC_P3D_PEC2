using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject loseCanvas;
    [SerializeField] GameObject winCanvas;

    private static GameManager instance;

    /// <summary>
    /// Awake method where we make sure the GameManager is unique and persistent between scenes
    /// </summary>
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

    /// <summary>
    /// Start method to listen for events
    /// </summary>
    private void Start()
    {
        Health.OnDeath += ManagePlayerDeath;
        GameWinner.OnGameWon += ManageWin;
    }

    /// <summary>
    /// Method to deactivate lose and win canvas at the start of a level
    /// </summary>
    public void StartLevel()
    {
        loseCanvas.SetActive(false);
        winCanvas.SetActive(false);
    }

    /// <summary>
    /// Method to deactivate menu canvas at the start of a level
    /// </summary>
    public void Play()
    {
        menuCanvas.SetActive(false);
    }

    /// <summary>
    /// Method to activate the menu canvas and deactivate the lose and win canvases when going to the Menu scene
    /// </summary>
    public void Menu()
    {
        menuCanvas.SetActive(true);
        loseCanvas.SetActive(false);
        winCanvas.SetActive(false);
    }

    /// <summary>
    /// Method to activate the loseCanvas when the player dies
    /// </summary>
    private void ManagePlayerDeath()
    {
        loseCanvas.SetActive(true);
    }

    /// <summary>
    /// Method to activate the winCanvas when the player wins
    /// </summary>
    private void ManageWin()
    {
        winCanvas.SetActive(true);
    }

    /// <summary>
    /// Method to stop listening to events
    /// </summary>
    private void OnDestroy()
    {
        Health.OnDeath -= ManagePlayerDeath;
        GameWinner.OnGameWon -= ManageWin;
    }
}
