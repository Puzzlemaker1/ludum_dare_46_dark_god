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
        necromancer,
        succubus,
        knight
    }

    public UserStates curUserState;

    public GameObject charactorBase;
    public Cultist cultist;
    public Knight knight;
    public Inquisitor inquisitor;
    public BaseUnit necromancer;
    public BaseUnit villager;
    public Victim victim;
    public Zombie zombie;
    public BaseUnit anythingElseIForgot;
    public Succubus succubus;

    public float updateTime;

    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }
    public Image SpellButton1Image;
    public Image SpellButton2Image;
    public Image SpellButton3Image;
    public Image SpellButton4Image;
    public Image SpellButton5Image;
    public MainMenu VolumeControl;

    public Text manaText;
    public Slider manaSlider;

    public int mana;
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

            if (mana >= 10)
            {
                DeltaMana(-10);
            }
            else
            {
                //Not enough mana thing here?
                return;
            }

            Cultist tileCultist = tile.CreateOrBoostUnit<Cultist>(cultist);

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
                if (tileCultist.hasLeader == Cultist.LeaderState.none)
                {
                    if (mana >= 20)
                    {
                        DeltaMana(-20);
                    }
                    else
                    {
                        //Not enough mana thing here?
                        return;
                    }
                }
                Debug.Log("Updating LEADER");
                tileCultist.UpdateLeader();
            }
        }
        else if(curUserState == UserStates.necromancer)
        {
          Cultist tileCultist = tile.GetComponentInChildren<Cultist>();
          if(tileCultist)
          {
              if (tileCultist.hasLeader != Cultist.LeaderState.necromancer)
              {
                if (mana >= 40)
                {
                    DeltaMana(-40);
                }
                else
                {
                    //Not enough mana thing here?
                    return;
                }
                Debug.Log("Updating NECROMANCER");
                tileCultist.UpdateNecromancer();
              }
          }


        }
        else if(curUserState == UserStates.succubus)
        {
            Succubus tileSuccubus = tile.CreateUnit<Succubus>(succubus);
        }
    }

    public void Spell1Clicked()
    {
        curUserState = UserStates.cultist;

    }
    public void Spell2Clicked()
    {
        curUserState = UserStates.necromancer;
    }
    public void Spell3Clicked()
    {
        curUserState = UserStates.succubus;
    }
    public void Spell4Clicked()
    {
        curUserState = UserStates.leader;
    }
    public void Spell5Clicked()
    {

    }
    public void knightdebugClicked()
    {
        curUserState = UserStates.knight;
    }

    public void DeltaMana(int manaDelta)
    {
        SetMana(mana + manaDelta);
    }

    public void SetMana(int newMana)
    {
        mana = newMana;
        manaText.text = mana + "/100";
        manaSlider.value = mana;
    }

    public int GetMana()
    {
        return mana;
    }

}
