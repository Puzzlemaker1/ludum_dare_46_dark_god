using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succubus : BaseUnit
{
    public int damage = 3;
    private Vector2Int tavernLocation = new Vector2Int(-1, -1);
    private bool satiated = false;

    protected override void UnitUpdate()
    {

        //Search out for the enemy!
        Tile tile = GetComponentInParent<Tile>();
        GridManager grid = this.transform.root.GetComponent<GridManager>();
        Knight knight = tile.GetComponentInChildren<Knight>();
        Vector2Int moveDir = new Vector2Int(0, 0);
        if (knight != null)
        {
            //Oh hai there
            if (tile is Tavern)
            {
                //Oh hello boys OwO
                knight.LoseHealth(damage);
                //They forget what they were doing
                knight.enemyLocation = new Vector2Int(-1,-1);
                this.UnitDie();
            }
            else if (!grid.IsValidTile(tavernLocation))
            {
                Tavern foundTavern = LocateClosestGridEntity<Tavern>();
                if (foundTavern == null)
                {
                    Debug.LogError("Cannot find a tavern");
                    return;
                }
                tavernLocation = foundTavern.coord;
            }
            moveDir = tavernLocation - tile.coord;
            Debug.Log("Succubus move dir: " + moveDir);

        }
        else if (satiated)
        {
            //Did our job!
            this.UnitDie();
        }

        if (moveDir.magnitude == 0)
        {
            //Just wait around!
        }
        else
        {
            MoveUnit(moveDir);
        }


    }
}
