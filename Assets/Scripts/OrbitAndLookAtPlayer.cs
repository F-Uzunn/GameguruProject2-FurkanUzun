using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OrbitAndLookAtPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public float orbitSpeed = 1.0f;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineOrbitalTransposer orbitalTransposer;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        orbitalTransposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").transform;
        }
    }
    private void Update()
    {
        orbitalTransposer.m_XAxis.Value += Time.deltaTime * orbitalTransposer.m_XAxis.m_MaxSpeed;
    }
}
