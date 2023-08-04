using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    public float shakeTimer;
    // Start is called before the first frame update

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPerfectTiming, OnPerfectTiming);
    }

    private void OnDisable()
    {
        EventManager.AddHandler(GameEvent.OnPerfectTiming, OnPerfectTiming);
    }
 

    //Camera shake voidi
    public void OnPerfectTiming(object intensityVal, object timeVal)
    {
        float intensity = (float)intensityVal;
        float time = (float)timeVal;

        cinemachineBasicMultiChannelPerlin =
                     cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                cinemachineBasicMultiChannelPerlin =
                     cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}