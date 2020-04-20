using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : BaseUnit
{
    public int manaBoost = 10;
    private Vector2Int dir;

    public Sprite[] types = new Sprite[6];
    // Start is called before the first frame update
    override protected void UnitStart()
    {
        int type = Random.Range(0, 2);
        this.sprite1 = types[type*2];
        this.sprite2 = types[type*2 + 1];
    }


    protected override void UnitUpdate()
    {
        //Do your stuff here

        Tile tile = GetComponentInParent<Tile>();
        if (tile is SacrificialChamber)
        {
            //Sacrifice yourself!
            PlayerController.Instance.DeltaMana(manaBoost);
            UnitDie();
        }
        else if (tile is Church)
        {
            //Dunno?
        }
        Cultist cultist = tile.GetComponentInChildren<Cultist>();
        if (cultist != null && (cultist.curLeaderState == Cultist.LeaderState.taskmaster))
        {
            dir = cultist.leaderDir;
        }
        if (dir.magnitude != 0)
        {
            MoveUnit(dir);
        }
        else
        {
            List<System.Tuple<Cultist, float>> neighborCultists = LocateGridEntity<Cultist>(1);
            //don't bother checking closest...
            for (int i = 0; i < neighborCultists.Count; i++)
            {
                if (neighborCultists[i].Item1.curLeaderState == Cultist.LeaderState.taskmaster)
                {
                    MoveUnit(neighborCultists[i].Item1.GetComponentInParent<Tile>().coord - tile.coord);
                    return;
                }
            }

        }


    }
}
