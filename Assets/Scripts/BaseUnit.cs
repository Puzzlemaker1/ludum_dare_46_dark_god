using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class BaseUnit : MonoBehaviour
{
    private float timeSinceUpdate;
    public Sprite sprite1;
    public Sprite sprite2;
    public int health = 1;
    public GameObject deathFX;
    public Camera SoundController;
    public AudioClip clip;

    public void InitializeUnit(BaseUnit settings)
    {
        if (this.GetType() != settings.GetType())
        {
            Debug.LogError("Invalid unit initialization");
            return;
        }
        System.Type type = settings.GetType();
        System.Reflection.FieldInfo[] fields = type.GetFields();

        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(this, field.GetValue(settings));
        }
        Debug.Log("Unit initialized");

        SoundController = FindObjectOfType<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Unit started");
        //Let any children initlialization happen first
        UnitStart();

        this.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, sprite1);
        this.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(1, sprite2);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceUpdate += Time.deltaTime;
        if (timeSinceUpdate > PlayerController.Instance.updateTime)
        {
            timeSinceUpdate = 0.0f;

            UnitUpdate();
        }
        if (health < 0)
        {
            //Kill self!
            UnitDie();
            return;
        }

        int curParticleCount = this.GetComponent<ParticleSystem>().particleCount;
        if (curParticleCount < health)
        {
            this.GetComponent<ParticleSystem>().Emit(1);
        }
        else if (curParticleCount > health)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[curParticleCount];
            this.GetComponent<ParticleSystem>().GetParticles(particles);
            particles[0].remainingLifetime = -1;
            this.GetComponent<ParticleSystem>().SetParticles(particles);
        }
    }

    public List<T> LocateGridEntity<T>(int distance)
    {
        List<T> entities = new List<T>();
        GridManager grid = this.transform.root.GetComponent<GridManager>();
        for (int x = -1 * distance; x <= distance; x++)
        {
            for (int y = -1 * distance; y <= distance; y++)
            {
                Vector2Int coord = new Vector2Int(x, y) + this.GetComponentInParent<Tile>().coord;
                Tile tile = grid.getTile(coord);
                if (tile != null)
                {
                    Debug.Log("Checking tile " + coord);
                    Type entityType = typeof(T);
                    T entity;
                    if(entityType.IsSubclassOf(typeof(Tile)))
                    {
                        Debug.Log("Is a tile");
                        entity = tile.GetComponent<T>();
                    }
                    else if (entityType.IsSubclassOf(typeof(BaseUnit)))
                    {
                        
                        entity = tile.GetComponentInChildren<T>();
                        Debug.Log("Is a unit: " + entity);
                    }
                    else
                    {
                        //Hmm
                        Debug.LogError("Non valid location called!");
                        return entities;
                    }
                    if(entity != null)
                    {
                        entities.Add(entity);
                    }
                }
            }
        }
        return entities;
    }

    public List<T> LocateTiles<T>() where T : Tile
    {
        List<T> tiles = new List<T>();
        GridManager grid = this.transform.root.GetComponent<GridManager>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = 01; y <= 1; y++)
            {
                Vector2Int coord = new Vector2Int(x, y);
                if (this.transform.root.GetComponent<GridManager>().IsValidTile(coord))
                {
                    Tile tile = grid.getTile(coord);
                    if (tile is T)
                    {
                        tiles.Add((T)tile);
                    }
                }
            }
        }
        return tiles;
    }

    public void MoveUnit(Vector2Int dir)
    {
        if (dir.magnitude > 1)
        {
            //Extra move?
            //Debug.LogError("Too much movement?");
        }
        Tile tile = this.GetComponentInParent<Tile>();
        GridManager grid = tile.GetComponentInParent<GridManager>();
        Vector2Int newCoord = tile.coord + dir;
        if(!this.transform.root.GetComponent<GridManager>().IsValidTile(newCoord))
        {
            //Invalid Move!
            //Should we return a false?
            return;
        }
        this.transform.parent = grid.getTile(newCoord).transform;
        this.transform.SetPositionAndRotation(this.transform.parent.position, this.transform.parent.rotation);
    }

    protected virtual void UnitDie()
    {
        Destroy(this.gameObject);
    }

    protected virtual void UnitStart()
    {

    }

    public void GainHealth(int healthDelta)
    {
        this.health += healthDelta;
        //More particles is handled in update, on purpose to allow for a slight delay between particles.
    }
    public void LoseHealth(int healthDelta)
    {
        Debug.Log("Health lost");
        this.health -= healthDelta;
        Instantiate<GameObject>(deathFX, this.transform.position, Quaternion.identity, this.GetComponentInParent<Tile>().transform);
        SoundController.GetComponent<AudioSource>().PlayOneShot(clip);
        if (this.health <= 0)
        {
            Debug.Log("Unit dead");
            this.UnitDie();
        }
    }

    protected virtual void UnitUpdate()
    {
        //Other classes should inherit from this
        Debug.Log("Update");
    }




}
