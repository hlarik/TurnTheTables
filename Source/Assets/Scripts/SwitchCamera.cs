using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{

    public Camera mainCamera;
    public Camera empathyCamera;

    bool changeCameraToEmpathy;

    // Start is called before the first frame update
    void Start()
    {
        changeCameraToEmpathy = false;
        mainCamera.enabled = true;
        empathyCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ) {
            changeCameraToEmpathy = true;
        }

        if (changeCameraToEmpathy && mainCamera.enabled)
        {
            empathyCamera.enabled = true;
            mainCamera.enabled = false;
        }

    }

    public void ChangeToMain()
    {
        changeCameraToEmpathy = false;
    }

    public void ChangeToEmpathy()
    {
        changeCameraToEmpathy = true;
    }
}
