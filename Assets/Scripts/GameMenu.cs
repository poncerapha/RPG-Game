﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public GameObject menu;
    private CharStats[] playerStats;
    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;
    public GameObject[] windows;
    public GameObject[] statusButtons;
    public Text statusName, statusHP, statusMP, statusStr, statusDef, statusWpnEq, statusWpnPwr, statusArmorEq, statusArmorPwr, statusExp;
    public Image statusImage;
    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName;
    public Text itemDescription;
    public Text useButtonText;
    public static GameMenu instance;
    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (menu.activeSelf)
            {
                CloseMenu();
            }
            else
            {
                menu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
            
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "Lvl: " + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;

            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for(int i=0; i < windows.Length; i++)
        {
            if(i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }

        itemCharChoiceMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        for(int i =0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        menu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;

        itemCharChoiceMenu.SetActive(false);
    }

    public void OpenStatus()
    {
        UpdateMainStats();

        StatusChar(0);

        for(int i =0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void StatusChar(int selectedChar)
    {
        statusName.text = playerStats[selectedChar].charName;
        statusHP.text = "" + playerStats[selectedChar].currentHP + "/" + playerStats[selectedChar].maxHP;
        statusMP.text = "" + playerStats[selectedChar].currentMP + "/" + playerStats[selectedChar].maxMP;
        statusStr.text = playerStats[selectedChar].strength.ToString();
        statusDef.text = playerStats[selectedChar].defence.ToString();
        if(playerStats[selectedChar].equippedWeapon != "")
        {
            statusWpnEq.text = playerStats[selectedChar].equippedWeapon;
        }
        statusWpnPwr.text = playerStats[selectedChar].weaponPower.ToString();
        if (playerStats[selectedChar].equippedArmor != "")
        {
            statusArmorEq.text = playerStats[selectedChar].equippedArmor;
        }
        statusArmorPwr.text = playerStats[selectedChar].armorPower.ToString();
        statusExp.text = (playerStats[selectedChar].expToNextLevel[playerStats[selectedChar].playerLevel] - playerStats[selectedChar].currentEXP).ToString();
        statusImage.sprite = playerStats[selectedChar].charImage;

    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for(int i =0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;
            if(GameManager.instance.itensHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itensHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItem[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item item)
    {
        activeItem = item;
        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }

        if(activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public void DiscardItem()
    {
        if(activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(true);

        for(int i =0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);

        }
    }

    public void CloseItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);
        CloseItemCharChoice();
    }
}
