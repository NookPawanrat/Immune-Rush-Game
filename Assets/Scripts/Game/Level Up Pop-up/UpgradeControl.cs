using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeControl : MonoBehaviour
{
    private PlayerShoot playerShoot;
    private PlayerMovement playerMovement;
    private PlayerDamageInvincibility playerInvincibility;

    private void Awake()
    {
        playerShoot = FindObjectOfType<PlayerShoot>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerInvincibility = FindObjectOfType<PlayerDamageInvincibility>();
    }
    public void ApplyDamageUpgrade()
    {
        if (playerShoot != null)
        {
            int newDamage = GameManager.Instance.currentBulletDamage + 5 ;
            GameManager.Instance.currentBulletDamage = newDamage;
            playerShoot.UpgradeBulletDamage(newDamage);
            Debug.Log($"Now the new BulletDamage {GameManager.Instance.currentBulletDamage}");
        }
    }

    public void ApplyFireRateUpgrade()
    {
        if (playerShoot != null)
        {
            float newFireRate = GameManager.Instance.timeBetweenShots - 0.03f;
            GameManager.Instance.timeBetweenShots = newFireRate;
            playerShoot.UpgradeFireRate(newFireRate);
            Debug.Log($"Now the new Fire Rate {GameManager.Instance.timeBetweenShots}");
        }
    }

    public void ApplyMovementUpgrade()
    {
        if (playerMovement != null)
        {
            float newSpeed = GameManager.Instance.speed + 0.66f;
            GameManager.Instance.speed = newSpeed;
            playerMovement.UpgradePlayerSpeed(newSpeed);
            Debug.Log($"Now the new speed {GameManager.Instance.speed}");
        }
    }

    public void ApplyInvicibility()
    {
        playerInvincibility.UpgradeInvincibility();
        Debug.Log($"Now apply the invincibility ");
    }

}
