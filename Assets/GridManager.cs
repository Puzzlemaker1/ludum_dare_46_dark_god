using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int xSize, ySize;
    public float xScale, yScale;
    public Tile prefab;

    Tile[,] tiles;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[xSize, ySize];
        Tile clone;
        for(int x = 0; x < xSize; x++)
        {
            for(int y = 0; y < ySize; y++)
            {
                clone = Instantiate(prefab, new Vector3(xScale * x, yScale * y), Quaternion.identity);
                clone.yCoord = y;
                clone.xCoord = x;
                tiles[x, y] = clone;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tile getTile(int x, int y)
    {
        return tiles[x, y];
    }
}
