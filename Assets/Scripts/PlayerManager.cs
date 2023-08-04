using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Animator _playerAnim;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.AddHandler(GameEvent.OnStartNewLevel, OnStartNewLevel);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.RemoveHandler(GameEvent.OnStartNewLevel, OnStartNewLevel);
    }
    void Start()
    {
        _playerAnim = transform.GetChild(0).GetComponent<Animator>();
    }

    void OnStartNewLevel()
    {
        AnimSet("run");
    }

    void OnPassFinishLine()
    {
        AnimSet("dance");
    }

    void AnimSet(string animString)
    {
        _playerAnim.SetTrigger(animString);
    }
}
