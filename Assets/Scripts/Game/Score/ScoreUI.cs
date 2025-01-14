using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    public void UpdateScore()
    {
        scoreText.text = $"Score: {GameManager.Instance.score}"; // Get the score from GameManager

    }
}
