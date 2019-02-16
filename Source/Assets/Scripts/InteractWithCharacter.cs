﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class InteractWithCharacter : MonoBehaviour
{
    /// dialogue system
    public delegate void NPCEvent(VIDE_Assign dialogue);
    public static NPCEvent NPCDialogue;
    /// 

    public GameObject uiObject;
    public bool collision;
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
            EPressed();
        } 

        //Don't stop turning until chaarcter is totally faced
        if (turningTowardsMainPlayer)
        {
            transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, Quaternion.LookRotation(delta).eulerAngles, ref turnSmoothVelocity, turnSmoothTime);
            if (transform.rotation.eulerAngles == Quaternion.LookRotation(delta).eulerAngles)
            {
                turningTowardsMainPlayer = false;
            }
        }

        
    }

    public void EPressed()
    {
        //E is pressed so we could close the ui now
        uiObject.SetActive(false);

        //Start dialogue with that character
        Interact();

        turningTowardsMainPlayer = true;
        delta = new Vector3(mainPlayer.transform.position.x - this.transform.position.x, 0.0f, mainPlayer.transform.position.z - this.transform.position.z);
        transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, Quaternion.LookRotation(delta).eulerAngles, ref turnSmoothVelocity, turnSmoothTime);

    }

    public void Interact()
    {
        //Check if we vd is already assigned, burda bir sorun vaaaaar!!!! cunku zaten dialog varken tekrar cagiriyor
        if(!VD.isActive)
            NPCDialogue(GetComponent<VIDE_Assign>());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collision = true;
            uiObject.SetActive(true);
        }
    }

    //????????????
    /*public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collision = true;
        }
    }*/

    void OnTriggerExit(Collider other)
    {
        collision = false;
        uiObject.SetActive(false);
    }
}
