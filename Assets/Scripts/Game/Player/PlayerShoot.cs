using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform gunOffSet;

    private float lastFireTime;
    
    void Update()
    {
        // Calculate amount time passed since last fire
        float calculateTimePassed = Time.time - lastFireTime;
        float timeBetweenShots = GameManager.Instance.timeBetweenShots;
        if (calculateTimePassed >= timeBetweenShots)
        {
            // If the time is greater, then can fire the new bullet
            FireBullet();
            GameManager.Instance.PlayShootSound();
            lastFireTime = Time.time;
        }
   
    }

    private void FireBullet()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        
        // Instantiate the bullet
        //Bullet go to the gun off set position 
        GameObject bullet = Instantiate(bulletPrefab, gunOffSet.position, Quaternion.Euler(0, 0, -90));
        Rigidbody2D rb2 = bullet.GetComponent<Rigidbody2D>(); // Get rigidbody from Bullet
        rb2.velocity = bulletSpeed * direction.normalized;

        // Change the bullet color based on the level
        SpriteRenderer bulletRenderer = bullet.GetComponentInChildren<SpriteRenderer>();
        if (bulletRenderer != null)
        {
            bulletRenderer.color = GetBulletColor(GameManager.Instance.level);
        }

    }
    private Color GetBulletColor(int level)
    {
        if (level >= 6)
        {

            ColorUtility.TryParseHtmlString("#fffffc", out Color whiteColor);
            return whiteColor; 
        }
        else if (level >= 3)
        {

            ColorUtility.TryParseHtmlString("#1f9bfa", out Color blueColor);
            return blueColor;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#fad71f", out Color yellowColor);
            return yellowColor;
        }
    }

    // Public method to update the bullet damage
    public void UpgradeBulletDamage(int newDamage)
    {
        GameManager.Instance.currentBulletDamage = newDamage;
        Debug.Log($"Bullet damage updated to: {GameManager.Instance.currentBulletDamage}");
    }

    // Public method to update the bullet damage
    public void UpgradeFireRate(float newFireRate)
    {
        GameManager.Instance.timeBetweenShots = newFireRate;
        Debug.Log($"Bullet FireRate updated to: {GameManager.Instance.timeBetweenShots}");
    }
    public void NewWeaponUpgrade()
    {
        if (GameManager.Instance.level == 3)
        {
            GameManager.Instance.currentBulletDamage += 3;
            GameManager.Instance.timeBetweenShots -= 0.05f;
            Debug.Log("New weapon upgrade Lv3");
            
        }
        else if (GameManager.Instance.level == 6)
        {
            GameManager.Instance.currentBulletDamage += 3;
            GameManager.Instance.timeBetweenShots -= 0.07f;
            Debug.Log("New weapon upgrade Lv6");
        }
    }

}
