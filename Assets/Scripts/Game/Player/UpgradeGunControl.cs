using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeGunControl : MonoBehaviour
{
    [SerializeField] public List<Sprite> spritesList;
    [SerializeField] private Image popupImage;
    private SpriteRenderer gunInGameplay;
    

    private void Awake()
    {
        
        gunInGameplay = GetComponent<SpriteRenderer>();
    }

  

    public void ChangeToNewWeapon()
    {
        if (GameManager.Instance.level == 3)
        {
            gunInGameplay.sprite = spritesList[0];
        }
        if (GameManager.Instance.level == 6)
        {
            gunInGameplay.sprite = spritesList[1];
        }
    }
}
