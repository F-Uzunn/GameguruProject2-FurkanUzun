using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<Transform> moveList;
    private int moveIndex;

    [SerializeField]
    private float speed;

    public bool moveForward;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnAddStackToMoveList, OnAddStackToMoveList);
        EventManager.AddHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.AddHandler(GameEvent.OnStartNewLevel, OnStartNewLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnAddStackToMoveList, OnAddStackToMoveList);
        EventManager.RemoveHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.RemoveHandler(GameEvent.OnStartNewLevel, OnStartNewLevel);
    }

    void OnAddStackToMoveList(object cube)
    {
        GameObject cubeTrans = (GameObject)cube;
        moveList.Add(cubeTrans.transform);
    }

    void OnStartNewLevel()
    {
        speed = 1f;
    }
    void OnPassFinishLine()
    {
        speed = 0;
        moveList.Clear();
        moveIndex = 0;
        moveForward = false;
    }

    void Update()
    {
        if (moveForward)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            return;
        }

        if (moveList.Count == 0)
            return;

        if (moveList[moveIndex] != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveList[moveIndex].transform.position.x, transform.position.y, moveList[moveIndex].transform.position.z), speed * Time.deltaTime);
        else
            moveForward = true;
        
        if (GetDistance() < 0.75f)
        {
            if (moveList.Count - 1 == moveIndex)
            {
                moveForward = true;
                return;
            }
            moveIndex++;
        }
    }
    float GetDistance()
    {
        if (moveList[moveIndex] != null)
        {
            float distance = Vector3.Distance(transform.position, moveList[moveIndex].position);
            return distance;
        }
        return 0;
    }
}
