using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : InstanceManager<GameManager>
{
    public GameObject finishObject;
    public bool isGameOver;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void Awake()
    {
        finishObject = GameObject.FindGameObjectWithTag("FinishLine");
    }

    private void OnGameOver()
    {
        isGameOver = true;
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

            if (isGameOver)
                return;

            EventManager.Broadcast(GameEvent.OnSpawnCube);

            CheckIfWeClosedToFinish();
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
}
