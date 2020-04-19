using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : Tile
{
    public int ticksTillSpawn;
    private int spawnTimer;
    private bool spawnedInquisitor;
    // Start is called before the first frame update
    public override void TileUpdate()
    {
        spawnTimer++;
        if(!spawnedInquisitor && spawnTimer < ticksTillSpawn)
        {
            CreateUnit<Inquisitor>(PlayerController.Instance.inquisitor);
            spawnedInquisitor = true;
        }
        base.TileUpdate();
    }

    public override void TileStart()
    {
    }
}
