using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificialChamber : Tile
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
            //CreateUnit<Knight>(PlayerController.Instance.knight);
        }
        base.TileUpdate();
    }
}
