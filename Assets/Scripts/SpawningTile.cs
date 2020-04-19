﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningTile <T>  : Tile
    where T : BaseUnit
{
    public int ticksTillRespawn = 100;
    public int initialSpawnTicks = 10;
    public int randomDelayMax = 10;
    public int totalSpawns = 1;

    public T spawningUnit;
    //public System.Type unitType;

    private int spawnTimer;
    private int randomDelay = 0;
    private bool initialSpawn = true;
    private T spawnedUnit;
    // Start is called before the first frame update
    public override void TileUpdate()
    {
        //WEee
        spawnTimer++;
        if (spawnTimer > (initialSpawn ? initialSpawnTicks : ticksTillRespawn) + randomDelay)
        {
            spawnTimer = 0;
            if (initialSpawn)
            {
                spawnedUnit = CreateOrBoostUnit<T>(spawningUnit);
                initialSpawn = false;
            }
            else if(spawnedUnit.GetComponentInParent<Tile>().coord == this.coord)
            {
                spawnedUnit = CreateOrBoostUnit<T>(spawningUnit);
            }
        }

        base.TileUpdate();
    }

    public override void TileStart()
    {
        randomDelay = Random.Range(0, randomDelayMax);
        base.TileStart();
    }
}
