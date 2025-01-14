using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectableBehaviour 
{
    // Will call this method when player pick up the collectable
    // regardless of what the implementation is
    void OnCollected(GameObject player)
    {

    } 
}
