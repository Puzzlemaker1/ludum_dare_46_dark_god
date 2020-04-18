using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseUnit
{
    // Start is called before the first frame update
    public int ticksTillMove;
    private int moveTimer;
    override protected void UnitStart()
    {
        
    }


    protected override void UnitUpdate()
    {
        //Do your stuff here
        moveTimer++;
        if (moveTimer > ticksTillMove)
        {
            //Move randomly
            Vector2Int moveVec = new Vector2Int(Random.Range(-1, 2), Random.Range(-1, 2));
            MoveUnit(moveVec);
            moveTimer = 0;
        }

        //Now check for combat
        Tile tile = GetComponentInParent<Tile>();
        Cultist cultist = tile.GetComponentInChildren<Cultist>();

        if(cultist != null)
        {
            Debug.Log("MORTAL COMMBAAAAT");
            //MORTAL COMBAAAAT
            cultist.LoseHealth(1);
            LoseHealth(1);
        }
    }
}
