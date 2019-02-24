using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpathyCameraControl : MonoBehaviour
{

    //public Transform target;
    public float dstFromTarget = 0.2f;

    Vector3 center = new Vector3(0f, 0.5f, 0f);
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        Vector3 wantedCameraPosition = transform.position;

        // Raycasting 
        /*Vector3 rayDirection = (wantedCameraPosition - (target.position + center)).normalized;

        if (Physics.Raycast(target.position + center, rayDirection, out hit, dstFromTarget)
            && hit.transform != target.parent) // ignore ray-casts that hit the user. DR
        {
            // Debug.Log(hit.transform.name  + " " + direction.ToString());
            wantedCameraPosition.x = hit.point.x;
            wantedCameraPosition.z = hit.point.z;
            //wantedPosition.y = wantedPosition.y);
        }

        transform.position = wantedCameraPosition;*/
    }
}
