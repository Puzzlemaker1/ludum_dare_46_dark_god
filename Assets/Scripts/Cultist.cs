using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : BaseUnit
{
    public int ticksTillSacrifice;
    private int sacrificeTimer;
    // Start is called before the first frame update
    override protected void UnitStart()
    {
        sacrificeTimer = 0;
    }


    protected override void UnitUpdate()
    {
        //Do your stuff here
        sacrificeTimer++;
        if (sacrificeTimer > ticksTillSacrifice)
        {
            sacrificeTimer = 0;
            Tile parentTile = this.GetComponentInParent<Tile>();
            if (parentTile == null)
            { 
                Debug.Log("ERROR");
            }
            parentTile.CreateUnit<Victim>(PlayerController.Instance.victim);
        }
    }
}
