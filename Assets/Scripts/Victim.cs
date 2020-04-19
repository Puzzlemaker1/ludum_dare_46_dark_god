using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : BaseUnit
{
    public int ticksTillMove;
    private int moveTimer;
    private Vector2Int dir;

    public Sprite[] types = new Sprite[6];
    // Start is called before the first frame update
    override protected void UnitStart()
    {
        Debug.Log("Victim start");
        int type = Random.Range(0, 2);
        this.sprite1 = types[type*2];
        this.sprite2 = types[type*2 + 1];
    }


    protected override void UnitUpdate()
    {
        //Do your stuff here
        moveTimer++;
        if (moveTimer > ticksTillMove)
        {
            //Move randomly
            Tile tile = GetComponentInParent<Tile>();
            Cultist cultist = tile.GetComponentInChildren<Cultist>();
            if(cultist != null && cultist.hasLeader)
            {
                dir = cultist.leaderDir;
            }
            if(dir.magnitude != 0)
            {
                MoveUnit(dir);
            }

            moveTimer = 0;
        }
    }
}
