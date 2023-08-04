using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : InstanceManager<GameManager>
{
    public GameObject finishPrefab;
    public GameObject finishObject;
    public bool isGameOver;
    public bool isLevelCompleted;
    public CameraState cameraState;

    public GameObject gameCam;
    public GameObject orbitCam;
    public enum CameraState
    {
        game,
        orbit
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.AddHandler(GameEvent.OnStartNewLevel, OnStartNewLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.RemoveHandler(GameEvent.OnStartNewLevel, OnStartNewLevel);
    }

    private void Awake()
    {
        finishObject = GameObject.FindGameObjectWithTag("FinishLine");
        cameraState = CameraState.game;
        SetActiveCam();
    }

    private void OnGameOver()
    {
        isGameOver = true;
    }

    private void OnStartNewLevel()
    {
        cameraState = CameraState.game;
        SetActiveCam();
    }


    private void OnPassFinishLine()
    {
        isLevelCompleted = true;
        cameraState = CameraState.orbit;
        SetActiveCam();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MovingCube.CurrentCube != null)
            {
                MovingCube.CurrentCube.Stop();
                if (CheckIfWeClosedToFinish())
                    return;
            }

            if (isLevelCompleted)
                return;

            if (isGameOver)
                return;

            EventManager.Broadcast(GameEvent.OnSpawnCube);

            CheckIfWeClosedToFinish();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLevelCompleted = false;
            GameObject finishObj = Instantiate(finishPrefab);
            finishObj.transform.position = finishObject.transform.parent.position;
            EventManager.Broadcast(GameEvent.OnStartNewLevel);
        }
    }

    public bool CheckIfWeClosedToFinish()
    {
        if (MovingCube.CurrentCube != null)
        {
            if (finishObject.transform.position.z - MovingCube.CurrentCube.transform.position.z < 3)
                return true;
        }
        return false;
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
