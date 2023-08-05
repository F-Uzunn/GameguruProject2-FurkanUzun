using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    public float moveSpeed = 1f;
    public bool isFallingCube;

    private void Awake()
    {
        if (GameManager.Instance.isGameStarted == false)
        {
            LastCube = null;
            CurrentCube = null;
        }
    }

    #region Events
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);

        if (LastCube == null)
            LastCube = GameObject.Find("Base").GetComponent<MovingCube>();

        if (LastCube.gameObject == GetComponent<MovingCube>().gameObject)
            return;

        CurrentCube = this;

        transform.localScale = new Vector3(LastCube.transform.localScale.x, LastCube.transform.localScale.y, transform.localScale.z);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
    }
    #endregion

    #region EventMethods
    private void OnPassFinishLine()
    {
        LastCube = null;
        CurrentCube = null;
    }
    #endregion
    private void Update()
    {
        if (MoveDirection == MoveDirection.plusX)
            transform.position += -transform.right * Time.deltaTime * moveSpeed;
        else
            transform.position += transform.right * Time.deltaTime * moveSpeed;
    }

    #region Voids
    internal void Stop()
    {
        moveSpeed = 0f;
        float leftOverMargin = transform.position.x - LastCube.transform.position.x;
        float direction = leftOverMargin > 0 ? 1f : -1f;

        if (GameManager.Instance.CheckIfGameOver(leftOverMargin))
            return;

        SplitCubeOnX(leftOverMargin, direction);
        LastCube = GetComponent<MovingCube>();
        EventManager.Broadcast(GameEvent.OnScore);

        if(!GameManager.Instance.CheckIfWeClosedToFinish())
            EventManager.Broadcast(GameEvent.OnSpawnCube);

    }
    private void SplitCubeOnX(float leftOverMargin, float direction)
    {
        float newZSize = LastCube.transform.localScale.x - Mathf.Abs(leftOverMargin);
        float fallingBlockSize = transform.localScale.x - newZSize;

        float newZPosition = LastCube.transform.position.x + (leftOverMargin / 2);
        transform.localScale = new Vector3(newZSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newZPosition, transform.position.y, transform.position.z);

        if ((leftOverMargin <= 0.1f && leftOverMargin>=0f) || (leftOverMargin >=-0.1f && leftOverMargin<=0))
        {
            transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(LastCube.transform.position.x, transform.position.y, transform.position.z);
            EventManager.Broadcast(GameEvent.OnPerfectTiming,0.5f,0.3f);
            EventManager.Broadcast(GameEvent.OnPlaySound, "combo");
            return;
        }

        EventManager.Broadcast(GameEvent.OnPlaySound, "combofail");
        float cubeEdge = transform.position.x + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnFallingCube(fallingBlockZPosition, fallingBlockSize);
    }
    private void SpawnFallingCube(float fallingBlockZPosisiton, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
        cube.transform.position = new Vector3(fallingBlockZPosisiton, transform.position.y, transform.position.z);

        cube.AddComponent<Rigidbody>();
        cube.transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.OutCubic);
        Destroy(cube.gameObject, 1.5f);
    }
    #endregion
}
