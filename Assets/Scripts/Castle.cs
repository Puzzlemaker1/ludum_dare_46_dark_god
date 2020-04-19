using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Tile
{
    public int ticksTillSpawn;
    private int spawnTimer;
    // Start is called before the first frame update
    public override void TileUpdate()
    {
        //WEee
        spawnTimer++;
        if (spawnTimer > ticksTillSpawn)
        {
            CreateOrBoostUnit<Knight>(PlayerController.Instance.knight);
            spawnTimer = 0;
        }
        base.TileUpdate();
    }
}
