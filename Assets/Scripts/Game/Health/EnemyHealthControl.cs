using UnityEngine;
using UnityEngine.Events; 

public class EnemyHealthControl : MonoBehaviour
{
    [SerializeField] private Animator enemy1Animator;
   

    [SerializeField] private float currentHealth;

    [SerializeField] private float maxhealth;

    private ScoreControl scoreControl;
    //Set Level // ScoreControl.Instance.Level;

    private void Awake()
    {
        scoreControl = FindObjectOfType<ScoreControl>();
    }

    public UnityEvent OnDied; // Allow to configure many subscribers in inspector 

    public UnityEvent OnDamaged;

    public UnityEvent OnHealthChanged; // Invoke either decrease or add health

    public void TakeDamage(float damageAmount)
    {

        // If current health is 0
        if (currentHealth == 0)
        {
            return;
        
        }


        currentHealth -= damageAmount;

        OnHealthChanged.Invoke();

        if (currentHealth < 0) 
        {
            // If current health lower than 0, then set it to 0 
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            // Invoke this event, if the health has reached 0
            OnDied.Invoke();
            enemy1Animator.SetTrigger("onDied");
            
            GameManager.Instance.PlayKillSound();

        }
        else
        {
            // Invoke this event, if player has taken damage but hasn't died
            OnDamaged.Invoke();
        }

    }

}
