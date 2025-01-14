using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damageAmount;

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            var healthcontrol = collision.gameObject.GetComponent<HealthControl>();

            healthcontrol.TakeDamage(damageAmount); // Take Damage from the player
        }
    }

}
