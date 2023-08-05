using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject failPanel;
    public GameObject winPanel;
    void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
        EventManager.AddHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
    }

    void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
        EventManager.RemoveHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
    }

    private void OnPassFinishLine()
    {
        Scale(winPanel, 1);
    }

    private void OnCreateNewLevel()
    {
        Scale(winPanel, 0);
    }

    private void OnGameOver()
    {
        Scale(failPanel, 1);
    }

    void Scale(GameObject panel,float val)
    {
        panel.transform.DOScale(Vector3.one * val, 0.1f);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
