﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class S_EffectJet : MonoBehaviour
{
    public ParticleSystem thuruster;

    private Vector2 curPos, lastPos;
    private float direction;
    private ParticleSystem[] particles;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        curPos = transform.position;

        
        particles = thuruster.GetComponentsInChildren<ParticleSystem>();        
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;

        direction = GetAngle(curPos, lastPos);

        Quaternion rotZ = Quaternion.AngleAxis(direction, Vector3.forward);

        foreach(var particle in particles)
        {
            particle.transform.rotation = rotZ;
        }

        lastPos = transform.position;
    }

    float GetAngle(Vector2 start ,Vector2 end)
    {
        Vector2 dt = end - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;
        return degree;
    }
}

