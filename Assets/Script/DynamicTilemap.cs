using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class DynamicTilemap : MonoBehaviour
{
    public TileBase temp;
    // Start is called before the first frame update
    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.SetTile(new Vector3Int(0, 0, 0), temp);
        tilemap.SetTile(new Vector3Int(1, 0, 0), temp);
        tilemap.SetTile(new Vector3Int(1, 1, 0), temp); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
