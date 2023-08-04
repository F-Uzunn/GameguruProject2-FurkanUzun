using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : InstanceManager<GameManager>
{
    public bool isGameOver;
    public bool isLevelCompleted;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
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
                MovingCube.CurrentCube.Stop();

            if (isGameOver)
                return;

            EventManager.Broadcast(GameEvent.OnSpawnCube);
        }
    }
}
