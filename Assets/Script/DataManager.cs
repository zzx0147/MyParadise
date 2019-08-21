using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DataManager : MonoSingleton<DataManager>
{
    public Text CashText;
    public Text HeartText;

    private int cash;
    private int heart;
    private List<FurnitureItem> inventory = new List<FurnitureItem>();

    public int Cash
    {
        get{return cash;}
        set{cash = value; CashText.text = cash.ToString(); PlayerPrefs.SetInt("Cash", cash);}
    }

    public int Heart
    {
        get{return heart;}
        set{heart = value; HeartText.text = heart.ToString(); PlayerPrefs.SetInt("Heart", heart);}
    }

    public FurnitureItem[] Inventory { get => inventory.ToArray();}

    public void AddItemToInventory(FurnitureItem item)
    {
        inventory.Add(item);
        SaveInventory();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Cash = PlayerPrefs.GetInt("Cash", 0);
        Heart = PlayerPrefs.GetInt("Heart", 0);
        LoadInventory();
    }

    public void SaveInventory()
    {
        string inventoryString = "";
        foreach(var v in inventory)
        {
            inventoryString += v.ToString() + "|";
        }

        inventoryString = inventoryString.Substring(0,inventoryString.Length - 1);

        PlayerPrefs.SetString("Inventory", inventoryString);
    }

    private void LoadInventory()
    {
        if (PlayerPrefs.HasKey("Inventory"))
        {
            string inventoryString = "";
            string[] inventoryStringSplit;

            inventoryString = PlayerPrefs.GetString("Inventory");
            inventoryStringSplit = inventoryString.Split('|');

            foreach (var v in inventoryStringSplit)
            {
                inventory.Add(new FurnitureItem(v));
            }
        }
    }
}
    