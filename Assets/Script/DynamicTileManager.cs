using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DynamicTileManager : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;
    public Grid grid;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        tilemap.SetTile(new Vector3Int(1, 0, 0), tile);   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("settile");
            // save the camera as public field if you using not the main camera
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(Input.mousePosition);
            // get the collision point of the ray with the z = 0 plane
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = grid.WorldToCell(worldPoint);

            tilemap.SetTile(position,tile);
        }
    }
}
