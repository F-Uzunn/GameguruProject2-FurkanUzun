using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : InstanceManager<GameManager>
{
    public bool isGameOver;
    public bool isLevelCompleted;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            if (isGameOver)
                return;

            FindObjectOfType<CubeSpawner>().SpawnCube();
        }
    }
}
