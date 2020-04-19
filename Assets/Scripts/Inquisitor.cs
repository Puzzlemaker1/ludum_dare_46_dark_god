using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inquisitor : BaseUnit
{
    public int wanderTime = 20;
    private int curWanderTime = 0;
    private Vector2Int enemyLocation = new Vector2Int(-1, -1);
    private Vector2Int castleLocation = new Vector2Int(-1, -1);

    protected override void UnitUpdate()
    {

        //Search out for the enemy!
        Tile tile = GetComponentInParent<Tile>();
        GridManager grid = this.transform.root.GetComponent<GridManager>();
        Cultist cultist = tile.GetComponentInChildren<Cultist>();
        if (enemyLocation != tile.coord && (tile is SacrificialChamber || cultist != null))
        {
            //OH SHIT
            enemyLocation = tile.coord;
            //Play FOUND sound or whatever
            return;
        }
        Vector2Int moveDir = new Vector2Int(0, 0);
        if (grid.IsValidTile(enemyLocation))
        {
            if (castleLocation == tile.coord)
            {
                //We are at the castle!
                Knight knight = tile.GetComponentInChildren<Knight>();
                if (knight != null)
                {
                    knight.enemyLocation = enemyLocation;
                }
                else
                {
                    //Uhh?
                    Debug.Log("Inquisitor At castle but no knights!");
                }
                enemyLocation.Set(-1, -1);
                castleLocation.Set(-1, -1);
                return;
            }
            else
            {
                if (!grid.IsValidTile(castleLocation))
                {
                    Castle closestCastle = LocateClosestGridEntity<Castle>();
                    if (closestCastle != default(Castle))
                    {
                        castleLocation = closestCastle.coord;
                    }
                }

                //Move towards the castle!
                moveDir = castleLocation - tile.coord;
            }

        }
        else
        {

            Cultist closestCultist = LocateClosestGridEntity<Cultist>(2);
            if (closestCultist != default(Cultist))
            {
                moveDir = closestCultist.GetComponentInParent<Tile>().coord - tile.coord;

            }

        }

        if (moveDir.magnitude == 0)
        {
            if (curWanderTime > this.wanderTime)
            {
                MoveUnit(this.hometile.coord - tile.coord);
                if(this.hometile.coord == tile.coord)
                {
                    //Made it home, start wandering again.
                    curWanderTime = 0;
                }
            }
            else
            {
                MoveUnitRandom();
                curWanderTime++;
            }
        }
        else
        {
            MoveUnit(moveDir);
        }

    }
}
