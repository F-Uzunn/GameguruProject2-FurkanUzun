using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameData gamedata;
    public GameObject failPanel;
    public GameObject winPanel;
    public GameObject gamePanel;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public int scoreIndex;
    public int levelIndex;
    #region Events
    void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
        EventManager.AddHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.AddHandler(GameEvent.OnScore, OnScore);
    }

    void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnCreateNewLevel, OnCreateNewLevel);
        EventManager.RemoveHandler(GameEvent.OnPassFinishLine, OnPassFinishLine);
        EventManager.RemoveHandler(GameEvent.OnScore, OnScore);
    }
    #endregion

    #region EventMethods

    private void Start()
    {
        scoreIndex = 0;
        UpdateTexts();
    }
    private void OnPassFinishLine()
    {
        gamePanel.SetActive(false);
        Scale(winPanel, 1);
    }

    private void OnCreateNewLevel()
    {
        gamedata.levelIndex++;
        scoreIndex = 0;
        UpdateTexts();
        gamePanel.SetActive(true);
        Scale(winPanel, 0);
    }

    private void OnGameOver()
    {
        gamePanel.SetActive(false);
        Scale(failPanel, 1);
    }
    private void OnScore()
    {
        scoreIndex++;
        UpdateTexts();
    }
    #endregion


    #region Voids
    void Scale(GameObject panel,float val)
    {
        panel.transform.DOScale(Vector3.one * val, 0.1f);
    }

    void UpdateTexts()
    {
        scoreText.text = "Score : " + scoreIndex.ToString();
        levelText.text = "LEVEL " + gamedata.levelIndex.ToString();
    }
    public void Restart()
    {
        gamedata.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
