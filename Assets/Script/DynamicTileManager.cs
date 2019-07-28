using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DynamicTileManager : MonoBehaviour
{
    public enum TileType
    {
        FLOOR,
        LWALL,
        RWALL,
        NONE
    }

    public int FloorXMin, FloorXMax, FloorYMin, FloorYMax, WallHeight;

    [Space(20)]
    public Tilemap FurnitureTilemap;
    public Tilemap FloorAndWallTilemap;
    public TileBase Floor,RWall,LWall;
    public Grid ClickGrid;
    public Camera MainCamera;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // save the camera as public field if you using not the main camera
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            // get the collision point of the ray with the z = 0 plane
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int CellPosition = ClickGrid.WorldToCell(worldPoint);
            Vector3 CellCenterWorldPosition = ClickGrid.GetCellCenterWorld(CellPosition);


            Debug.Log("Tile position : " + worldPoint + " : " + CellPosition);
            Debug.Log("cell World Center Position : " + CellCenterWorldPosition);


            switch(CheckTileType(worldPoint,CellPosition,CellCenterWorldPosition))
            {
                case TileType.FLOOR :
                    break;

            }




            if (CellPosition.x <= FloorXMax && CellPosition.x >= FloorXMin && CellPosition.y <= FloorYMax && CellPosition.y >= FloorYMin)
                //클릭한 타일이 설정된 바닥 타일 내의 타일일 경우(클릭한 타일이 바닥일 경우)
            {
                Debug.Log("Settile");
                FurnitureTilemap.SetTile(CellPosition, Floor);
            }
            else if (worldPoint.x < 0 && CellPosition.y <= FloorYMax + WallHeight)//클릭한 타일이 바닥이 아닌 왼쪽 벽일 경우
            {
                if (worldPoint.x < CellCenterWorldPosition.x)
                {
                    FurnitureTilemap.SetTile(new Vector3Int(CellPosition.x - 1, CellPosition.y, CellPosition.z), LWall);
                }
                else
                {
                    FurnitureTilemap.SetTile(CellPosition, LWall);
                }
            }
            else if (worldPoint.x >= 0)//클릭한 타일이 바닥이 아닌 오른쪽 벽일 경우
            {
                if (worldPoint.x > CellCenterWorldPosition.x)
                {
                    FurnitureTilemap.SetTile(new Vector3Int(CellPosition.x , CellPosition.y - 1, CellPosition.z), RWall);
                }
                else
                {
                    FurnitureTilemap.SetTile(CellPosition, RWall);
                }
            }

            


        }
    }









    public TileType CheckTileType(Vector3 worldPoint, Vector3Int CellPosition, Vector3 CellCenterWorldPosition)
    {
        return TileType.NONE;
    }
}
