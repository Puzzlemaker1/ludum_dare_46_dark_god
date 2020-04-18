﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    private float timeSinceUpdate;
    public Sprite sprite1;
    public Sprite sprite2;
    public int health = 1;

    public void InitializeUnit(BaseUnit settings)
    {
        if(this.GetType() != settings.GetType())
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
        if(health < 0)
        {
            //Kill self!
            Destroy(this);
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

    protected virtual void UnitStart()
    {

    }

    protected virtual void UnitUpdate()
    {
        //Other classes should inherit from this
        Debug.Log("Update");
    }



    
}
