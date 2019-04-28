﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimatorController : MonoBehaviour
{
    //For rotating to the inital rotation
    Vector3 turnSmoothVelocity;
    float turnSmoothTime = 0.2f;

    InteractWithCharacter interactScript;
    Quaternion initialRotation;
    bool turningTowardsInitialRot = false;
    Quaternion initialRot;

    private void Start()
    {
        interactScript = this.GetComponent<InteractWithCharacter>();
        initialRotation = this.transform.rotation;
        initialRot = this.transform.rotation;

        this.GetComponent<Animator>().SetTrigger("talking1");
    }

    void Update()
    {
        if (turningTowardsInitialRot)
        {
            //Debug.Log(this.transform.rotation.eulerAngles + "\t\t");
            Debug.Log(this.tag + "|||||||||||||||||Turning");
            transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, initialRotation.eulerAngles, ref turnSmoothVelocity, turnSmoothTime);
            if (transform.rotation.eulerAngles == initialRotation.eulerAngles || transform.rotation.eulerAngles.Equals(initialRotation.eulerAngles))
            {
                turningTowardsInitialRot = false;
                this.GetComponent<Animator>().SetTrigger("talking1");
            }
        }
    }

    public void makeCharacterIdle()
    {
        this.GetComponent<Animator>().SetTrigger("isIdle");
    }

    public void makeCharacterShy()
    {
        this.GetComponent<Animator>().SetTrigger("isShy");
    }

    public void makeCharacterAngry()
    {
        this.GetComponent<Animator>().SetTrigger("isAngry");
    }

    public void TalkWithoutTurning()
    {
        this.GetComponent<Animator>().SetTrigger("talking1");
    }

    public void makeCharacterTalk()
    {
        makeCharacterIdle();
        turningTowardsInitialRot = true;
        transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, initialRotation.eulerAngles, ref turnSmoothVelocity, turnSmoothTime);
    }
}
