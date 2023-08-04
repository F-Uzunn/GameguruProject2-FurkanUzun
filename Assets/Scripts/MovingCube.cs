using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }

    [SerializeField]
    private float moveSpeed = 1f;
    private void  OnEnable()
    {
        CurrentCube = this;
    }
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    internal void Stop()
    {
        moveSpeed = 0f;
    }
}
