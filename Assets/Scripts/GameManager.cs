using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharStats[] playerStats;
    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;
    public string[] itensHeld;
    public int[] numberOfItem;
    public Item[] referenceItem;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }

       
    }

    public Item GetItemDetails(string itemToGrab)
    {
        for(int i =0; i < referenceItem.Length; i++)
        {
            if(referenceItem[i].itemName == itemToGrab)
            {
                return referenceItem[i];
            }
        }
        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itensHeld.Length - 1; i++)
            {
                if (itensHeld[i] == "")
                {
                    itensHeld[i] = itensHeld[i + 1];
                    itensHeld[i + 1] = "";
                    numberOfItem[i] = numberOfItem[i + 1];
                    numberOfItem[i + 1] = 0;

                    if(itensHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }  
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPos = 0;
        bool foundSpace = false;

        for (int i = 0; i < itensHeld.Length; i++)
        {
            if (itensHeld[i] == "" || itensHeld[i] == itemToAdd)
            {
                newItemPos = i;
                i = itensHeld.Length;
                foundSpace = true;
            }
        }

        if (foundSpace)
        {
            bool itemExists = false;
            for (int i = 0; i < referenceItem.Length; i++)
            {
                if (referenceItem[i].itemName == itemToAdd)
                {
                    itemExists = true;
                    i = referenceItem.Length;
                }
            }
            if (itemExists)
            {
                itensHeld[newItemPos] = itemToAdd;
                numberOfItem[newItemPos]++;
            }
            else
            {
                Debug.LogError(itemToAdd + " Does not exist");
            }
        }
        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        int newItemPos = 0;
        bool foundSpace = false;

        for (int i = 0; i < itensHeld.Length; i++)
        {
            if (itensHeld[i] == itemToRemove)
            {
                newItemPos = i;
                i = itensHeld.Length;
                foundSpace = true;
            }
        }
        if (foundSpace)
        {
            numberOfItem[newItemPos]--;
            if(numberOfItem[newItemPos] <= 0){
                itensHeld[newItemPos] = "";
            }
            GameMenu.instance.ShowItems();
        }
    }

   
}
