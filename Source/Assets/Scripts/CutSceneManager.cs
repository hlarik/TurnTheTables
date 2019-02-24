﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    //[SerializeField]
    public GameObject timeline;
    PlayableDirector pd;
    Animator UIanimator;
    public GameObject bullied;
    public GameObject maincharacter;

    private GameObject cameraScript;

    bool playerMoveTowardTarget = false;
    bool empathize = false;
    Vector3 turnSmoothVelocity;
    float turnSmoothTime = 0.2f;

    //UI eleements
    public GameObject DecisionsCanvas;

    //Empathy Camera
    public Camera empathyCamera;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        cameraScript = Camera.main.gameObject;
        DecisionsCanvas.SetActive(false);
        UIanimator = DecisionsCanvas.transform.GetChild(0).gameObject.GetComponent<Animator>();
        bullied.gameObject.GetComponent<InteractWithCharacter>().EPressed();
        //empathyCamera = bullied.transform.Find("EmpathyCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pd != null)
        {
            //When cutscene is finished, pop up the decisions panel
            if (pd.state != PlayState.Playing)
            {
                //Destroy(this);
                //When dialogue starts diable camera movement and character movement
                cameraScript.GetComponent<CameraController>().disableCameraMouse();
                GameObject.Find("MainCharacter").GetComponent<PlayerController>().enabled = false;

                //Daha sonra yukarda disable ettiklerini acmayi unutma

                if (empathize)
                {
                    ChangeToMainCamera();
                    empathize = false;
                }

                DecisionsCanvas.SetActive(true);
                UIanimator.SetBool("isOpen", true);
            }
        }

        if (playerMoveTowardTarget)
        {
            maincharacter.gameObject.GetComponent<Animator>().SetFloat("speedPercent", 0.5f);

            // Move our position a step closer to the target.
            float step = maincharacter.gameObject.GetComponent<PlayerController>().walkSpeed * Time.deltaTime; // calculate distance to move
            maincharacter.transform.position = Vector3.MoveTowards(maincharacter.transform.position, bullied.transform.position, step);

            //rotation
            Vector3 delta = new Vector3(bullied.transform.position.x - maincharacter.transform.position.x, 0.0f, bullied.transform.position.z - maincharacter.transform.position.z);
            maincharacter.transform.eulerAngles = Vector3.SmoothDamp(maincharacter.transform.rotation.eulerAngles, Quaternion.LookRotation(delta).eulerAngles, ref turnSmoothVelocity, turnSmoothTime);

            //if enters trigger of target
            if (bullied.gameObject.GetComponent<InteractWithCharacter>().collision)
            {
                //Debug.Log("ashdksahd");
                bullied.gameObject.GetComponent<InteractWithCharacter>().EPressed();
                playerMoveTowardTarget = false;
                maincharacter.gameObject.GetComponent<Animator>().SetFloat("speedPercent", 0.0f);
            }
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        pd = timeline.GetComponent<PlayableDirector>();
        if (pd != null)
        {
            GameObject.Find("MainCharacter").GetComponent<PlayerController>().enabled = false;
            cameraScript.GetComponent<CameraController>().disableCameraMouse();
            pd.Play();
        }
    }

    public void TalkWithNPC()
    {
        //Close canvas
        UIanimator.SetBool("isOpen", false);
        pd = null;
        //float step = maincharacter.gameObject.GetComponent<PlayerController>().walkSpeed * Time.deltaTime;
        playerMoveTowardTarget = true;
    }

    public void Empathsize()
    {
        //Close canvas
        UIanimator.SetBool("isOpen", false);
        pd = null;
        pd = timeline.GetComponent<PlayableDirector>();
        if (pd != null)
        {
            GameObject.Find("MainCharacter").GetComponent<PlayerController>().enabled = false;
            cameraScript.GetComponent<CameraController>().disableCameraMouse();
            ChangeToEmpathyCamera();
            pd.Play();
            //GameObject.Find("SwitchCamera").GetComponent<SwitchCamera>().ChangeToEmpathy();
        }
        empathize = true;
        
        //Destroy(this);
    }

    public void Ignore()
    {
        //Close canvas
        UIanimator.SetBool("isOpen", false);
        pd = null;

        //Destroy(this);
    }

    public void ReportToAnAdult()
    {
        //Close canvas
        UIanimator.SetBool("isOpen", false);
        pd = null;

        //Destroy(this);
    }

    private void ChangeToEmpathyCamera()
    {
        empathyCamera.enabled = true;
        mainCamera.enabled = false;
    }

    private void ChangeToMainCamera()
    {
        mainCamera.enabled = true;
        empathyCamera.enabled = false;
    }


}
