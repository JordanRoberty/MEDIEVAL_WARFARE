using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnObs : MonoBehaviour
{
    public TileBase tileA;
    public TileBase tileB;
    public int obstacle_number;
    // Start is called before the first frame update
    void Start()
    {
        Vector3Int[] positions = new Vector3Int[obstacle_number];
        TileBase[] tileArray = new TileBase[positions.Length];

        for (int index = 0; index < positions.Length; index++)
        {
            positions[index] = new Vector3Int(Random.Range(6,100), -5, 0);
            tileArray[index] = index % 2 == 0 ? tileA : tileB;
        }

        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.SetTiles(positions, tileArray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
