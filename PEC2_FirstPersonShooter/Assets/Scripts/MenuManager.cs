using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        LevelChangeDetector.OnEnteredLevelChangeDetector += ChangeToLevel;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeToLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        LevelChangeDetector.OnEnteredLevelChangeDetector -= ChangeToLevel;
    }
}
