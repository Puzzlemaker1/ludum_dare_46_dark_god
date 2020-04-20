using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningTile <T>  : Tile
    where T : BaseUnit
{
    public int ticksTillRespawn = 100;
    public int initialSpawnTicks = 10;
    public int randomDelayMax = 10;
    public int minLifeToSpawn = 0;
    public int healthPerSpawn = 1;

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
        if (PlayerController.Instance.life > minLifeToSpawn)
        {

            spawnTimer++;
            if (spawnTimer > (initialSpawn ? initialSpawnTicks : ticksTillRespawn) + randomDelay)
            {
                spawnTimer = 0;
                if (initialSpawn)
                {
                    spawnedUnit = CreateOrBoostUnit<T>(spawningUnit, healthPerSpawn);
                    initialSpawn = false;
                }
                else if (spawnedUnit == null || spawnedUnit.GetComponentInParent<Tile>().coord == this.coord)
                {
                    //if it's at home or it's been destroyed, spawn more!
                    spawnedUnit = CreateOrBoostUnit<T>(spawningUnit, healthPerSpawn);
                }
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
