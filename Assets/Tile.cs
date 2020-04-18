using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool blocking;
    public int xCoord;
    public int yCoord;

    public enum TileTypeEnum
    {
        test1,
        test2,
        test3
    }

    TileTypeEnum type;

    // Start is called before the first frame update
    void Start()
    {
        type = (TileTypeEnum)Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
