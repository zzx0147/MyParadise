using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FurnitureItem
{
    public string itemCategory;//아이템 분류
    //public int itemCode;
    public string name;//아이템 이름
    public int price;//가격
    public Tile[] Tile;//4,2방향의 타일
    public Vector3Int position;//배치된 위치, null 일 경우 소유하고 있으나 배치되지 않은 가구


    public FurnitureItem(string name,int price,string itemCategory,params Tile[] sprite)
    {
        this.name = name;
        this.price = price;
        this.Tile = sprite;
        this.itemCategory = itemCategory;
        
    }
}
