using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OrbitAndLookAtPlayer : MonoBehaviour
{
    public Transform playerTransform; // Oyuncunun Transform bileşeni
    public float orbitSpeed = 1.0f; // Dönme hızı

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineOrbitalTransposer orbitalTransposer;

    private void Start()
    {
        // Sanal kamera nesnesini alın
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // OrbitalTransposer bileşenini alın
        orbitalTransposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

        if (playerTransform == null)
        {
            // Eğer playerTransform atanmadıysa, sahnede "Player" nesnesini bulun
            playerTransform = GameObject.Find("Player").transform;

            if (playerTransform == null)
            {
                Debug.LogError("PlayerTransform is not assigned and no 'Player' GameObject found in the scene!");
            }
        }
    }

    private void Update()
    {
        orbitalTransposer.m_XAxis.Value += Time.deltaTime * orbitalTransposer.m_XAxis.m_MaxSpeed;
    }
}
