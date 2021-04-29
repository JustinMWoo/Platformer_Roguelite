using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake _current;
    public static CameraShake Current { get { return _current; } }

    // Singleton
    private void Awake()
    {
        if (_current != null && _current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _current = this;
        }
    }

    private CinemachineVirtualCamera cinemachineCamera;
    private float timer;
    private CinemachineBasicMultiChannelPerlin perlin;

    private void Start()
    {
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        perlin = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineCamera.Follow = PlayerStats.Current.gameObject.transform;
    }

    public void ShakeCamera(float intensity, float time)
    {
        perlin.m_AmplitudeGain = intensity;
        timer = time;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                perlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
