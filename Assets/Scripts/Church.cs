using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Church : SpawningTile<Inquisitor>
{
    public int ticksTillSpawn;
    private int spawnTimer;
    private bool spawnedInquisitor;
    // Start is called before the first frame update
    public override void TileUpdate()
    {

        base.TileUpdate();
    }

    public override void TileStart()
    {
    }
}
