using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = (GameObject.FindObjectOfType(typeof(DataManager)) as DataManager);
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "MyClassContainer";
                    _instance = container.AddComponent(typeof(DataManager)) as DataManager;
                }
            }

            return _instance;
        }
    }

    public Text CashText;
    public Text HeartText;
    private int cash;
    private int heart;
    public List<FunitureItem> Inventory = new List<FunitureItem>();

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
        _instance = this;
        Cash = PlayerPrefs.GetInt("Cash", 0);
        Heart = PlayerPrefs.GetInt("Heart", 0);
    }

}
