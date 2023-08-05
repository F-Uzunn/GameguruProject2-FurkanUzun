using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public List<Transform> moveList;
    [SerializeField]
    private int moveIndex;
    [SerializeField]
    private float speed;

    #region Events
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnAddStackToMoveList, OnAddStackToMoveList);
        EventManager.AddHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.AddHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnAddStackToMoveList, OnAddStackToMoveList);
        EventManager.RemoveHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.RemoveHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
    }
    #endregion

    #region EventMethods
    void OnAddStackToMoveList(object cube)
    {
        GameObject cubeTrans = (GameObject)cube;
        moveList.Add(cubeTrans.transform);
    }

    void OnCreateNewLevel()
    {
        speed = 1f;
    }
    void OnPassFinishLine()
    {
        speed = 0;
        moveList.Clear();
        moveIndex = 0;
        transform.GetChild(0).DORotate(Vector3.zero, 0.1f);
        transform.DOMoveX(0, 0.1f);
    }
    #endregion
    void Update()
    {
        if (GameManager.Instance.isGameStarted)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            Vector3 direction = Vector3.zero;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.GetChild(0).localRotation = targetRotation;
        }

        if (moveList.Count == 0)
            return;

        if (GetDistance() < 2.75f)
        {
            if (moveList[moveIndex].GetComponent<MovingCube>() != MovingCube.CurrentCube)
            {
                transform.DOMoveX(moveList[moveIndex].transform.position.x, 0.25f).SetEase(Ease.Linear);
                if (moveList.Count != moveIndex+1)
                    moveIndex++;
            }
        }
    }
    float GetDistance()
    {
        if (moveList[moveIndex] != null)
        {
            float distance = Vector3.Distance(transform.position, moveList[moveIndex].position + new Vector3(0, 0, -1));
            return distance;
        }
        return 100;
    }
}
