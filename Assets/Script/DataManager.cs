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
    public List<FurnitureItem> Inventory = new List<FurnitureItem>();

    public int Cash
    {
        get{return cash;}
        set{cash = value; CashText.text = cash.ToString(); PlayerPrefs.SetInt("Cash", cash); }
    }

    public int Heart
    {
        get{return heart;}
        set{heart = value; HeartText.text = heart.ToString(); PlayerPrefs.SetInt("Heart", heart); }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cash = PlayerPrefs.GetInt("Cash", 0);
        Heart = PlayerPrefs.GetInt("Heart", 0);
    }

}
