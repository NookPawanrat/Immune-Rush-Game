using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private float gunDistance = 1.5f;

    private bool gunFacingRight = true;
    

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position; 

        // Rotate the gun toward the mouse
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        
        // Calculaet gun position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

        if (mousePosition.x < gun.position.x && gunFacingRight)
        {
            GunFlip();
        }
        else if (mousePosition.x > gun.position.x && !gunFacingRight) 
        {
            GunFlip();
        }
    
    }

    private void GunFlip()
    {
        gunFacingRight = !gunFacingRight;
        // Flipped visually, with no changes to its position or rotation.
        gun.localScale = new Vector3(
            gun.localScale.x, 
            gun.localScale.y * -1, 
            gun.localScale.z);
    }
}
