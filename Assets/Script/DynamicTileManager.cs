using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DynamicTileManager : MonoSingleton<DynamicTileManager>
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
    public Tilemap FollowTilemap;
    public TileBase Floor,RWall,LWall;
    public Grid ClickGrid;
    public Camera MainCamera;

    private Coroutine presentCoroutine;
    private bool UpdateFurnitureArrangementBreak = false;


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // save the camera as public field if you using not the main camera
        //    Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        //    // get the collision point of the ray with the z = 0 plane
        //    Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        //    Vector3Int CellPosition;

        //    switch(CheckTileTypeAndPosition(worldPoint,out CellPosition))
        //    {
        //        case TileType.FLOOR:
        //            FurnitureTilemap.SetTile(CellPosition, Floor);
        //            break;
        //        case TileType.LWALL:
        //                FurnitureTilemap.SetTile(CellPosition, LWall);
        //            break;

        //        case TileType.RWALL:
        //                FurnitureTilemap.SetTile(CellPosition, RWall);
        //            break;
        //    }
        //}
    }

    public bool PutTile(FurnitureItem item,int spriteSelect)
    {

        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        // get the collision point of the ray with the z = 0 plane
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int CellPosition;

        switch (CheckTileTypeAndPosition(worldPoint, out CellPosition))
        {
            case TileType.FLOOR:
                FurnitureTilemap.SetTile(CellPosition, item.Tiles[spriteSelect]);
                break;
            case TileType.LWALL:
                FurnitureTilemap.SetTile(CellPosition, item.Tiles[spriteSelect]);
                break;
            case TileType.RWALL:
                FurnitureTilemap.SetTile(CellPosition, item.Tiles[spriteSelect]);
                break;
        }
        return true;
    }

    public void StartFurnitureArrangement(FurnitureItem furnitureItem)
    {
        presentCoroutine = StartCoroutine(UpdateFurnitureArrangement(furnitureItem));
    }

    public void StopFurnitureArrangement()
    {
        UpdateFurnitureArrangementBreak = true;
    }

    IEnumerator UpdateFurnitureArrangement(FurnitureItem furnitureItem)
    {
        Debug.Log(furnitureItem.Name+"ArrangeMent start");
        Vector3Int CellPosition, PrevCellPosition = new Vector3Int(100,100,0);

        while (true)
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            // get the collision point of the ray with the z = 0 plane
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);

            TileType PresentTileType = CheckTileTypeAndPosition(worldPoint,out CellPosition);

            switch (furnitureItem.Category)
            {
                case FurnitureItem.ItemCategory.FLOOR_FURNITURE://이미 설치되어 있었다면 맨 처음에는 그 위치에 있을 것, 마지막 터치(마우스) 위치를 기준으로 체크 버튼을 누르면 확정 다른 타일과 충돌 처리 해야 함
                    if (PresentTileType == TileType.FLOOR)
                    {
                        FollowTilemap.SetTile(PrevCellPosition, null);
                        FollowTilemap.SetTile(CellPosition, furnitureItem.Tiles[furnitureItem.MainTileNum]);
                        PrevCellPosition = CellPosition;
                    }
                    else
                    {
                        FollowTilemap.SetTile(PrevCellPosition, furnitureItem.Tiles[furnitureItem.MainTileNum]);
                    }
                    break;
                case FurnitureItem.ItemCategory.FLOOR_TILE://마우스가 지나가는 위치에는 모두 설치 되돌리기 키 없음, 회전 없음 이미 설치된 것 고려하지 않음
                    if(PresentTileType == TileType.FLOOR)
                    {
                        FloorTilemap.SetTile(CellPosition, furnitureItem.Tiles[0]);
                    }
                    break;  
                case FurnitureItem.ItemCategory.WALL_FURNITURE://이미 설치된 것 고려 O,회전 버튼 없음 대신 왼쪽벽 오른쪽벽 감지 후 자동 선택, 회수 버튼은 있음
                    if (PresentTileType == TileType.LWALL)
                    {
                        FollowTilemap.SetTile(PrevCellPosition, null);
                        FollowTilemap.SetTile(CellPosition, furnitureItem.Tiles[0]);
                        PrevCellPosition = CellPosition;
                    }
                    else if(PresentTileType == TileType.RWALL)
                    {
                        FollowTilemap.SetTile(PrevCellPosition, null);
                        FollowTilemap.SetTile(CellPosition, furnitureItem.Tiles[1]);
                        PrevCellPosition = CellPosition;
                    }

                    break;
                case FurnitureItem.ItemCategory.WALL_PAPER://마우스가 지나가는 위치에 모두 설치 되돌리기 키 없음, 회전 없음, 이미 설치된 것 고려하지 않음, 왼쪽벽 오른쪽벽 감지 후 자동 선택
                    if (PresentTileType == TileType.LWALL)
                    {
                        WallTilemap.SetTile(CellPosition, furnitureItem.Tiles[0]);
                    }
                    else if(PresentTileType == TileType.RWALL)
                    {
                        WallTilemap.SetTile(CellPosition, furnitureItem.Tiles[1]);
                    }
                    break;
                default:
                    Debug.LogError("없는 타입의 타일입니다");
                    break;
            }

            yield return null;
            
            if(UpdateFurnitureArrangementBreak)
            {
                Debug.Log("EndArrangement");
                UpdateFurnitureArrangementBreak = false;
                if(furnitureItem.Category == FurnitureItem.ItemCategory.FLOOR_FURNITURE || furnitureItem.Category == FurnitureItem.ItemCategory.WALL_FURNITURE)
                {
                    FurnitureTilemap.SetTile(PrevCellPosition, furnitureItem.Tiles[furnitureItem.MainTileNum]);
                    furnitureItem.position = PrevCellPosition;
                    FollowTilemap.ClearAllTiles();
                }
                
                break;
            }
        }
    }

    private TileType CheckTileTypeAndPosition(Vector3 worldPoint, out Vector3Int CellPosition)
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
