using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Animator _playerAnim;

    #region Events
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.AddHandler(GameEvent.OnStartNewLevel, OnStartNewLevel);
        EventManager.AddHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.RemoveHandler(GameEvent.OnStartNewLevel, OnStartNewLevel);
        EventManager.RemoveHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
    }
    #endregion
    void Start()
    {
        _playerAnim = transform.GetChild(0).GetComponent<Animator>();
    }
    #region EventMethods
    void OnStartNewLevel()
    {
        AnimSet("run");
    }

    void OnCreateNewLevel()
    {
        AnimSet("idle");
    }

    void OnPassFinishLine()
    {
        AnimSet("dance");
    }
    #endregion

    void AnimSet(string animString)
    {
        _playerAnim.SetTrigger(animString);
    }

    private void Update()
    {
        if (transform.position.y < -2f)
            EventManager.Broadcast(GameEvent.OnGameOver);
    }
}
