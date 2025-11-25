using Unity.VisualScripting;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
    //private  float smoothSpeed = 1f;
    public Vector3 target;       // Object to follow (e.g., Player)
    public Vector3 offset;         // Distance from the target
    public float smoothSpeed = 1f; // Smoothness factor
    public void CameraHeight(float y)
    {
        //this.transform.position = new Vector3(this.transform.position.x,y-2, this.transform.position.z);
        Vector3 newLocation = new Vector3(0, y - 2, -10);
        target = newLocation;
    }


 

    void LateUpdate()
    {
        // Desired position
        Vector3 desiredPosition = target;

        // Smoothly interpolate between current and desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply position
        transform.position = smoothedPosition;

        // Optionally keep camera looking at the target
        transform.LookAt(target);
    }

}
