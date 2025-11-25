using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManger : MonoBehaviour
{

    public CinemachineCamera Camera;
    public float ShakePower;
    public float ShakeTime;
    private WaitForSeconds shakeWait;
    ////private  float smoothSpeed = 1f;
    //public Vector3 target;       // Object to follow (e.g., Player)
    //public Vector3 offset;         // Distance from the target
    //public float smoothSpeed = 1f; // Smoothness factor
    //public void CameraHeight(float y)
    //{
    //    //this.transform.position = new Vector3(this.transform.position.x,y-2, this.transform.position.z);
    //    Vector3 newLocation = new Vector3(0, y - 2, -10);
    //    target = newLocation;
    //}

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

    //void LateUpdate()
    //{
    //    // Desired position
    //    Vector3 desiredPosition = target;

    //    // Smoothly interpolate between current and desired position
    //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime*smoothSpeed);

    //    // Apply position
    //    transform.position = smoothedPosition;

    //    // Optionally keep camera looking at the target
    //    transform.LookAt(target);
    //}

}
