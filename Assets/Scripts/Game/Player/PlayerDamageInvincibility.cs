using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerDamageInvincibility : MonoBehaviour
{
    [SerializeField] private float invincibleDuration; // Set how long invicibility should last
    [SerializeField] private Color flashColor;
    [SerializeField] private int numberOfFlashes;

    private InvincibilityControl invincibilityControl;

    private void Awake()
    {
        invincibilityControl = GetComponent<InvincibilityControl>();
    }

    public void StartInvincibility() // Trigger from the OnDamage Event
    {
        invincibilityControl.StartInvincibility(invincibleDuration, flashColor, numberOfFlashes);
    }

    public void UpgradeInvincibility()
    {
        float newDuration = 5.0f;
        int newNumberOfFlashes = 16;
        invincibilityControl.StartInvincibility(newDuration, flashColor, newNumberOfFlashes);
    }
}
