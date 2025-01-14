using UnityEngine;

public class EnemyDestroyControl : MonoBehaviour
{
    public void DestroyEnemy(float delay)
    {
        Destroy(gameObject, delay); 
    }
}
