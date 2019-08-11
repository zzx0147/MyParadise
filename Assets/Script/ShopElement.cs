using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{

    public Text Price;
    public Image Thumnail;

    public void SetElement(int price,Sprite thumnail)
    {
        Price.text = price.ToString();
        Thumnail.sprite = thumnail;
        
    }

}
