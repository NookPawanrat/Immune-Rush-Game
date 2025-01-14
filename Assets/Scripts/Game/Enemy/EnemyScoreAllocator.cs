using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField] private int killscore;

    private ScoreControl scoreControl;

    private void Awake()
    {
        scoreControl = FindFirstObjectByType<ScoreControl>(); // If multiple player we need to keep track which player killed enemy
        
    }

    public void AllocateScore()
    {
        scoreControl.AddScore(killscore); // Send killscore to add score
    }
}
