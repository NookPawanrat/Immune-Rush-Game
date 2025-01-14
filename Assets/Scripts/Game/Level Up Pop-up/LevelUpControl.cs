using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpControl : MonoBehaviour
{
    [System.Serializable] 
    public class UpgradeOption
    {
        public string Name; 
        public string Description;
        public int ID;
        public Sprite Icon;
    }

    public List<UpgradeOption> allUpgrades; // List sets of all upgrade option 
    public GameObject levelUpPopup;

    public TMP_Text upgrade1NameText;
    public TMP_Text upgrade2NameText;
    
    public TMP_Text upgrade1DescriptionText;
    public TMP_Text upgrade2DescriptionText;

    public Button upgrade1Button;
    public Button upgrade2Button;

    private UpgradeOption selectedUpgrade1;
    private UpgradeOption selectedUpgrade2;

    private UpgradeControl upgradeControl;

    private void Awake()
    {
        upgradeControl = FindObjectOfType<UpgradeControl>();

    }

    public void ShowLevelUpPopup()
    {
        // Randomly pick 2 distinct upgrades option 
        List<UpgradeOption> selectedUpgrades = RandomUpgrades(2);
        
        selectedUpgrade1 = selectedUpgrades[0];
        selectedUpgrade2 = selectedUpgrades[1];

        // Update UI text 
        upgrade1NameText.text = selectedUpgrade1.Name;
        upgrade1DescriptionText.text = selectedUpgrade1.Description;
        upgrade2NameText.text = selectedUpgrade2.Name;
        upgrade2DescriptionText.text = selectedUpgrade2.Description;

        // Update the button sprites 
        UpdateButtonSprite(upgrade1Button, selectedUpgrade1.Icon);
        UpdateButtonSprite(upgrade2Button, selectedUpgrade2.Icon);

        // Show the popup
        levelUpPopup.SetActive(true);
        GameManager.Instance.PlayLevelUpSound();

        // Add button listeners
        upgrade1Button.onClick.RemoveAllListeners();
        upgrade1Button.onClick.AddListener(() => ChooseUpgrade(selectedUpgrade1));
        upgrade2Button.onClick.RemoveAllListeners();
        upgrade2Button.onClick.AddListener(() => ChooseUpgrade(selectedUpgrade2));

    }

    private void UpdateButtonSprite(Button button, Sprite newSprite)
    {
        Image buttonSprite = button.GetComponent<Image>();
        if (buttonSprite != null )
        {
            buttonSprite.sprite = newSprite;
        }
        else
        {
            Debug.LogError("Button Sprite component not found");
        }
    }

    private List<UpgradeOption> RandomUpgrades(int amount)
    {
        List<UpgradeOption> selected = new List<UpgradeOption>();
        List<UpgradeOption> available = new List<UpgradeOption>(allUpgrades); // Copy the list to avoid modifying the original

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, available.Count);
            selected.Add(available[randomIndex]); // Add the chosen option
            available.RemoveAt(randomIndex); // Remove the chosen option to avoid duplicates
        }

        return selected; // returns a list of UpgradeOption objects 
    }


    private void ChooseUpgrade(UpgradeOption selectedUpgrade)
    {
        Debug.Log($"Player chose upgrade: {selectedUpgrade.Name}"); 
        if (selectedUpgrade.ID == 1)
        {
            upgradeControl.ApplyDamageUpgrade();
            Debug.Log("Call the upgradeControl.ApplyDamageUpgrade()");
        } 
        else if (selectedUpgrade.ID == 2)
        {
            upgradeControl.ApplyMovementUpgrade();
            Debug.Log("Call the upgradeControl.ApplyMovementUpgrade()");
        } 
        else if (selectedUpgrade.ID == 3)
        {
            upgradeControl.ApplyFireRateUpgrade();
            Debug.Log("Call the upgradeControl.ApplyFireRateUpgrade()");
        }
        else if (selectedUpgrade.ID == 4)
        {
            upgradeControl.ApplyInvicibility();
            Debug.Log("Call the .ApplyInvicibility()");
        }

        // Hide the popup
        levelUpPopup.SetActive(false);

        // Resume game 
        Time.timeScale = 1f;
    }

    
    public void TriggerLevelUp()
    {
        ShowLevelUpPopup();
        // Pause the game
        Time.timeScale = 0f;
    }

   

}
