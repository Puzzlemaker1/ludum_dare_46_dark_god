using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : BaseUnit
{ 
    public Sprite leftLeader;
    public Sprite rightLeader;
    public Sprite upLeader;
    public Sprite downLeader;
    public Sprite necroSprite;

    public enum LeaderState
    {
        taskmaster,
        necromancer,
        none
    }

    //Make these getters and setters instead of this
    public LeaderState curLeaderState = LeaderState.none;
    public Vector2Int leaderDir = Vector2Int.up;

    public SpriteRenderer leaderSprite;
    //public SpriteRenderer necromancerSprite;

    public float sacrificeGatherRate;
    public float sacrificeGatherHealthBonus;
    private float curSacrificePoints;
    private SpriteRenderer curLeaderSprite = null;


    // Start is called before the first frame update
    override protected void UnitStart()
    {
        
        curSacrificePoints = 0;
    }


    protected override void UnitUpdate()
    {

        Tile parentTile = this.GetComponentInParent<Tile>();
        if (parentTile == null)
        {
            Debug.Log("ERROR");
        }
        if (parentTile is House && (parentTile as House).population != 0)
        {
            curSacrificePoints += sacrificeGatherRate + ((this.health - 1) * sacrificeGatherHealthBonus);

            if (curSacrificePoints >= 1)
            {
                parentTile.CreateUnit<Victim>(PlayerController.Instance.victim);
                (parentTile as House).population--;
                curSacrificePoints = 0;
            }
        }
    }

    public void UpdateLeader(LeaderState newState)
    {
        if(curLeaderSprite == null)
        {
            curLeaderSprite = Instantiate(leaderSprite);
            curLeaderSprite.enabled = false;
        }

        if(curLeaderState == LeaderState.none)
        {
            if(newState == LeaderState.necromancer)
            {
                curLeaderSprite.enabled = true;
                
                curLeaderState = LeaderState.necromancer;
                curLeaderSprite.sprite = necroSprite;
                curLeaderSprite.transform.position = this.transform.position;
                curLeaderSprite.transform.parent = this.GetComponentInParent<Tile>().transform;
            }
            else if(newState == LeaderState.taskmaster)
            {
                curLeaderSprite.enabled = true;
                curLeaderState = LeaderState.taskmaster;
                leaderDir = Vector2Int.up;
                //leaderSprite = upLeader;
                
                curLeaderSprite.sprite = upLeader;
                curLeaderSprite.transform.position = this.transform.position;
                curLeaderSprite.transform.parent = this.GetComponentInParent<Tile>().transform;
            }

        }
        else if (curLeaderState == LeaderState.taskmaster && newState == LeaderState.taskmaster)
        {
            Debug.Log("Updating Existing taskmaster");
            //Rotate in an infuriating fashion
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
        else if(newState != curLeaderState)
        {
            curLeaderState = LeaderState.none;
            curLeaderSprite.enabled = false;
            UpdateLeader(newState);
        }
    }

    protected override void UnitDie()
    {
        ClearLeader();

        base.UnitDie();
    }

    protected void ClearLeader()
    {
        if (curLeaderSprite != null)
        {
            Destroy(curLeaderSprite.gameObject);
        }
    }
    public override void LoseHealth(int healthDelta)
    {
        this.health -= healthDelta;
        if (deathFX != null)
        {
            Instantiate<GameObject>(deathFX, this.transform.position, Quaternion.identity, this.GetComponentInParent<Tile>().transform);
        }
        if (deathClip != null)
        {
            SoundController.GetComponent<AudioSource>().PlayOneShot(deathClip);
        }
        if (curLeaderState == LeaderState.necromancer)
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
