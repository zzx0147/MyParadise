using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureArrangementManager : MonoSingleton<FurnitureArrangementManager>
{
    private FurnitureItem TargetFurniture;
    private int spriteSelect = 0;

    public GameObject returnButton;
    public GameObject turnButton;

    public int SpriteSelect { get => spriteSelect; }

    public void ArrangementStart(FurnitureItem item)
    {
        Debug.Log("arrangementstart");
        gameObject.SetActive(true);//update를 활성화한다, false일 때는 update가 돌아가지 않음
        TargetFurniture = item;

        switch(TargetFurniture.Category)
        {
            case FurnitureItem.ItemCategory.WALL_FURNITURE:
                turnButton.SetActive(false);
                returnButton.SetActive(true);
                break;

            case FurnitureItem.ItemCategory.FLOOR_TILE:
            case FurnitureItem.ItemCategory.WALL_PAPER:
                turnButton.SetActive(false);
                returnButton.SetActive(false);
                break;

            case FurnitureItem.ItemCategory.FLOOR_FURNITURE:
                turnButton.SetActive(true);
                returnButton.SetActive(true);
                break;
        }


        DynamicTileManager.Instance.StartFurnitureArrangement(item);
    }

    public void TurnFurniture()
    {
        TargetFurniture.TurnFurniture();
    }

    public void ExitFurnitureArrangement()
    {
        DynamicTileManager.Instance.StopFurnitureArrangement();
        DataManager.Instance.SaveInventory();
    }

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ExitFurnitureArrangement();
        }
    }
}
