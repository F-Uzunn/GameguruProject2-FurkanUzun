using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource comboAudioSource;
    public AudioSource gameAudioSource;
    public AudioClip comboClip;
    public AudioClip collectClip;
    public AudioClip winClip;
    public AudioClip failClip;
    public List<AudioClip> comboFailClips;
    public float lastPitchVal;

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
            case "win":
                gameAudioSource.clip = winClip;
                gameAudioSource.Play();
                break;
            case "fail":
                gameAudioSource.clip = failClip;
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
