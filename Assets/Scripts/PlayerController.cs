using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public enum UserStates
    {
        leader,
        cultist,
        fourthSpell,
        fifthSpell,
        knight
    }
    public UserStates curUserState;


    public Cultist cultist;
    public Knight knight;
    public BaseUnit inquisition;
    public BaseUnit necromancer;
    public BaseUnit villager;
    public Victim victim;
    public BaseUnit anythingElseIForgot;

    public float updateTime;

    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }
    public Image SpellButton1Image;
    public Image SpellButton2Image;
    public Image SpellButton3Image;
    public Image SpellButton4Image;
    public Image SpellButton5Image;
    public MainMenu VolumeControl;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVec = new Vector3();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveVec.y += moveSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveVec.y -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x += moveSpeed;
        }
        this.transform.position += moveVec;

        /*
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("Click!");
            if (Physics.Raycast(ray, out hit))
            {
                // the object identified by hit.transform was clicked
                // do whatever you want
                Debug.Log(hit.transform.gameObject);
                Tile clickedTile = hit.transform.gameObject.GetComponent<Tile>();
                if(clickedTile != null)
                {
                    Debug.Log("Tile clicked using raycast");
                }
            }
        }*/
    }

    public void TileClicked(Tile tile)
    {
        //Depending on your mode it will do different stuff.
        if (curUserState == UserStates.cultist)
        {
            //Okay!
            Cultist tileCultist = tile.GetComponentInChildren<Cultist>();
            if (tileCultist != null)
            {
                tileCultist.health += 1;
            }
            else
            {
                tileCultist = tile.CreateUnit<Cultist>(cultist);
            }

        }
        else if (curUserState == UserStates.knight)
        {
            Knight tileKnight = tile.GetComponentInChildren<Knight>();
            if (tileKnight != null)
            {
                tileKnight.health += 1;
            }
            else
            {
                tileKnight = tile.CreateUnit<Knight>(knight);
            }
        }
        else if(curUserState == UserStates.leader)
        {
            Cultist tileCultist = tile.GetComponentInChildren<Cultist>();
            if(tileCultist)
            {
                Debug.Log("UPdating LEADER");
                tileCultist.UpdateLeader();
            }
            else
            {
                //Auto add cultist?  I think no, but maybe?

                //curUserState = UserStates.cultist;
                //TileClicked(tile);
                //curUserState = UserStates.leader;
            }
        }
    }

    public void Spell1Clicked()
    {
        Debug.Log("Leader mode turned on");
        curUserState = UserStates.leader;
    }
    public void Spell2Clicked()
    {
        curUserState = UserStates.cultist;
    }
    public void Spell3Clicked()
    {

    }
    public void Spell4Clicked()
    {

    }
    public void Spell5Clicked()
    {

    }
    public void knightdebugClicked()
    {
        if (curUserState == UserStates.cultist)
        {
            curUserState = UserStates.knight;
        }
        else if (curUserState == UserStates.knight)
        {
            curUserState = UserStates.cultist;
        }
    }


}
