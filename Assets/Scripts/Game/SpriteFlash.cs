using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); //Get Graphic  
    }

    public void StartFlash(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        StartCoroutine(FlashCoroutine(flashDuration, flashColor, numberOfFlashes));
    }

    public IEnumerator FlashCoroutine(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        Color startColor = spriteRenderer.color;
        //float elapsedFlashTime = 0;

        // Duration of each flash (total duration divided by the number of flashes)
        float singleFlashDuration = flashDuration / numberOfFlashes;

        // Loop for the exact number of flashes
        for (int i = 0; i < numberOfFlashes; i++)
        {
            // Flash to the target color
            float flashElapsed = 0;
            while (flashElapsed < singleFlashDuration / 2) // Half the first transition
            {
                flashElapsed += Time.deltaTime;
                spriteRenderer.color = Color.Lerp(startColor, flashColor, flashElapsed / (singleFlashDuration / 2));
                yield return null;
            }

            // Flash back to the original color
            flashElapsed = 0;
            while (flashElapsed < singleFlashDuration / 2)
            {
                flashElapsed += Time.deltaTime;
                spriteRenderer.color = Color.Lerp(flashColor, startColor, flashElapsed / (singleFlashDuration / 2));
                yield return null;
            }
        }

        // Reset color to the starting color 
        spriteRenderer.color = startColor;
    }

}
