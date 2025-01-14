using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthbarUI;
    private void Awake()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar() // Sent healthControl as parameter
    {
        float remainingHealthPercentage = GameManager.Instance.currentHealth / GameManager.Instance.maxHealth;
        healthbarUI.fillAmount = remainingHealthPercentage; // Expect value between 0-1, 1 = max health, 
        Debug.Log(remainingHealthPercentage);
    }
}
