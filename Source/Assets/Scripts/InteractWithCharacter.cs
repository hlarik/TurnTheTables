using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using System;

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
    bool mainPlayerTurningTowardsNPC = false;
    bool ePressed = false;
    bool isContacting = false;

    //Burda artik cok caresisim 
    bool hasReachedAtLast = false;
    bool VDwasActive = false;
    float rotSpeed = 3f;

    /// <summary>
    Vector3 delta; //For NPC
    Vector3 delta_mainCharacter; //For maincharacter
    /// </summary>
    /// 

    void Start()
    {
        collision = false;
        uiObject.SetActive(false);
    }

    void Update()
    {
        if (this.tag != "Player")
        {
            if (Input.GetKey(KeyCode.E) && collision)
            {
                EPressed();
            }
            //Don't stop turning until chaarcter is totally faced
            if (turningTowardsMainPlayer)
            {
                transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(delta), rotSpeed * Time.deltaTime);// ref turnSmoothVelocity, turnSmoothTime);
                //if (transform.rotation == Quaternion.LookRotation(delta) || transform.rotation.Equals(Quaternion.LookRotation(delta)))
                if(Vector3.Distance(transform.eulerAngles, Quaternion.LookRotation(delta).eulerAngles) <= 0.01f)
                {
                    turningTowardsMainPlayer = false;
                    hasReachedAtLast = true;
                    ePressed = false;
                }
            }

        }
        if((this.tag == "Ms. Susan" || this.tag == "Mr. Noah") &&  this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle") && !this.GetComponent<Animator>().IsInTransition(0) && ePressed)
        {
            if (ePressed)
            {
                Interact();
                ePressed = false;
            }
            turningTowardsMainPlayer = true;
            delta = new Vector3(mainPlayer.transform.position.x - this.transform.position.x, 0.0f, mainPlayer.transform.position.z - this.transform.position.z);
            //transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, Quaternion.LookRotation(delta).eulerAngles, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(delta), rotSpeed * Time.deltaTime);
        }

        if (hasReachedAtLast && !VD.isActive && VDwasActive)
        {
            if (this.tag == "Mr. Noah")
            {
                this.GetComponent<Animator>().SetBool("reportEnd", true);
                this.GetComponent<Animator>().SetBool("violetReporting", false);
                this.GetComponent<MaleTeacher_VioletReport>().turnToInitialRoattion();
            }
            else if (this.tag == "Ms. Susan")
            {
                //turningTowardsMainPlayer = false;
                GameObject.Find("MsSusan_Chair").GetComponent<Animator>().SetBool("SusanStand", false);
                GameObject.Find("MsSusan_Chair").GetComponent<Animator>().SetBool("SusanSit", true);
                this.GetComponent<Animator>().SetBool("reportEnd", true);
                this.GetComponent<Animator>().SetBool("violetReporting", false);
                this.GetComponent<MaleTeacher_VioletReport>().turnToInitialRoattion();
            }
            VDwasActive = false;
        }

        if (VD.isActive)
            VDwasActive = true;
    }

    public bool GetWhetherTurning()
    {
        return turningTowardsMainPlayer;
    }

    public void EPressed()
    {
        ePressed = true;
        hasReachedAtLast = false;
        //E is pressed so we could close the ui now
        if (this.tag != "Player")
        {
            //There are some special cases for the teachers
            if(this.tag == "Mr. Noah")
            {
                this.GetComponent<Animator>().SetBool("reportEnd", false);
                this.GetComponent<Animator>().SetBool("violetReporting", true);
            }
            else if (this.tag == "Ms. Susan")
            {
                GameObject.Find("MsSusan_Chair").GetComponent<Animator>().SetBool("SusanSit", false);
                GameObject.Find("MsSusan_Chair").GetComponent<Animator>().SetBool("SusanStand", true);
                this.GetComponent<Animator>().SetBool("reportEnd", false);
                this.GetComponent<Animator>().SetBool("violetReporting", true);
            }

            uiObject.SetActive(false);

            //Start dialogue with that character
            if(this.tag != "Ms. Susan" && this.tag != "Mr. Noah")
            { 
                Interact();
                turningTowardsMainPlayer = true;
                delta = new Vector3(mainPlayer.transform.position.x - this.transform.position.x, 0.0f, mainPlayer.transform.position.z - this.transform.position.z);
                transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(delta), rotSpeed * Time.deltaTime);
                //transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, Quaternion.LookRotation(delta).eulerAngles, ref turnSmoothVelocity, turnSmoothTime);
            }
        }
        if (!VD.isActive)
        {
            isContacting = false;
        }
        else
        {
            isContacting = true;
        }
    }

    public void Interact()
    {
        //Check if we vd is already assigned, burda bir sorun vaaaaar!!!! cunku zaten dialog varken tekrar cagiriyor
        if (!VD.isActive)
        {
            if (this.GetComponent<VIDE_Assign>() == null)
                Debug.Log("No dialogue assigned");
            else
                NPCDialogue(GetComponent<VIDE_Assign>());
        }
    }

    public void JannetOpenVDLastScene()
    {
        uiObject.SetActive(false);
        if (!VD.isActive)
        {
            if (GameObject.Find("Jannet").GetComponent<VIDE_Assign>() == null)
                Debug.Log("No dialogue assigned");
            else
            {
                //Debug.Log("!!!!!!!!!HERE!!!!!!!!!");
                NPCDialogue(GameObject.Find("Jannet").GetComponent<VIDE_Assign>());
                VD.SetNode(15);
            }
        }
    }

    public bool isInContact()
    {
        return isContacting;
    }

    void OnTriggerEnter(Collider other)
    {
        if (this.tag != "Player")
        {
            if (other.CompareTag("Player"))
            {
                collision = true;
                uiObject.SetActive(true);
            }
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
        if (this.tag != "Player")
        {
            collision = false;
            uiObject.SetActive(false);
        }
        /*if (other.CompareTag("Player"))
        {
            turningTowardsMainPlayer = false;
            if (this.tag == "Mr. Noah")
            {
                this.GetComponent<Animator>().SetBool("reportEnd", true);
                this.GetComponent<Animator>().SetBool("violetReporting", false);
                this.GetComponent<MaleTeacher_VioletReport>().turnToInitialRoattion();
            }
            else if (this.tag == "Ms. Susan")
            {
                //turningTowardsMainPlayer = false;
                GameObject.Find("MsSusan_Chair").GetComponent<Animator>().SetBool("SusanStand", false);
                GameObject.Find("MsSusan_Chair").GetComponent<Animator>().SetBool("SusanSit", true);
                this.GetComponent<Animator>().SetBool("reportEnd", true);
                this.GetComponent<Animator>().SetBool("violetReporting", false);
                this.GetComponent<MaleTeacher_VioletReport>().turnToInitialRoattion();
            }
        }*/
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///These will be specific for the user/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void InnerVoiceDialogue(int status)
    {
        if (this.tag == "Player")
        {
            //Check if we vd is already assigned, burda bir sorun vaaaaar!!!! cunku zaten dialog varken tekrar cagiriyor
            if (!VD.isActive)
            {
                if (this.GetComponent<VIDE_Assign>() == null)
                    Debug.Log("No dialogue assigned");
                else
                {
                    NPCDialogue(GetComponent<VIDE_Assign>());
                    VD.SetNode(status);
                }

            }
        }
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///These will be specific for the user/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
