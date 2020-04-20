using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : BaseUnit
{
    // public int ticksTillSacrifice;
    // private int sacrificeTimer;


    // Start is called before the first frame update
    override protected void UnitStart()
    {
        // sacrificeTimer = 0;
    }


    protected override void UnitUpdate()
    {

    }

    protected override void UnitDie()
    {
        base.UnitDie();
    }
}
