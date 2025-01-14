using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public void LoadMap()
    {
        SceneManager.LoadScene("LevelMap");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void LoadGame()
    {
        Debug.Log("Load Game Pressed");
        GameManager.Instance.LoadGame();
 
        
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

}
