﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingFloor : MonoBehaviour
{
    [SerializeField]
    float angle = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, angle));
    }
}
