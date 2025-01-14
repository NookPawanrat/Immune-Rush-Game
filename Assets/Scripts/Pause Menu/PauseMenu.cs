using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true); 
        Time.timeScale = 0f; // Stop the gameplay behind
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Continue the gameplay 
    }

    public void SaveGame()
    {
        Debug.Log("Save Game Pressed");
        GameManager.Instance.SaveGame();
    }

    public void Restart()
    {
        //change scene function call
        Time.timeScale = 1f;
        GameManager.Instance.ChangeScene("Level1");
    }

    public void Home()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ChangeScene("MainMenu");
    }

}
