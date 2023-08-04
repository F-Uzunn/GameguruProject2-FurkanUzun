using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCube cubePrefab;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private MoveDirection moveDirection;
    private int posX;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnSpawnCube,OnSpawnCube);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnSpawnCube,OnSpawnCube);
    }
    public void OnSpawnCube()
    {
        var cube = Instantiate(cubePrefab, parent);

        if(MovingCube.LastCube != null)
        {
            posX = posX == -4 ? 4 : -4;
            cube.transform.position = new Vector3(posX, transform.position.y, MovingCube.LastCube.transform.position.z + cubePrefab.transform.localScale.z);
        }
        moveDirection = moveDirection == MoveDirection.plusX ? MoveDirection.minusX : MoveDirection.plusX;
        cube.MoveDirection = moveDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}
