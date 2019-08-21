using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    private FurnitureItem item;
    public Image thumnail;
    private GameObject panel;

    public void SetElement(FurnitureItem item, GameObject panel)
    {
        this.item = item;
        thumnail.sprite = item.Tiles[0].sprite;
        this.panel = panel;
    }


    public void UseItem()
    {
        FurnitureArrangementManager.Instance.ArrangementStart(item);
        panel.SetActive(false);
    }
}
