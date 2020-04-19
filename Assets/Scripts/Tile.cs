using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    private float timeSinceUpdate;
    public bool blocking;
    public Vector2Int coord;
    public float sacrificeMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceUpdate += Time.deltaTime;
        if (timeSinceUpdate > PlayerController.Instance.updateTime)
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

    public virtual void TileUpdate()
    {

    }

}
