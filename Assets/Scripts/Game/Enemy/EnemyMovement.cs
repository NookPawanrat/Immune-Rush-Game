using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private float screenBorder;

    private Rigidbody2D rb2;
    private PlayerAwarenessControl playerAwarenessControl;
    private Vector2 targetDirection;
    private float changeDirectionCooldown; //Remaining time until change direction
    private Camera _camera;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        playerAwarenessControl = GetComponent<PlayerAwarenessControl>();
        // Set the initial target direction to the current direction it faceing = up
        targetDirection = transform.up; 
        _camera = Camera.main;
    
    }
    

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardTarget();
        SetVelocity();
    }
    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
        ScreenBoundsEnemy();
    }

    private void HandleRandomDirectionChange() // To make the enemy wander  
    {
        changeDirectionCooldown -= Time.deltaTime; 
        // when this value is zero, it will change to new angle direction
        if (changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(90f, -90f); // Random float number
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            targetDirection = rotation * targetDirection;

            changeDirectionCooldown = Random.Range(1f, 5f); // Random time to cooldown
        }
    }

    private void HandlePlayerTargeting()
    {
        // Check if the player is near the enemy
        if (playerAwarenessControl.AwareOfPlayer)
        {
            targetDirection = playerAwarenessControl.DirectionToPlayer;
        }
    }

    private void ScreenBoundsEnemy()
    {
        // Change their direction if ehey go off the screen
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position); // To get the position of playe in the screen
        
        if ((screenPosition.x < screenBorder && targetDirection.x < 0) ||
            (screenPosition.x > _camera.pixelWidth - screenBorder && targetDirection.x > 0))
        {
            // Check if the enemy now in the left side and target directoion is to the left
            // Or in the right side 
            targetDirection = new Vector2(-targetDirection.x, targetDirection.y); // Reverse X direction 
        }
        
        if ((screenPosition.y < screenBorder && targetDirection.y < 0) ||
            (screenPosition.y > _camera.pixelHeight - screenBorder && targetDirection.y > 0))
        {
            // Check if the player now in the bottom and trying to move out of the screen
            // Or in the top of the screen 
            targetDirection = new Vector2(targetDirection.x, -targetDirection.y); // Reverse Y direction 
        }
    }

    private void RotateTowardTarget()
    {
       // Set the rotation to Player
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb2.SetRotation(rotation);
    }


    private void SetVelocity()
    {
        //The sprite facing in the up direction
        rb2.velocity = transform.up * speed;
    }

}
