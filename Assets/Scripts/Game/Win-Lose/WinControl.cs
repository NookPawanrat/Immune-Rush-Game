using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinControl : MonoBehaviour
{
    [SerializeField] GameObject winPopup;

    public void winPopupOpen() 
    {
        winPopup.SetActive(true);
        GameManager.Instance.StopSound();
        GameManager.Instance.PlayWinSound();
        Time.timeScale = 0f; // Stop the gameplay behind
    }
  

    public void Home()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ChangeScene("MainMenu");
    }
}
