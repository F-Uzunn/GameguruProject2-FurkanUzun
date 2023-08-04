using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCube cubePrefab;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private MoveDirection moveDirection;
    private int posX;

    public List<Material> cubeMaterialList;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnSpawnCube,OnSpawnCube);
        EventManager.AddHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnSpawnCube,OnSpawnCube);
        EventManager.RemoveHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
    }
    public void OnSpawnCube()
    {
        var cube = Instantiate(cubePrefab, parent);
        cube.GetComponent<Renderer>().material = cubeMaterialList[Random.Range(0, cubeMaterialList.Count)];
        EventManager.Broadcast(GameEvent.OnAddStackToMoveList, cube.gameObject);

        if(MovingCube.LastCube != null)
        {
            posX = posX == -4 ? 4 : -4;
            cube.transform.position = new Vector3(posX, transform.position.y, MovingCube.LastCube.transform.position.z + cubePrefab.transform.localScale.z);
        }
        moveDirection = moveDirection == MoveDirection.plusX ? MoveDirection.minusX : MoveDirection.plusX;
        cube.MoveDirection = moveDirection;
    }

    private void OnCreateNewLevel()
    {
        for (int i = 2; i < transform.childCount; i++)
        {
            transform.GetChild(i).DOScale(Vector3.zero, 0.1f);
            Destroy(transform.GetChild(i).gameObject, 0.1f);
        }
        transform.DOScale(Vector3.zero, 0f);
        transform.DOMove(new Vector3(transform.position.x, transform.position.y, GameManager.Instance.finishObject.transform.parent.position.z + 2.39708f), 0.1f).SetDelay(0.1f).OnComplete(()=> 
        {
            transform.DOScale(Vector3.one, 0.1f);
        });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}
