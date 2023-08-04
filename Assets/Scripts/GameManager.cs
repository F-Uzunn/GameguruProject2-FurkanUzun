using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : InstanceManager<GameManager>
{
    public GameObject finishPrefab;
    public GameObject finishObject;
    public bool isGameStarted;
    public bool isGameOver;
    public bool isLevelCompleted;
    public CameraState cameraState;

    public GameObject gameCam;
    public GameObject orbitCam;

    public int levelIndex;
    public enum CameraState
    {
        game,
        orbit
    }

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
        isGameOver = true;
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
            if (isGameStarted == false)
            {
                isGameStarted = true;
                EventManager.Broadcast(GameEvent.OnSpawnCube);
                EventManager.Broadcast(GameEvent.OnStartNewLevel);
                return;
            }

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
            isGameStarted = false;
            isLevelCompleted = false;
            GameObject fakeFinishObj = Instantiate(finishPrefab);
            fakeFinishObj.transform.position = finishObject.transform.parent.position;

            EventManager.Broadcast(GameEvent.OnCreateNewLevel);
            finishObject.transform.parent.localPosition = new Vector3(finishObject.transform.parent.localPosition.x, finishObject.transform.parent.localPosition.y, finishObject.transform.parent.localPosition.z + (3 * levelIndex * UnityEngine.Random.Range(1, 4)));

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
