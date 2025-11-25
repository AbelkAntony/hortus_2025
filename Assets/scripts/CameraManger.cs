using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManger : MonoBehaviour
{

    public static CameraManger Instance { get; private set; }
    public CinemachineCamera Camera;
    public float ShakePower;
    public float ShakeTime;
    private WaitForSeconds shakeWait;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
    }

    private void Start()
    {
        shakeWait = new WaitForSeconds(ShakeTime);
    }
    internal void CameraHeight(Transform transform)
    {
        Camera.Target.TrackingTarget = transform;
    }

    internal void ShakeCamera()
    {
        StartCoroutine(Shake());     
    }

    private IEnumerator Shake()
    {
        Camera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = ShakePower;
        yield return shakeWait;
        Camera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = 0;
    }

}
