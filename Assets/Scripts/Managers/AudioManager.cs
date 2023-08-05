using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip comboClip;
    [SerializeField]
    private List<AudioClip> comboFailClips;

    #region Events
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPlaySound, OnPlaySound);
        EventManager.AddHandler(GameEvent.OnStartNewLevel, OnStartNewLevel);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPlaySound, OnPlaySound);
        EventManager.RemoveHandler(GameEvent.OnStartNewLevel, OnStartNewLevel);
    }
    #endregion
    private void OnStartNewLevel()
    {
        audioSource.pitch = 1;
    }
    #region EventsMethods
    private void OnPlaySound(object clipName)
    {
        string clip = (string)clipName;
        switch (clip)
        {
            case "combo":
                audioSource.pitch += 0.1f;
                audioSource.clip = comboClip;
                audioSource.Play();
                break;

            case "combofail":
                audioSource.pitch = 1;
                audioSource.clip = (comboFailClips[UnityEngine.Random.Range(0,comboFailClips.Count)]);
                audioSource.Play();
                break;
        }
    }
    #endregion

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
