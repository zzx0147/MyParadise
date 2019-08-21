using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class FurnitureItem : ICloneable
{
    [Serializable]
    public enum ItemTheme
    {
        LIVINGROOM,
        KITCHEN,
        BEDROOM,
        BATHROOM,
        NONE
    }
    [Serializable]
    public enum ItemCategory
    {
        FLOOR_FURNITURE,
        WALL_FURNITURE,
        FLOOR_TILE,
        WALL_PAPER,
        NONE
    }


    private ItemTheme theme;//아이템  테마 분류
    private ItemCategory category;

    //public int itemCode;
    private string name;//아이템 이름
    private int price;//가격
    private int mainTileNum;
    public Vector3Int position;//배치된 위치, null 일 경우 소유하고 있으나 배치되지 않은 가구
    private Tile[] tiles;//4,2방향의 타일


    public ItemTheme Theme { get => theme; }
    public ItemCategory Category { get => category; }
    public string Name { get => name; set => name = value; }
    public int Price { get => price; set => price = value; }
    public int MainTileNum { get => mainTileNum;}
    public Tile[] Tiles { get => tiles; set => tiles = value; }


    public FurnitureItem(string name, int price, ItemTheme theme, ItemCategory category,int mainTileNum, params Tile[] tiles)
    {
        this.name = name;
        this.price = price;
        this.tiles = tiles;
        this.theme = theme;
        this.category = category;
        this.mainTileNum = mainTileNum;
        this.position = new Vector3Int(100, 100, 0);
    }

    public FurnitureItem(string str)
    {
        string[] s = str.Split('/');
        theme = StringToItemTheme(s[0]);
        category = StringToItemCategory(s[1]);
        name = s[2];
        price = int.Parse(s[3]);


        string positionString = s[4].Trim('(', ')');
        string[] temp = positionString.Split(',');

        position = new Vector3Int(int.Parse(temp[0].Trim()),int.Parse(temp[0].Trim()),int.Parse(temp[0].Trim()));

        mainTileNum = int.Parse(s[5]);

        List<Tile> tilesTemp = new List<Tile>();
        for(int i = 6; i < s.Length; ++i)
        {
            tilesTemp.Add(Resources.Load("Tile/Furniture/" + s[i]) as Tile);
        }

        tiles = tilesTemp.ToArray();
    }

    public void TurnFurniture()
    {
        mainTileNum += 1;
        mainTileNum %= tiles.Length;
    }

    public static ItemTheme StringToItemTheme(string s)
    {
        if (s == "거실" || s == "LIVINGROOM")
        {
            return ItemTheme.LIVINGROOM;
        }
        else if (s == "침실" || s == "BEDROOM")
        {
            return ItemTheme.BEDROOM;
        }
        else if (s == "주방" || s == "KITCHEN")
        {
            return ItemTheme.KITCHEN;
        }
        else if (s == "욕실" || s == "BATHROOM")
        {
            return ItemTheme.BATHROOM;
        }

        return ItemTheme.NONE;
    }

    public static ItemCategory StringToItemCategory(string s)
    {
        if (s == "바닥" || s == "FLOOR_FURNITURE")
        {
            return ItemCategory.FLOOR_FURNITURE;
        }
        else if (s == "벽" || s == "WALL_FURNITURE")
        {
            return ItemCategory.WALL_FURNITURE;
        }
        else if (s == "타일" || s == "FLOOR_TILE")
        {
            return ItemCategory.FLOOR_TILE;
        }
        else if (s == "벽지" || s == "WALL_PAPER")
        {
            return ItemCategory.WALL_PAPER;
        }

        return ItemCategory.NONE;
    }

    public object Clone()
    {
        return new FurnitureItem(name, price, theme, category,mainTileNum, tiles);
    }

    override public string ToString()
    {
        string tilenames = "";
        foreach(var v in tiles)
        {
            tilenames += v.name + "/";
        }


        return theme.ToString() + "/" + category.ToString() + "/" + name + "/" + price.ToString() + "/" + position.ToString() + "/"+ mainTileNum +"/" + tilenames.Substring(0,tilenames.Length - 1);
    }
}
