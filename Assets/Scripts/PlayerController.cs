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
        cultist_taskmaster,
        cultist,
        necromancer,
        succubus,
        knight,
        ghost
    }

    public UserStates curUserState;

    public GameObject charactorBase;
    public Cultist cultist;
    public BaseUnit necromancer;
    public BaseUnit villager;
    public Victim victim;
    public Zombie zombie;
    public Succubus succubus;

    public float updateTime = 1;

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

    public int cultistCost;
    public int necroCost;
    public int succCost;
    public int taskmasterCost;

    private float timeSinceUpdate;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        lifeSlider.maxValue = maxLife;
        manaSlider.maxValue = maxMana;

    }

    // Update is called once per frame
    void Update()
    {

      timeSinceUpdate += Time.deltaTime;
      if (timeSinceUpdate > updateTime)
      {
          timeSinceUpdate = 0.0f;

          WorldUpdate();
      }

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

        if (Input.GetKey(KeyCode.Alpha1))
        {
            Spell1Clicked();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Spell2Clicked();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Spell3Clicked();
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            Spell4Clicked();
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            Spell5Clicked();
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
    private void WorldUpdate()
    {
      SetLife(life-1);
      if (life <= 0 )
      {
        //gameover
      }
      if (life >= 1000 )
      {
        //gameover win!
      }
    }

    public void TileClicked(Tile tile)
    {
        //Depending on your mode it will do different stuff.
        if (curUserState == UserStates.cultist)
        {
            //Okay!

            if (mana >= cultistCost)
            {
                DeltaMana(-cultistCost);
            }
            else
            {
                NotEnoughMana();
                return;
            }

            Cultist tileCultist = tile.CreateOrBoostUnit<Cultist>(cultist, 1);

        }
        else if (curUserState == UserStates.knight)
        {
            //Was used for debugging
        }
        else if (curUserState == UserStates.cultist_taskmaster)
        {

            Cultist tileCultist = tile.GetComponentInChildren<Cultist>();

            if (!tileCultist)
            {
                curUserState = UserStates.cultist;
                //SUUUPER HACKY
                TileClicked(tile);
                curUserState = UserStates.cultist_taskmaster;
                return;
            }
            if (tileCultist.curLeaderState == Cultist.LeaderState.none)
            {
                if (mana >= taskmasterCost)
                {
                    DeltaMana(-taskmasterCost);
                }
                else
                {
                    NotEnoughMana();
                    return;
                }
            }
            Debug.Log("Updating LEADER");
            tileCultist.UpdateLeader(Cultist.LeaderState.taskmaster);

        }
        else if (curUserState == UserStates.necromancer)
        {
            Cultist tileCultist = tile.GetComponentInChildren<Cultist>();
            if (tileCultist)
            {
                if (tileCultist.curLeaderState != Cultist.LeaderState.necromancer)
                {
                    if (mana >= necroCost)
                    {
                        DeltaMana(-necroCost);
                    }
                    else
                    {
                        NotEnoughMana();
                        return;
                    }
                    Debug.Log("Updating NECROMANCER");
                    tileCultist.UpdateLeader(Cultist.LeaderState.necromancer);
                }
            }


        }
        else if (curUserState == UserStates.succubus)
        {
            if (mana >= necroCost)
            {
                DeltaMana(-necroCost);
            }
            else
            {
                NotEnoughMana();
                return;
            }
            Succubus tileSuccubus = tile.CreateUnit<Succubus>(succubus);
        }
        else if(curUserState == UserStates.ghost)
        {

        }
    }

    protected void NotEnoughMana()
    {

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
        curUserState = UserStates.cultist_taskmaster;
        unhighlight();
        SpellButton4.sprite = SpellButton4ImageHighlight;
    }
    public void Spell5Clicked()
    {
      curUserState = UserStates.ghost;
      unhighlight();
      SpellButton5.sprite = SpellButton5ImageHighlight;
    }
    public void unhighlight()
    {
      SpellButton1.sprite = SpellButton1Image;
      SpellButton2.sprite = SpellButton2Image;
      SpellButton3.sprite = SpellButton3Image;
      SpellButton4.sprite = SpellButton4Image;
      SpellButton5.sprite = SpellButton5Image;
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
            if (manaDelta > 0)
            {
                SetLife(life + (manaDelta / 10));
            }
        }
        else
        {
            if (manaDelta > 0)
            {
                SetLife(life + (manaDelta / 2));
                maxMana = maxMana + manaDelta / 10;
            }
            SetMana(maxMana);
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
