using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float screenBorder;

    private Rigidbody2D rb2;
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputVelocity;
    //private Camera _camera;
    private Animator animator;


    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        //_camera = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirection();
        SetAnimator();

    }

    private void SetAnimator()
    {
        bool isMoveing = movementInput != Vector2.zero; // Check if the player is moving   
        animator.SetBool("IsMoving", isMoveing); // Set the value of the parameter  
    }

    private void SetPlayerVelocity()
    {
        //Set Smoothed Movement
        smoothedMovementInput = Vector2.SmoothDamp(
            smoothedMovementInput,
            movementInput,
            ref movementInputVelocity, 0.1f);

        float speed = GameManager.Instance.speed;
        rb2.velocity = smoothedMovementInput * speed;

    }

  

    private void RotateInDirection()
    {
        //Rotate to the movement direction
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rb2.MoveRotation(rotation);
        }
    }
   
    private void OnMove(InputValue inputValue)
    {
        // Movement Input
        movementInput = inputValue.Get<Vector2>();
    }

    // Public method to update the player speed
    public void UpgradePlayerSpeed(float newSpeed)
    {
        GameManager.Instance.speed = newSpeed;
        Debug.Log($"Player Speed updated to: {GameManager.Instance.speed}");
    }
}
