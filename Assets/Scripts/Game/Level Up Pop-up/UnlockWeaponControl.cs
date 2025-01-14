using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Runtime.CompilerServices;

public class UnlockWeaponControl : MonoBehaviour
{
    [SerializeField] private Image popupImage;
   
    public UnityEvent OnUnlockWeapon;

    public GameObject unlockWeaponPopup;
    public Button nextButton;
    private UpgradeGunControl upgradeGunControl;
    private ScoreControl scoreControl;
    

    private void Awake()
    {
        upgradeGunControl = FindObjectOfType<UpgradeGunControl>();
        scoreControl = FindObjectOfType<ScoreControl>();
        popupImage = GetComponentInChildren<Image>();
    }
    

    public void ShowUnlockWeaponPopup()
    {
        OnUnlockWeapon.Invoke();
        unlockWeaponPopup.SetActive(true);
        GameManager.Instance.PlayUnlockWeaponSound();
    }

    public void TriggerOpenUnlockWeapon()
    {
        ShowUnlockWeaponPopup();
        // Pause the game
        Time.timeScale = 0f;
    }
    public void TriggerCloseUnlockWeapon()
    {
        unlockWeaponPopup.SetActive(false);
        // Resume the game
        Time.timeScale = 1f;
        // Change to new weapon
        upgradeGunControl.ChangeToNewWeapon();
    }

    
}
