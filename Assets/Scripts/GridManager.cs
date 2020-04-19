using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int size;
    public float xScale, yScale;
    public Church churchPrefab;
    public Castle castlePrefab;
    public House housePrefab;
    public Tavern tavernPrefab;
    public Wilderness wildernessPrefab;
    public SacrificialChamber sacrificialChamberPrefab;

    public int numTowns;
    public int castlePerTown;
    public int churchPerTown;
    public int housePerTown;
    public int tavernPerTown;

    public int startingAreaSize;

    Tile[,] tiles;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[size.x, size.y];

        List<System.Tuple<int, Tile>> townConfig = new List<System.Tuple<int, Tile>>();
        townConfig.Add(new System.Tuple<int, Tile>(castlePerTown, castlePrefab));
        townConfig.Add(new System.Tuple<int, Tile>(churchPerTown, churchPrefab));
        townConfig.Add(new System.Tuple<int, Tile>(housePerTown, housePrefab));
        townConfig.Add(new System.Tuple<int, Tile>(tavernPerTown, tavernPrefab));

        ArrayList startingArea = new ArrayList();
        ArrayList[] towns = new ArrayList[numTowns];
        for (int startCount = 0; startCount < startingAreaSize; startCount++)
        {
            if(Random.Range(0, 4) == 0)
            {
                startingArea.Add(Instantiate(wildernessPrefab));
            }
            else
            {
                House newHouse = Instantiate(housePrefab);
                //One house in the wilderness
                newHouse.maxPopulation = newHouse.popPerHouse;
                startingArea.Add(newHouse);
            }
        }

        FisherYatesShuffle(startingArea);

        for (int townCount = 0; townCount < numTowns; townCount++)
        {
            towns[townCount] = new ArrayList();
            foreach (System.Tuple<int, Tile> tileTuple in townConfig)
            {
                for(int c = 0; c < tileTuple.Item1; c++)
                {
                    Tile newTile = Instantiate(tileTuple.Item2);
                    if (tileTuple.Item2 is House)
                    {
                        //Randomize population slightly.
                        ((House)newTile).maxPopulation -= Random.Range(0, 5);
                    }
                    towns[townCount].Add(newTile);
                }
            }
            FisherYatesShuffle(towns[townCount]);
            Debug.Log("Town count:" + towns[townCount].Count);
        }

        //Add our sacrifical chamber to the middle of the map.
        int centerx = size.x / 2;
        int centery = size.y / 2;
        AddTile(Instantiate(sacrificialChamberPrefab), centerx, centery);

        AddTown(startingArea, centerx , centery);
        foreach (ArrayList town in towns)
        {
            Debug.Log("adding one town");
            AddTown(town, Random.Range(0, size.x), Random.Range(0, size.y));
        }

        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                if(tiles[x, y] == null)
                {
                    if (Random.Range(0, 3) == 0)
                    {
                        House newHouse = Instantiate(housePrefab);

                        //Randomize population slightly.
                        newHouse.maxPopulation = newHouse.popPerHouse;

                        AddTile(newHouse, x, y);
                    }
                    else
                    {
                        AddTile(Instantiate(wildernessPrefab), x, y);
                    }
                }
            }
        }
    }

    private bool AddTile(Tile newTile, int x, int y)
    {
        if(!IsValidTile(new Vector2Int(x, y)))
        {
            return false;
        }
        if(tiles[x,y] != null)
        {
            Debug.Log("Non null add tile thing");
            return false;
        }
        newTile.transform.position = new Vector3(xScale * x, yScale * y);
        newTile.transform.parent = this.transform;
        Debug.Log("x: " + x + " y: " + y);
        newTile.coord = new Vector2Int(x, y);
        Debug.Log("Coord: " + newTile.coord);
        tiles[x, y] = newTile;
        return true;
    }



    private void AddTown(ArrayList town, int x, int y)
    {
        int edgeSize = (int)System.Math.Sqrt(town.Count);

        AddTownRecursive(town, (edgeSize * -1) + x, edgeSize + x, (edgeSize * -1) + y, edgeSize + y);
    }

    private void AddTownRecursive(ArrayList town, int minX, int maxX, int minY, int maxY)
    {
        for(int curx = minX; curx < maxX; curx++)
        {
            for(int cury = minY; cury < maxY; cury++)
            {
                if(AddTile((Tile)town[0], curx, cury))
                {
                    town.RemoveAt(0);
                    if(town.Count == 0)
                    {
                        Debug.Log("Finished adding town");
                        //All finished
                        return;
                    }
                }
            }
        }
        //Not done yet?
        Debug.Log("Recursively adding town");
        AddTownRecursive(town, minX - 1, maxX + 1, minY - 1, maxY + 1);
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
        if(!IsValidTile(tileCoord))
        {
            return null;
        }
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
