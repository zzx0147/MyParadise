using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    public Text priceText;
    public Image thumnail;
    private FurnitureItem itemData;

    public void SetElement(string name, int price, string itemCategory,params Tile[] tile)
    {
        itemData = new FurnitureItem(name, price,itemCategory, tile);
        priceText.text = itemData.price.ToString();
        this.thumnail.sprite =  itemData.Tile[0].sprite;
        this.thumnail.SetNativeSize();
    }

    public void BuyItem()
    {
        DataManager.Instance.Cash -= itemData.price;
        DataManager.Instance.Inventory.Add(itemData);
    }
}
