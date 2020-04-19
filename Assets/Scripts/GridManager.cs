using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int size;
    public float xScale, yScale;
    public Tile[] prefab;
    public int[] prefabCount;

    Tile[,] tiles;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[size.x, size.y];
        Tile newTile;
        ArrayList tempTiles = new ArrayList();
        for(int i = 0; i < prefab.Length; i++)
        {
            for(int j = 0; j < prefabCount[i]; j++)
            {
                tempTiles.Add(Instantiate(prefab[i]));
            }
        }
        FisherYatesShuffle(tempTiles);
        int tileCount = 0;
        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                //clone = Instantiate(prefab[Random.Range(0, prefab.Length)], new Vector3(xScale * x, yScale * y), Quaternion.identity);
                newTile = (Tile)tempTiles[tileCount];
                tileCount++;
                if(tileCount > tempTiles.Count)
                {
                    tileCount = 0;
                }
                newTile.transform.position = new Vector3(xScale * x, yScale * y);
                newTile.coord = new Vector2Int(x, y);
                newTile.transform.parent = this.transform;
                tiles[x, y] = newTile;
                
            }
        }

        
    }

    //Yoinked from stack overflow
    public static void FisherYatesShuffle(ArrayList array)
    {
        for (int i = array.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            object temp = array[j];
            array[j] = array[i];
            array[i] = temp;
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

    public bool IsValidTile(Vector2Int coord)
    {
        if (coord.x < 0 || coord.y < 0 || coord.x >= this.size.x || coord.y >= this.size.y)
        {
            return false;
        }
        return true;
    }
}
