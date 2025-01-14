using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour collectableBehaviour;
    private void Awake()
    {
        collectableBehaviour = GetComponent<ICollectableBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovement>();

        if (player != null) // If it not null = player has collided the collect box 
        {
            collectableBehaviour.OnCollected(player.gameObject);
            
            Destroy(gameObject);
        }
    }
}
