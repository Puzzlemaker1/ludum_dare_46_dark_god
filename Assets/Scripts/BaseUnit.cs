using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class BaseUnit : MonoBehaviour
{
    
    public float updateTime = 1;
    public Sprite sprite1;
    public Sprite sprite2;
    public int health = 1;
    public int maxHealth = 5;
    public GameObject deathFX;
    public Camera SoundController;
    public AudioClip clip;
    public float maxMove = 1;

    protected Tile hometile;

    private float timeSinceUpdate;


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

        SoundController = FindObjectOfType<Camera>();

        hometile = this.GetComponentInParent<Tile>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Let any children initlialization happen first
        UnitStart();

        this.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, sprite1);
        this.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(1, sprite2);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceUpdate += Time.deltaTime;
        if (timeSinceUpdate > updateTime)
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

    public List<Tuple<T, float>> LocateGridEntity<T>(int distance)
    {
        List<Tuple<T, float>> entities = new List<Tuple<T, float>>();
        GridManager grid = this.transform.root.GetComponent<GridManager>();
        for (int x = -1 * distance; x <= distance; x++)
        {
            for (int y = -1 * distance; y <= distance; y++)
            {
                Vector2Int coord = new Vector2Int(x, y) + this.GetComponentInParent<Tile>().coord;
                Tile tile = grid.getTile(coord);
                if (tile != null)
                {
                    Type entityType = typeof(T);
                    T entity;
                    if(entityType.IsSubclassOf(typeof(Tile)))
                    {
                        entity = tile.GetComponent<T>();
                    }
                    else if (entityType.IsSubclassOf(typeof(BaseUnit)))
                    {
                        entity = tile.GetComponentInChildren<T>();
                    }
                    else
                    {
                        //Hmm
                        Debug.LogError("Non valid location called!");
                        return entities;
                    }
                    if(entity != null)
                    {
                        float entityDistance = (tile.coord - this.GetComponentInParent<Tile>().coord).magnitude;
                        entities.Add(new Tuple<T, float>(entity, entityDistance));
                    }
                }
            }
        }
        return entities;
    }

    public T LocateClosestGridEntity<T>()
    {
        //HACKY HACK
        return LocateClosestGridEntity<T>(100);
    }

    public T LocateClosestGridEntity<T>(int distance)
    {
        List<Tuple<T, float>> entities = LocateGridEntity<T>(distance);
        T closest = default(T);
        float minMag = Mathf.Infinity;
        for(int i = 0; i < entities.Count; i++)
        {
            Type entityType = typeof(T);
            if(entities[i].Item2 < minMag)
            {
                minMag = entities[i].Item2;
                closest = entities[i].Item1;
            }
        }
        if(minMag != Mathf.Infinity)
        {
            return closest;
        }
        return default(T);
    }

    public void MoveUnit(Vector2Int dir)
    {
        if(dir.magnitude == 0)
        {
            //Welp
            Debug.Log("Move with a mag of 0");
            return;
        }
        Vector2 newDir = new Vector2();
        Vector2Int newIntDir = new Vector2Int();
        //Set a floating point vector to equal the movement direction
        newDir.Set(dir.x, dir.y);
        //Reduce the size of the vector to the max move speed.
        newDir *= (maxMove / newDir.magnitude);
        //Round it back to an integer
        newIntDir.Set((int)Math.Floor(newDir.x), (int)Math.Floor(newDir.y));
        if (newIntDir.magnitude == 0)
        {
            //Rounded down to nothing!
            if (dir.x != 0)
            {
                newIntDir.x = Math.Sign(dir.x);
            }
            else
            {
                newIntDir.y = Math.Sign(dir.y);
            }
        }
        dir = newIntDir;

        if(dir.magnitude == 0)
        {
            Debug.LogError("AHHHH");
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

    public void MoveUnitRandom()
    {
        Vector2Int moveVec = new Vector2Int(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-2, 3));
        MoveUnit(moveVec);
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
        if(this.health > maxHealth)
        {
            this.health = maxHealth;
        }
        //More particles is handled in update, on purpose to allow for a slight delay between particles.
    }
    public virtual void LoseHealth(int healthDelta)
    {
        this.health -= healthDelta;
        if (deathFX != null)
        {
           Instantiate<GameObject>(deathFX, this.transform.position, Quaternion.identity, this.GetComponentInParent<Tile>().transform);
        }
        if (clip != null)
        {
          SoundController.GetComponent<AudioSource>().PlayOneShot(clip);
        }
        if (this.health <= 0)
        {
            this.UnitDie();
        }
    }

    protected virtual void UnitUpdate()
    {
        //Other classes should inherit from this
    }




}
