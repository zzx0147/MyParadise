using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureArrangementManager : MonoSingleton<FurnitureArrangementManager>
{
    private FurnitureItem TargetFurniture;
    private int SpriteSelect = 0;

    public void ArrangementStart(FurnitureItem item)
    {
        Debug.Log("arrangementstart");
        gameObject.SetActive(true);
        TargetFurniture = item;
    }

    public void TurnFurniture()
    {
        ++SpriteSelect;
        SpriteSelect %= TargetFurniture.Tile.Length;        
       

    }

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DynamicTileManager.Instance.PutTile(TargetFurniture,SpriteSelect);
        }
    }
}
