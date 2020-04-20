using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : BaseUnit
{
    // public int ticksTillSacrifice;
    // private int sacrificeTimer;
    public int timeToLive = 10;

    // Start is called before the first frame update
    override protected void UnitStart()
    {
        // sacrificeTimer = 0;
    }


    protected override void UnitUpdate()
    {
        timeToLive--;
        Tile tile = GetComponentInParent<Tile>();
        Knight knight = tile.GetComponentInChildren<Knight>();
        Inquisitor inquisitor = tile.GetComponentInChildren<Inquisitor>();
        if (knight != null)
        {
            knight.enemyLocation.Set(-1, -1);

        }
        if (inquisitor != null)
        {
            inquisitor.EndWander();
        }
        if(timeToLive <= 0)
        {
            UnitDie();
        }
    }

    protected override void UnitDie()
    {
        base.UnitDie();
    }
}
