using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LevelUpControl;

public class ImageUnlockWeaponPopup : MonoBehaviour
{
    [System.Serializable]
    public class NewWeaponPopupImage
    {
        public string Name;
        public int ID;
        public Sprite Image;
    }

    public Button imgButtonPopup;
    public List<NewWeaponPopupImage> allWeapon;

    private NewWeaponPopupImage weaponLv3;
    private NewWeaponPopupImage weaponLv6;

    private void Awake()
    {

        weaponLv3 = allWeapon.Find(weapon => weapon.ID == 3);
        weaponLv6 = allWeapon.Find(weapon => weapon.ID == 6);

        if (weaponLv3 == null || weaponLv3.Image == null)
        {
            Debug.LogError("WeaponLv3 or its Image is null.");
        }
        if (weaponLv6 == null || weaponLv6.Image == null)
        {
            Debug.LogError("WeaponLv6 or its Image is null.");
        }
    }

    public void UpdateImageUnlockPopup()
    {
   
        Image buttonSprite = imgButtonPopup.GetComponent<Image>();
        int currentLevel = GameManager.Instance.level;

        if (currentLevel == 3)
        {
            if (weaponLv3 != null && weaponLv3.Image != null)
            {
                buttonSprite.sprite = weaponLv3.Image;
            }
            else
            {
                Debug.LogError("WeaponLv3 or its Image is missing. Cannot assign sprite.");
            }
        }
        else if (currentLevel == 6)
        {
            if (weaponLv6 != null && weaponLv6.Image != null)
            {
                buttonSprite.sprite = weaponLv6.Image;
            }
            else
            {
                Debug.LogError("WeaponLv6 or its Image is missing. Cannot assign sprite.");
            }
        }
        else
        {
            Debug.Log("No specific weapon image for this level.");
        }
    }
}













//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using static LevelUpControl;

//public class ImageUnlockWeaponPopup : MonoBehaviour
//{

//    [System.Serializable]
//    public class NewWeaponPopupImage
//    {
//        public string Name;
//        public int ID;
//        public Sprite Image;
//    }

//    public Button imgButtonPopup;

//    public List<NewWeaponPopupImage> allWeapon;

//    private NewWeaponPopupImage weaponLv3;
//    private NewWeaponPopupImage weaponLv6;


//    private ScoreControl scoreControl;

//    private void Awake()
//    {
//        scoreControl = FindObjectOfType<ScoreControl>();
//        // Reference the image by weapon ID 
//        weaponLv3 = allWeapon.Find(weapon => weapon.ID == 3);
//        weaponLv6 = allWeapon.Find(weapon => weapon.ID == 6);

//    }

//    public void UpdateImageUnlockPopup()
//    {
//        Image buttonSprite = imgButtonPopup.GetComponent<Image>();
//        if (ScoreControl.Instance.Level == 3)
//        {
//            buttonSprite.sprite = weaponLv3.Image;
//        }
//        else if (ScoreControl.Instance.Level == 6)
//        {
//            buttonSprite.sprite = weaponLv6.Image;
//        }

//    }

//}
