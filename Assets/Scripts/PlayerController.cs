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
    public Image SpellButton1;
    public Sprite SpellButton1Image;
    public Sprite SpellButton1ImageHighlight;
    public Image SpellButton2;
    public Sprite SpellButton2Image;
    public Sprite SpellButton2ImageHighlight;
    public Image SpellButton3;
    public Sprite SpellButton3Image;
    public Sprite SpellButton3ImageHighlight;
    public Image SpellButton4;
    public Sprite SpellButton4Image;
    public Sprite SpellButton4ImageHighlight;
    public Image SpellButton5;
    public Sprite SpellButton5Image;
    public Sprite SpellButton5ImageHighlight;

    public MainMenu VolumeControl;

    public Text manaText;
    public Slider manaSlider;
    public int mana;
    public int maxMana;

    public Text lifeText;
    public Slider lifeSlider;
    public int life;
    public int maxLife;
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
        unhighlight();
        SpellButton1.sprite = SpellButton1ImageHighlight;
    }
    public void Spell2Clicked()
    {
        curUserState = UserStates.necromancer;
        unhighlight();
        SpellButton2.sprite = SpellButton2ImageHighlight;
    }
    public void Spell3Clicked()
    {
        curUserState = UserStates.succubus;
        unhighlight();
        SpellButton3.sprite = SpellButton3ImageHighlight;
    }
    public void Spell4Clicked()
    {
        curUserState = UserStates.leader;
        unhighlight();
        SpellButton4.sprite = SpellButton4ImageHighlight;
    }
    public void Spell5Clicked()
    {

      // unhighlight();
      // SpellButton5 = SpellButton5ImageHighlight;
    }
    public void unhighlight()
    {
      SpellButton1.sprite = SpellButton1Image;
      SpellButton2.sprite = SpellButton2Image;
      SpellButton3.sprite = SpellButton3Image;
      SpellButton4.sprite = SpellButton4Image;
      // SpellButton5 = SpellButton5Image;
    }


    public void knightdebugClicked()
    {
        curUserState = UserStates.knight;
    }

    public void DeltaMana(int manaDelta)
    {
        if (mana + manaDelta <= maxMana)
        {
          SetMana(mana + manaDelta);
          SetLife(life + 5);
        }
        else
        {
           maxMana = maxMana + 1;
           SetMana(maxMana);
           SetLife(life + 15);
        }
    }

    public void SetMana(int newMana)
    {
        mana = newMana;
        manaText.text = mana + "/" + maxMana;
        manaSlider.value = mana;
        manaSlider.maxValue = maxMana;
    }

    public void SetLife(int newLife)
    {
        life = newLife;
        lifeText.text = life + "/" + maxLife;
        lifeSlider.value = life;
        lifeSlider.maxValue = maxLife;
    }

    public int GetMana()
    {
        return mana;
    }

}
