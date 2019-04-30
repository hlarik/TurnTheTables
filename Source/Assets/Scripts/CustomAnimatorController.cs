using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimatorController : MonoBehaviour
{
    //For rotating to the inital rotation
    Vector3 turnSmoothVelocity;
    float turnSmoothTime = 0.2f;
    float rotSpeed = 2f;

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
        bool AnotherTurningEvent = interactScript.GetWhetherTurning();
        if (turningTowardsInitialRot && !AnotherTurningEvent)
        {
            //Debug.Log(this.transform.rotation.eulerAngles + "\t\t");
            Debug.Log(this.tag + "|||||||||||||||||Turning");
            //transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, initialRotation.eulerAngles, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, initialRotation, rotSpeed * Time.deltaTime);
            //if (transform.rotation.eulerAngles == initialRotation.eulerAngles || transform.rotation.eulerAngles.Equals(initialRotation.eulerAngles))
            if(Vector3.Distance(transform.eulerAngles, initialRotation.eulerAngles) <= 0.01f)
            {
                turningTowardsInitialRot = false;
                this.GetComponent<Animator>().SetBool("isIdle", false);
                this.GetComponent<Animator>().SetTrigger("talking1");
            }
        }
    }

    public void makeCharacterIdle()
    {
        this.GetComponent<Animator>().SetBool("isIdle", true);
    }

    public void makeCharacterShy()
    {
        this.GetComponent<Animator>().SetBool("isIdle", false);
        this.GetComponent<Animator>().SetTrigger("isShy");
    }

    public void makeCharacterAngry()
    {
        this.GetComponent<Animator>().SetBool("isIdle", false);
        this.GetComponent<Animator>().SetTrigger("isAngry");
    }

    public void TalkWithoutTurning()
    {
        this.GetComponent<Animator>().SetBool("isIdle", false);
        this.GetComponent<Animator>().SetTrigger("talking1");
    }

    public void makeCharacterTalk()
    {
        //makeCharacterIdle();
        this.GetComponent<Animator>().SetBool("isIdle", true);
        turningTowardsInitialRot = true;
        transform.rotation = Quaternion.Slerp(this.transform.rotation, initialRotation, rotSpeed * Time.deltaTime);
    }
}
