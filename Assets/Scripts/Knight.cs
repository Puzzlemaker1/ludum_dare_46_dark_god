using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseUnit
{

    public Vector2Int enemyLocation = new Vector2Int(-1, -1);
    public bool terrified = false;
    override protected void UnitStart()
    {

    }


    protected override void UnitUpdate()
    {
        //Do your stuff here


        //First combat (part of move timer?)
        Tile tile = GetComponentInParent<Tile>();

        if (terrified)
        {
            if (this.hometile.coord == tile.coord)
            {
                terrified = false;
            }
            else
            {
                MoveUnit(this.hometile.coord - tile.coord);
            }
            return;
        }

        GridManager grid = this.transform.root.GetComponent<GridManager>();
        Cultist cultist = tile.GetComponentInChildren<Cultist>();
        Zombie zombie = tile.GetComponentInChildren<Zombie>();
        Victim victim = tile.GetComponentInChildren<Victim>();
        if (cultist != null)
        {
            Debug.Log("MORTAL COMMBAAAAT C");
            //MORTAL COMBAAAAT
            cultist.LoseHealth(1);
            LoseHealth(1);
        }
        else if (zombie != null)
        {
            Debug.Log("MORTAL COMMBAAAAT Z");
            //MORTAL COMBAAAAT
            zombie.LoseHealth(1);
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
            //First:  Think with your pants.
            Succubus foundSuccubus = LocateClosestGridEntity<Succubus>(1);
            if (foundSuccubus != null)
            {
                //Hey there cute stuff
                MoveUnit(foundSuccubus.GetComponentInParent<Tile>().coord - tile.coord);
            }
            else if (grid.IsValidTile(enemyLocation))
            {
                //Check if we have an alert
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
                Cultist foundCultist = LocateClosestGridEntity<Cultist>(1);
                if (foundCultist != null)
                {
                    //WE FOUND A CULTIST, FUCK EM UP
                    Debug.Log("FOUND A CULTIST, GET EM");
                    MoveUnit(foundCultist.GetComponentInParent<Tile>().coord - tile.coord);
                }
                else if (!(tile is Castle))
                {
                    //We arn't on a castle, and there is nothing else to do.

                    //Go home
                    MoveUnit(this.hometile.coord - tile.coord);
                }
            }
        }

    }
}
