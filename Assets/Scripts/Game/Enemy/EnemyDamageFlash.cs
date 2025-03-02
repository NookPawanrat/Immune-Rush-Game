using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageFlash : MonoBehaviour
{
    [SerializeField] private float flashDuration;
    [SerializeField] private Color flashColor;
    [SerializeField] private int numberOfFlashes;

    private SpriteFlash spriteFlash;

    private void Awake()
    {
        spriteFlash = GetComponent<SpriteFlash>();
    }

    public void StartFlash()
    {
        // Call this when triggered on the Enemy
        spriteFlash.StartFlash(flashDuration, flashColor, numberOfFlashes);
    }
}
