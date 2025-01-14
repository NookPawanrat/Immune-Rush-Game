using UnityEngine;

public class PlayerAwarenessControl : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; } // Check if the player near the enemy
    public Vector2 DirectionToPlayer { get; private set; }
    
    [SerializeField]
    private float playerAwarenessDistance; 

    private Transform player;

    private void Awake()
    {
        player = Object.FindAnyObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {
        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized; // normalized to get only direction

        // Check how close between Player and Enemy
        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance) 
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
