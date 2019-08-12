using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelManager : MonoBehaviour
{

    public ScrollRect[] scrollRects;
    public GameObject inventoryElementPrefeb;
    public int Gap;
    // Start is called before the first frame update
    void Start()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        
        for (int j = 0; j < scrollRects.Length;++j)
        {
            Transform[] t = scrollRects[j].content.gameObject.GetComponentsInChildren<Transform>();
            for(int k = 0; k < t.Length; ++k)
            {
                if(t[k].name != "Content")
                {
                    Destroy(t[k].gameObject);
                }
            }
            
        }

        int[] elementCnt = { 0, 0, 0, 0 };
        foreach (var v in DataManager.Instance.Inventory)
        {
            int select = 0;
            if (v.itemCategory == "거실")
            {
                select = 0;
            }
            else if (v.itemCategory == "주방")
            {
                select = 1;
            }
            else if (v.itemCategory == "침실")
            {
                select = 2;
            }
            else if (v.itemCategory == "욕실")
            {
                select = 3;
            }

            GameObject go = Instantiate(inventoryElementPrefeb, scrollRects[select].content);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(Gap * elementCnt[select], go.GetComponent<RectTransform>().anchoredPosition.y);
            go.GetComponent<InventoryElement>().SetElement(v);
            ++elementCnt[select];
        }

        for (int i = 0; i < scrollRects.Length; ++i)
        {
            scrollRects[i].content.sizeDelta = new Vector2(Gap*elementCnt[i],scrollRects[i].content.sizeDelta.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
