﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTrailParticle : MonoBehaviour
{
    public float speed = 200f;

    private Vector3 axis = new Vector3(1,1,1);
    private float x;
    private float y;
    private float z;
    void Start()
    {
        
    }

    void Update()
    {
        x += Random.Range(-0.1f, 0.1f);
        y += Random.Range(-0.1f, 0.1f);
        z += Random.Range(-0.1f, 0.1f);
        if (x > 1) x = 1;
        if (x < -1) x = -1;
        if (y > 1) y = 1;
        if (y < -1) y = -1;
        if (z > 1) z = 1;
        if (z < -1) z = -1;

        axis = new Vector3(Mathf.Clamp(x,-1,1), Mathf.Clamp(y, -1, 1), Mathf.Clamp(z, -1, 1));
        transform.Rotate(axis * speed * Time.deltaTime);
    }
}
