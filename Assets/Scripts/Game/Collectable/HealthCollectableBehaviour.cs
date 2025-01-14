using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectableBehaviour : MonoBehaviour, ICollectableBehaviour // add interface 
{
    [SerializeField] private float healthAmount;
    public void OnCollected(GameObject player) // add interface then add the method
    {
        player.GetComponent<HealthControl>().AddHealth(healthAmount);
    }
}
