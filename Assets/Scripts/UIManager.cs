using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        winPanel.SetActive(true);
    }

    private void OnCreateNewLevel()
    {
        winPanel.SetActive(false);
    }

    private void OnGameOver()
    {
        failPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
