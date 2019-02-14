using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithCharacter : MonoBehaviour
{
    public GameObject uiObject;
    bool collision;
    public GameObject mainPlayer;

    Vector3 turnSmoothVelocity;
    float turnSmoothTime = 0.2f;

    private float m_Speed = 0.4f;
    bool turningTowardsMainPlayer = false;

    /// <summary>
    Vector3 delta;
    /// </summary>

    void Start()
    {
        collision = false;
        uiObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && collision)
        {
            //Start dialogue with that character

            //This immideiately changes rotation, not smooth
            /*Vector3 delta = new Vector3(mainPlayer.transform.position.x - this.transform.position.x, 0.0f, mainPlayer.transform.position.z - this.transform.position.z);
             * 
            
            this.transform.rotation = Quaternion.LookRotation(delta);*/

            //transform.LookAt(mainPlayer.transform);

            /*Vector3 lTargetDir = mainPlayer.transform.position - this.transform.position;
            lTargetDir.y = 0.0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * m_Speed);*/
            turningTowardsMainPlayer = true;
            delta = new Vector3(mainPlayer.transform.position.x - this.transform.position.x, 0.0f, mainPlayer.transform.position.z - this.transform.position.z);
            transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, Quaternion.LookRotation(delta).eulerAngles, ref turnSmoothVelocity, turnSmoothTime);
        }

        //Don't stop turning until chaarcter is totally faced
        if (turningTowardsMainPlayer)
        {
            transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, Quaternion.LookRotation(delta).eulerAngles, ref turnSmoothVelocity, turnSmoothTime);
        }

        if(transform.rotation.eulerAngles == Quaternion.LookRotation(delta).eulerAngles)
        {
            turningTowardsMainPlayer = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collision = true;
            uiObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        collision = false;
        uiObject.SetActive(false);
    }
}
