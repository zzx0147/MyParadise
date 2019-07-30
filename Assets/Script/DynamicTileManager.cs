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
    public Tilemap FloorTilemap;
    public Tilemap WallTilemap;
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
            Vector3Int CellPosition;

            switch(CheckTileTypeAndPosition(worldPoint,out CellPosition))
            {
                case TileType.FLOOR:
                    FurnitureTilemap.SetTile(CellPosition, Floor);
                    break;
                case TileType.LWALL:
                        FurnitureTilemap.SetTile(CellPosition, LWall);
                    break;

                case TileType.RWALL:
                        FurnitureTilemap.SetTile(CellPosition, RWall);
                    break;
            }
        }
    }









    public TileType CheckTileTypeAndPosition(Vector3 worldPoint, out Vector3Int CellPosition)
    {
        CellPosition = ClickGrid.WorldToCell(worldPoint);
        Vector3 CellCenterWorldPosition = ClickGrid.GetCellCenterWorld(CellPosition);

        if (FloorTilemap.GetTile(CellPosition) != null    /*CellPosition.x <= FloorXMax && CellPosition.x >= FloorXMin && CellPosition.y <= FloorYMax && CellPosition.y >= FloorYMin*/)
        {
            return TileType.FLOOR;
        }

        else if (worldPoint.x < 0 && WallTilemap.GetTile(CellPosition) != null)//클릭한 타일이 바닥이 아닌 왼쪽 벽일 경우
        {
            if (worldPoint.x < CellCenterWorldPosition.x)
            {
                CellPosition.x -= 1;
                if (WallTilemap.GetTile(CellPosition) == null)
                {
                    return TileType.NONE;
                }
                
                
            }
            return TileType.LWALL;
        }

        else if (worldPoint.x >= 0 && WallTilemap.GetTile(CellPosition) != null)//클릭한 타일이 바닥이 아닌 오른쪽 벽일 경우
        {
            if (worldPoint.x > CellCenterWorldPosition.x)
            {
                CellPosition.y -= 1;
                if (WallTilemap.GetTile(CellPosition) == null)
                {
                    return TileType.NONE;
                }
            }
            return TileType.RWALL;
        }
        else
        {
            return TileType.NONE;
        }
    }
}
