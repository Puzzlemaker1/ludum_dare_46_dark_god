using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseUnit
{
    // Start is called before the first frame update
    public int ticksTillMove;
    private int moveTimer;
    public Vector2Int enemyLocation = new Vector2Int(-1, -1);
    override protected void UnitStart()
    {

    }


    protected override void UnitUpdate()
    {
        //Do your stuff here
        //Have a delay before every action
        moveTimer++;
        if (moveTimer > ticksTillMove)
        {
            //First combat (part of move timer?)
            Tile tile = GetComponentInParent<Tile>();
            GridManager grid = this.transform.root.GetComponent<GridManager>();
            Cultist cultist = tile.GetComponentInChildren<Cultist>();
            Victim victim = tile.GetComponentInChildren<Victim>();
            if (cultist != null)
            {
                Debug.Log("MORTAL COMMBAAAAT");
                //MORTAL COMBAAAAT
                cultist.LoseHealth(1);
                LoseHealth(1);
            }
            else if (victim != null)
            {
                //Free the victims from their oppressors!
                victim.LoseHealth(1);
            }
            else
            {
                //No combat or victims to save.
                //Check if we have an alert
                if (grid.IsValidTile(enemyLocation))
                {
                    if (enemyLocation == tile.coord)
                    {
                        //Set it to null, we got to the point.
                        enemyLocation.Set(-1, -1);
                    }
                    else
                    {
                        MoveUnit(enemyLocation - tile.coord);
                    }
                }
                else
                {
                    List<Cultist> cultists = LocateGridEntity<Cultist>(1);
                    if (cultists.Count > 0)
                    {
                        //WE FOUND A CULTIST, FUCK EM UP
                        //Grab one randomly and head towards it!
                        Debug.Log("FOUND A CULTIST, GET EM");
                        MoveUnit(cultists[Random.Range(0, cultists.Count)].GetComponentInParent<Tile>().coord - tile.coord);
                    }
                    else if (!(tile is Castle))
                    {
                        //We arn't on a castle, we should wander.

                        //Move randomly
                        MoveUnitRandom();
                    }
                }
            }
            moveTimer = 0;
        }
    }
}
