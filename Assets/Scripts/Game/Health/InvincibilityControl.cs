using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InvincibilityControl : MonoBehaviour
{
    private HealthControl healthControl;
    private SpriteFlash spriteFlash;

    private void Awake()
    {
        healthControl = GetComponent<HealthControl>();
        spriteFlash = GetComponent<SpriteFlash>();
    }
    
    public void StartInvincibility(float invincibilityDuration, Color flashColor, int numberOfFlashes)
    {
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration, flashColor, numberOfFlashes));
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration, Color flashColor, int numberOfFlashes)
    {
        healthControl.IsInvincible = true;
        yield return spriteFlash.FlashCoroutine(invincibilityDuration, flashColor, numberOfFlashes);
        healthControl.IsInvincible = false;
    }
}
