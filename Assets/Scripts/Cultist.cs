using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : BaseUnit
{
    public int ticksTillSacrifice;
    private int sacrificeTimer;
    public Sprite leftLeader;
    public Sprite rightLeader;
    public Sprite upLeader;
    public Sprite downLeader;

    //Make these getters and setters instead of this
    public bool hasLeader = false;
    public Vector2Int leaderDir = Vector2Int.up;

    public SpriteRenderer leaderSprite;
    private SpriteRenderer curLeaderSprite;
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

    public void UpdateLeader()
    {
        if(hasLeader != true)
        {
            Debug.Log("Making new leader!");
            hasLeader = true;
            leaderDir = Vector2Int.up;
            //leaderSprite = upLeader;
            curLeaderSprite = Instantiate(leaderSprite);
            curLeaderSprite.sprite = upLeader;
            curLeaderSprite.transform.position = this.transform.position;
        }
        else
        {
            Debug.Log("Updating Existing Leader");
            //Rotate clockwise in an infuriating fashion
            leaderDir.Set(leaderDir.y, leaderDir.x);
            if(leaderDir.x != 0)
            {
                leaderDir *= -1;
            }

            if (leaderDir == Vector2Int.right)
            {
                curLeaderSprite.sprite = rightLeader;
            }
            else if (leaderDir == Vector2Int.left)
            {
                curLeaderSprite.sprite = leftLeader;
            }
            else if (leaderDir == Vector2Int.up)
            {
                curLeaderSprite.sprite = upLeader;
            }
            else if (leaderDir == Vector2Int.down)
            {
                curLeaderSprite.sprite = downLeader;
            }
        }
    }

    protected override void UnitDie()
    {
        Destroy(leaderSprite.gameObject);
        base.UnitDie();
    }
}
