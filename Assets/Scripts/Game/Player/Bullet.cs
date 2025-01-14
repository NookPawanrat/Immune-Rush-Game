using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{   
    
    private Camera _camera; 

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<EnemyMovement>())
        {
            // Check if the bullet collides with enemy (which have EnemyMovement component)
            EnemyHealthControl enemyHealthControl = collision.GetComponent<EnemyHealthControl>(); // Get Healthcontrol from Enemy
            int bulletDamage = GameManager.Instance.currentBulletDamage;
            enemyHealthControl.TakeDamage(bulletDamage); // Take damage from Enemy
            Destroy(gameObject); // Destroy Bullet
            
        }
    }

    private void DestroyWhenOffScreen()
    {
       Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

       if (screenPosition.x < 0 ||
            screenPosition.x > _camera.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > _camera.pixelHeight
            )
       {
            // Check the position of bullet, whether it out of the screen
            Destroy(gameObject); // Destroy Bullet 
        }
    }


}
