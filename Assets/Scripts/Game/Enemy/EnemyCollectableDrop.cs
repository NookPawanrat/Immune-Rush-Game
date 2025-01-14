using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollectableDrop : MonoBehaviour
{
    [SerializeField] private float chanceOfCollectbleDrop; // percantage value from 0-1
    
    private CollectableSpawner collectbleSpawner;

    private void Awake()
    {
        collectbleSpawner = FindObjectOfType<CollectableSpawner>();
    }

    public void RandomlyDropCollectable()
    {
        float random = Random.Range(0f, 1f);

        if (chanceOfCollectbleDrop >= random)
        {
            // If the chance higher, the more likely this condition to be true
            collectbleSpawner.SpawanCollectable(transform.position);
        }
    }
}
