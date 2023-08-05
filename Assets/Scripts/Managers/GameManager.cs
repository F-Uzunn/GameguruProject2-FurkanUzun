using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : InstanceManager<GameManager>
{
    public GameObject finishPrefab;
    public GameObject finishObject;
    public GameObject gameCam;
    public GameObject orbitCam;

    public bool isGameStarted;
    public bool isGameOver;
    public bool isLevelCompleted;
    public bool cantClick;

    public CameraState cameraState;

    public int levelIndex;
   
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.AddHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.RemoveHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
    }

    private void Awake()
    {
        finishObject = GameObject.FindGameObjectWithTag("FinishLine");
        cameraState = CameraState.game;
        SetActiveCam();
    }

    private void OnGameOver()
    {
        if (isGameOver == false)
        {
            gameCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = null;
            isGameOver = true;
        }
    }
    private void OnCreateNewLevel()
    {
        cameraState = CameraState.game;
        SetActiveCam();
    }

    private void OnPassFinishLine()
    {
        isLevelCompleted = true;
        cameraState = CameraState.orbit;
        levelIndex++;
        SetActiveCam();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (cantClick)
                return;

            if (isGameStarted == false)
            {
                isGameStarted = true;
                EventManager.Broadcast(GameEvent.OnSpawnCube);
                EventManager.Broadcast(GameEvent.OnStartNewLevel);
                return;
            }

            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();
        }
    }

    //next level buton voidi
    private void LevelBuild()
    {
        isGameStarted = false;
        cantClick = false;
        GameObject fakeFinishObj = Instantiate(finishPrefab);
        fakeFinishObj.transform.position = finishObject.transform.parent.position;

        EventManager.Broadcast(GameEvent.OnCreateNewLevel);
        finishObject.transform.parent.localPosition = new Vector3(finishObject.transform.parent.localPosition.x,finishObject.transform.parent.localPosition.y, finishObject.transform.parent.localPosition.z + (3 * levelIndex * UnityEngine.Random.Range(1, 4)));
    }

    public bool CheckIfWeClosedToFinish()
    {
        if (MovingCube.CurrentCube != null)
        {
            if (finishObject.transform.position.z - MovingCube.CurrentCube.transform.position.z < 3)
            {
                cantClick = true;
                return true;
            }
        }
        return false;
    }

    public bool CheckIfGameOver(float leftOverMargin)
    {
        if (Mathf.Abs(leftOverMargin) >= MovingCube.LastCube.transform.localScale.x)
        {
            GameOver();
            return true;
        }
        return false;
    }

    private void GameOver()
    {
        cantClick = true;
        MovingCube.CurrentCube.gameObject.AddComponent<Rigidbody>();
        Destroy(MovingCube.CurrentCube.gameObject, 1f);
        EventManager.Broadcast(GameEvent.OnPlaySound, "combofail");
    }

    void SetActiveCam()
    {
        switch (cameraState)
        {
            case CameraState.game:
                gameCam.SetActive(true);
                orbitCam.SetActive(false);
                break;
            case CameraState.orbit:
                gameCam.SetActive(false);
                orbitCam.SetActive(true);
                break;
            default:
                break;
        }
    }
}
