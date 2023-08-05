using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource comboAudioSource;
    [SerializeField]
    private AudioSource gameAudioSource;
    [SerializeField]
    private AudioClip comboClip;
    [SerializeField]
    private AudioClip collectClip;
    [SerializeField]
    private List<AudioClip> comboFailClips;
    [SerializeField]
    private float lastPitchVal;

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
        comboAudioSource.pitch = 1;
    }
    #region EventsMethods
    private void OnPlaySound(object clipName)
    {
        string clip = (string)clipName;
        switch (clip)
        {
            case "combo":
                lastPitchVal += 0.1f;
                comboAudioSource.pitch = lastPitchVal;
                comboAudioSource.clip = comboClip;
                comboAudioSource.Play();
                break;

            case "combofail":
                lastPitchVal = 1;
                comboAudioSource.pitch = lastPitchVal;
                comboAudioSource.clip = (comboFailClips[UnityEngine.Random.Range(0,comboFailClips.Count)]);
                comboAudioSource.Play();
                break;
            case "collect":
                gameAudioSource.clip = collectClip;
                gameAudioSource.Play();
                break;
        }
    }
    #endregion

    void Start()
    {
        lastPitchVal = 1;
    }
}
