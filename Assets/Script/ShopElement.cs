using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    public Text priceText;
    public Image thumnail;

    [SerializeField]
    private FurnitureItem itemData;

    public void SetElement(string name, int price, FurnitureItem.ItemTheme theme, FurnitureItem.ItemCategory category ,params Tile[] tile)
    {
        itemData = new FurnitureItem(name, price,theme,category,0,tile);
        priceText.text = itemData.Price.ToString();
        this.thumnail.sprite =  itemData.Tiles[0].sprite;
        this.thumnail.SetNativeSize();
        Debug.Log(itemData.ToString());
    }

    public void BuyItem()
    {
        DataManager.Instance.AddItemToInventory(itemData.Clone() as FurnitureItem);
        DataManager.Instance.Cash -= itemData.Price;
    }
}
