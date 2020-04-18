﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public enum UserStates
    {
        cultist,
        knight,
        thirdSpell,
        fourthSpell,
        fifthSpell
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
        if(curUserState == UserStates.cultist)
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
        else if(curUserState == UserStates.knight)
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
    }

        public void Spell1Clicked()
        {

        }
        public void Spell2Clicked()
        {
          if(curUserState == UserStates.cultist)
          {
            curUserState = UserStates.knight;
            SpellButton2Image.sprite = Resources.Load<Sprite>("../Sprites/Spell2Cultist");
          }
          else if(curUserState == UserStates.knight)
          {
            curUserState = UserStates.cultist;
            SpellButton2Image.sprite = Resources.Load<Sprite>("../Sprites/Spell2Knight");
          }
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


}
