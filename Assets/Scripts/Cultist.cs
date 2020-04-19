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
    public LeaderState hasLeader = LeaderState.none;
    public Vector2Int leaderDir = Vector2Int.up;

    public SpriteRenderer leaderSprite;
    public SpriteRenderer necromancerSprite;
    private SpriteRenderer curLeaderSprite = null;
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
    public void UpdateNecromancer()
    {
        Debug.Log("Adding Necromancer");
        curLeaderSprite = Instantiate(necromancerSprite);
        hasLeader = LeaderState.necromancer;
        curLeaderSprite.transform.position = this.transform.position;
        curLeaderSprite.transform.parent = this.GetComponentInParent<Tile>().transform;
    }
    public void UpdateLeader()
    {
        if(hasLeader == LeaderState.none)
        {
            Debug.Log("Making new leader!");
            hasLeader = LeaderState.leader;
            leaderDir = Vector2Int.up;
            //leaderSprite = upLeader;
            curLeaderSprite = Instantiate(leaderSprite);
            curLeaderSprite.sprite = upLeader;
            curLeaderSprite.transform.position = this.transform.position;
            curLeaderSprite.transform.parent = this.GetComponentInParent<Tile>().transform;
        }
        else if (hasLeader != LeaderState.none)
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
        if (curLeaderSprite != null)
        {
            Destroy(curLeaderSprite.gameObject);
        }

        base.UnitDie();
    }
    public override void LoseHealth(int healthDelta)
    {
        this.health -= healthDelta;
        if (deathFX != null)
        {
           Instantiate<GameObject>(deathFX, this.transform.position, Quaternion.identity, this.GetComponentInParent<Tile>().transform);
        }
        if (clip != null)
        {
          SoundController.GetComponent<AudioSource>().PlayOneShot(clip);
        }
        if (hasLeader == LeaderState.necromancer)
        {
          //spawn zombie
          Tile parentTile = this.GetComponentInParent<Tile>();
          if (parentTile == null)
          {
              Debug.Log("ERROR");
          }
          parentTile.CreateUnit<Zombie>(PlayerController.Instance.zombie);
        }
        if (this.health <= 0)
        {
            this.UnitDie();
        }
    }
}
