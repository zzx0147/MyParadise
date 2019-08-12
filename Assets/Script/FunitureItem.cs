using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunitureItem
{
    public string itemCategory;
    public int itemCode;
    public string name;
    public int price;
    public Sprite[] sprite;

    public FunitureItem(string name,int price,string itemCategory,params Sprite[] sprite)
    {
        this.name = name;
        this.price = price;
        this.sprite = sprite;
        this.itemCategory = itemCategory;
    }
}
