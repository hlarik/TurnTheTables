using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    bool cameraTurn = true;

    float yaw;
    float pitch;

    Vector3 center = new Vector3(0f, 0.5f, 0f);
    RaycastHit hit;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        Camera.main.nearClipPlane = 0.01f;
    }

    void LateUpdate()
    {
        if (cameraTurn) {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;
        }

        Vector3 wantedCameraPosition = target.position - transform.forward * dstFromTarget;

        // Raycasting 
        Vector3 rayDirection = (wantedCameraPosition - (target.position + center)).normalized;

        if (Physics.Raycast(target.position + center, rayDirection, out hit, dstFromTarget)
            && hit.transform != target.parent) // ignore ray-casts that hit the user. DR
        {
            // Debug.Log(hit.transform.name  + " " + direction.ToString());
            wantedCameraPosition.x = hit.point.x;
            wantedCameraPosition.z = hit.point.z;
            //wantedPosition.y = wantedPosition.y);
        }

        transform.position = wantedCameraPosition;

    }

    //To disable camera Turn in cutscenes and dialogues
    public void diableCameraMouse()
    {
        cameraTurn = false;
    }

    public void enableCameraMouse()
    {
        cameraTurn = true;
    }

}