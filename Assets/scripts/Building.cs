using UnityEngine;
using UnityEngine.Rendering;

public class Building : MonoBehaviour
{
    public float FallSpeed;
    private bool isFalling = false;

    void Update()
    {
        if(isFalling)
            FallBuilding();
    }

    private void FallBuilding()
    {
        transform.position += FallSpeed * Time.deltaTime * Vector3.down;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Building") && isFalling)
        {
            
            //stop falling
            isFalling = false;
            CameraManger.Instance.ShakeCamera();
            Spawner.Instance.Hit();
        }
    }

    public void SetFalling()
    {
        isFalling = true;
    }
}
