using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentLevelUI : MonoBehaviour
{
    public TMP_Text currentLevel;

    private void Awake()
    {
        currentLevel = GetComponent<TMP_Text>();
    }

    public void UpdateLevel()
    {
        currentLevel.text = $"Level: {GameManager.Instance.level}"; // Get the level from Gamemanager

    }
}
