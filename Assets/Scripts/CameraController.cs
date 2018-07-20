using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;
    public Transform camTransformer;

    private Camera cam;

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    private const float Y_ANGLE_MIN = 10.0f;    // Minimum camera angle allowed
    private const float Y_ANGLE_MAX = 50.0f;    //  Maximum camera angle allowed

    private void Start()
    {
        camTransformer = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");   //  Changing to -= inverts camera
        currentY -= Input.GetAxis("Mouse Y");   //  Changing to += inverts camera
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX); //  Restricts vertical camera movement
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransformer.position = lookAt.position + rotation * dir;
        camTransformer.LookAt(lookAt.position);
    }
}