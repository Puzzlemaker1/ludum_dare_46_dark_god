using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int size;
    public float xScale, yScale;
    public Tile prefab;

    Tile[,] tiles;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[size.x, size.y];
        Tile clone;
        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                clone = Instantiate(prefab, new Vector3(xScale * x, yScale * y), Quaternion.identity);
                clone.coord = new Vector2Int(x, y);
                clone.transform.parent = this.transform;
                tiles[x, y] = clone;
                
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tile getTile(Vector2Int tileCoord)
    {
        return tiles[tileCoord.x, tileCoord.y];
    }
}
