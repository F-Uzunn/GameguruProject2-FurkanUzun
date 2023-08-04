using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCube cubePrefab;
    [SerializeField]
    private Transform parent;

    public void SpawnCube()
    {
        var cube = Instantiate(cubePrefab,parent);

        if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("Base"))
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, MovingCube.LastCube.transform.position.z + cubePrefab.transform.localScale.z);
        else
            cube.transform.position = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}
