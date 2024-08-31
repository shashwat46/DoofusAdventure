using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform doofus;  
    public float smoothTime = 0.3f;
    public float height = 5f;  
    public float distance = 12f;  
    public float angle = 30f;  

    private Vector3 _currentVelocity = Vector3.zero;

    void LateUpdate()
    {
        if (doofus == null) return;

       
        Vector3 targetPosition = doofus.position;
        targetPosition -= Quaternion.Euler(angle, 0, 0) * Vector3.forward * distance;
        targetPosition += Vector3.up * height;

        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

       
        transform.LookAt(doofus);
    }
}