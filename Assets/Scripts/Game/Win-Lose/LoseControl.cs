using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseControl : MonoBehaviour
{
    [SerializeField] GameObject losePopup;
    public void losePopupOpen()
    {
        losePopup.SetActive(true);
        //GameManager.Instance.StopSound();
        GameManager.Instance.PlayLoseSound();
        Time.timeScale = 0f; // Stop the gameplay behind
    }
    public void Home()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ChangeScene("MainMenu");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ChangeScene("Level1");
    }
}
