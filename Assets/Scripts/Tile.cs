using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public bool blocking;
    public Vector2Int coord;

    public GameObject CharBase;

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

    public T CreateUnit<T>(T unitSettings) where T : BaseUnit
    {
        GameObject particles = Instantiate(CharBase, this.transform);
        T newUnit = particles.AddComponent<T>();
        newUnit.InitializeUnit(unitSettings);
        Debug.Log("Unit created!");
        return newUnit;
    }

    public void OnPointerClick(PointerEventData data)
    {
        //Lets just do OnMouseDown for all the tiles to check if we get a click.
        //It's simple, but dirty
        Debug.Log("Tile clicked");
        PlayerController.Instance.TileClicked(this);

    }


}
