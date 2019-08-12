using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    private FunitureItem item;
    public Image thumnail;

    public void SetElement(FunitureItem item)
    {
        this.item = item;
        thumnail.sprite = item.sprite[0];
    }
}
