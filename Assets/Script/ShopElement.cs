using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    public Text priceText;
    public Image thumnail;
    private FunitureItem itemData;

    public void SetElement(string name, int price, string itemCategory,params Sprite[] thumnail)
    {
        itemData = new FunitureItem(name, price,itemCategory, thumnail);
        priceText.text = itemData.price.ToString();
        this.thumnail.sprite = itemData.sprite[0];
        this.thumnail.SetNativeSize();
    }

    public void BuyItem()
    {
        DataManager.Instance.Cash -= itemData.price;
        DataManager.Instance.Inventory.Add(itemData);
    }
}
