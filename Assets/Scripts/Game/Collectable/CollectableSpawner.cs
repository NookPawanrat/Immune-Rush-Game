using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> collectablePrefab; // just health for now 

    public void SpawanCollectable(Vector2 position)
    {
        // Random the inidex of collectable from the list 
        int index = Random.Range(0, collectablePrefab.Count); 
        var selectCollectable = collectablePrefab[index];

        Instantiate(selectCollectable, position, Quaternion.identity);
    }
}
