using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public float updateTime = 1;
    public bool blocking;
    public Vector2Int coord;
    public float sacrificeMultiplier;

    private float timeSinceUpdate;

    // Start is called before the first frame update
    void Start()
    {
        TileStart();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceUpdate += Time.deltaTime;
        if (timeSinceUpdate > updateTime)
        {
            timeSinceUpdate = 0.0f;

            TileUpdate();
        }

    }

    public T CreateUnit<T>(T unitSettings) where T : BaseUnit
    {
        GameObject particles = Instantiate(PlayerController.Instance.charactorBase, this.transform);
        T newUnit = particles.AddComponent<T>();
        newUnit.InitializeUnit(unitSettings);
        return newUnit;
    }

    public T CreateOrBoostUnit<T>(T unitSettings) where T : BaseUnit
    {
        T unit = this.GetComponentInChildren<T>();
        if(unit == null)
        {
            unit = CreateUnit<T>(unitSettings);
        }
        else
        {
            unit.GainHealth(1);
        }
        return unit;
    }

    public void OnPointerClick(PointerEventData data)
    {
        //Lets just do OnMouseDown for all the tiles to check if we get a click.
        //It's simple, but dirty
        Debug.Log("Tile clicked");
        PlayerController.Instance.TileClicked(this);

    }

    public virtual void TileUpdate()
    {

    }

    public virtual void TileStart()
    {

    }

}
