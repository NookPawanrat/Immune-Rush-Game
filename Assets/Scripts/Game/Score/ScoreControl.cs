using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreControl : MonoBehaviour
{
    public UnityEvent OnScoreChanged;
    public UnityEvent OnLevelUpPopup;
    public UnityEvent OnUnlockWeaponPopup;
    public UnityEvent OnLevelUp;
    public UnityEvent OnWinGame;



    public void AddScore(int amount)
    {
 
        // Add score and update GameManager
        GameManager.Instance.score += amount;

        // Check if the target score for the current level is reached
        if (GameManager.Instance.score >= GameManager.Instance.targetScore)
        {
            LevelUp();
        }

        // Notify listeners about score changes
        OnScoreChanged.Invoke();
    }

    private void LevelUp()
    {
        GameManager.Instance.level++;
        OnLevelUp.Invoke();

        // Check for special level unlocks
        if (GameManager.Instance.level == 3 || GameManager.Instance.level == 6)
        {
            Debug.Log("Unlock weapon invoked.");
            OnUnlockWeaponPopup.Invoke();
        }else if (GameManager.Instance.level == 10)
        {
            Debug.Log("win pop up invoked.");
            OnWinGame.Invoke();
        }
        else
        {
            OnLevelUpPopup.Invoke();
        }

        // Update UI and target score
        UpdateTargetScore();
    }


    private void UpdateTargetScore()
    {
        // Increase target by 100 for each level
        GameManager.Instance.targetScore = 100 + ((GameManager.Instance.level - 1) * 100);
    }
}
