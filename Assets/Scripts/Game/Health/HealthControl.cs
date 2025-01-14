using UnityEngine;
using UnityEngine.Events; 

public class HealthControl : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    public bool IsInvincible { get; set; }// Indicate whether can currently take damage 

    public UnityEvent OnDied; // Allow to configure many subscribers in inspector 

    public UnityEvent OnDamaged;

    public UnityEvent OnHealthChanged; // Invoke either decrease or add health

    public void TakeDamage(float damageAmount)
    {
        float health = GameManager.Instance.currentHealth;
        // If current health is 0
        if (health == 0)
        {
            return;
        
        }

        if (IsInvincible)
        {
            // If the invincible is true, cannot take damage = return nothing
            return;
        }

        health -= damageAmount;
        GameManager.Instance.currentHealth = health; // Update GameManager's currentHealth

        OnHealthChanged.Invoke();

        if (health < 0) 
        {
            // If current health lower than 0, then set it to 0 
            health = 0;
        }

        if (health == 0)
        {
            // Invoke this event, if the health has reached 0
            OnDied.Invoke();
            playerAnimator.SetTrigger("onDied");
        }
        else
        {
            // Invoke this event, if player has taken damage but hasn't died
            OnDamaged.Invoke();
        }

    }

    public void AddHealth(float amountToAdd)
    {
        float health = GameManager.Instance.currentHealth;
        float max = GameManager.Instance.maxHealth;
        if (health == max)
        {
            return;

        }

        health += amountToAdd;
        GameManager.Instance.currentHealth = health; // Update GameManager's currentHealth

        OnHealthChanged.Invoke();

        if (health > max)
        {
            // If current health higher than max, then set it to max health 
            health = max;
        }

    }

}
