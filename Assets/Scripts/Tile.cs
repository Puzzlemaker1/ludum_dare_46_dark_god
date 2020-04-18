using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public bool blocking;
    public int xCoord;
    public int yCoord;

    public BaseUnit cultist;
    public BaseUnit knight;
    public BaseUnit inquisition;
    public BaseUnit necromancer;
    public BaseUnit civilian;
    public BaseUnit sacrifice;
    public BaseUnit anythingElseIForgot;


    private ArrayList curUnits;
    public enum TileTypeEnum
    {
        test1,
        test2,
        test3
    }

    TileTypeEnum type;

    // Start is called before the first frame update
    void Start()
    {
        type = (TileTypeEnum)Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddUnit()
    {

    }
    public void OnPointerClick(PointerEventData data)
    {
        //Lets just do OnMouseDown for all the tiles to check if we get a click.
        //It's simple, but dirty
        Debug.Log("Tile clicked");
        PlayerController.Instance.TileClicked(this);

    }

    private void AddUnit()
    {

    }


}
