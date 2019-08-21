using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject ShopElement;
    public ScrollRect[] ScrollRect;
    public float Gap;

    // Start is called before the first frame update
    void Start()
    {
        string[,] result = CsvLoader.LoadCsvBy2DimensionArray("Csv/My_Paradise_Shop");

        int[] ElementCnt = { 0,0,0,0 };
        int select = 0;
        for (int i = 0; i < result.GetLength(0); ++i)
        {
            if (result[i, 0] == "거실")
            {
                select = 0;
            }
            else if (result[i, 0] == "주방")
            {
                select = 1;
            }
            else if (result[i, 0] == "침실")
            {
                select = 2;
            }
            else if (result[i, 0] == "욕실")
            {
                select = 3;
            }


            if(int.TryParse(result[i,7],out int s))
            {
                GameObject go = Instantiate(ShopElement, ScrollRect[select].content.transform);
                go.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -Gap * ElementCnt[select], 0);

                if (Resources.Load("Tile/Furniture/"+result[i,3]) is Tile)
                {
                    List<Tile> tiles = new List<Tile>();
                    for(int j = 3; j < 7; ++j)
                    {
                        if(Resources.Load("Tile/Furniture/" + result[i, j]) is Tile)
                        {
                            tiles.Add(Resources.Load("Tile/Furniture/" + result[i, j]) as Tile);
                        }
                        else
                        {
                            break;
                        }
                    }
                    go.GetComponent<ShopElement>().SetElement(result[i, 2], s,
                    FurnitureItem.StringToItemTheme(result[i, 0]),
                    FurnitureItem.StringToItemCategory(result[i, 1]),
                    tiles.ToArray());
                }
                else
                {
                    Debug.Log(result[i, 3]+": Can't loading");
                }
                ++ElementCnt[select];
            }
        }
        for (int i = 0; i < 4; ++i)
        {
            ScrollRect[i].content.GetComponent<RectTransform>().sizeDelta = new Vector2(ScrollRect[i].content.GetComponent<RectTransform>().sizeDelta.x, Gap * ElementCnt[i]);
        }
    }
}
