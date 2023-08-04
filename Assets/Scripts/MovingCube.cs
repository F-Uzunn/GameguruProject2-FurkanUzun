using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }

    [SerializeField]
    private float moveSpeed = 1f;
    private void  OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("Base").GetComponent<MovingCube>();

        if (LastCube.gameObject == GetComponent<MovingCube>().gameObject)
            return;

        CurrentCube = this;

        Debug.Log(CurrentCube.gameObject.name);

        transform.localScale = new Vector3(LastCube.transform.localScale.x,LastCube.transform.localScale.y,transform.localScale.z);

    }
    internal void Stop()
    {
        moveSpeed = 0f;
        float leftOverMargin = transform.position.x - LastCube.transform.position.x;

        if(Mathf.Abs(leftOverMargin) >= LastCube.transform.localScale.x)
        {
            Debug.Log("gameOver");
            LastCube = null;
            CurrentCube = null;
            GameManager.Instance.isGameOver = true;
            this.gameObject.AddComponent<Rigidbody>();
            Destroy(this.gameObject, 1f);
            return;
        }

        float direction = leftOverMargin > 0 ? 1f:-1f;
        SplitCubeOnX(leftOverMargin,direction);

        LastCube = GetComponent<MovingCube>();
    }

    private void SplitCubeOnX(float leftOverMargin,float direction)
    {
        float newZSize = LastCube.transform.localScale.x - Mathf.Abs(leftOverMargin);
        float fallingBlockSize = transform.localScale.x - newZSize;

        float newZPosition = LastCube.transform.position.x + (leftOverMargin / 2);
        transform.localScale = new Vector3(newZSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newZPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newZSize / 2f* direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f* direction;

        SpawnFallingCube(fallingBlockZPosition, fallingBlockSize);
    }
    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * moveSpeed;

        if (LastCube != null)
        {
            float leftOverMargin = transform.position.x - LastCube.transform.position.x;
        }
    }

    private void SpawnFallingCube(float fallingBlockZPosisiton,float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
        cube.transform.position = new Vector3(fallingBlockZPosisiton, transform.position.y, transform.position.z);

        cube.AddComponent<Rigidbody>();
        Destroy(cube.gameObject, 1f);
    }
}
