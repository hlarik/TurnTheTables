﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class FindingNoteScene : MonoBehaviour
{
    public PlayableDirector pd;
    public QuizManagement qm;
    bool isStart = false;
    private GameObject cameraScript;

    private GameObject cinemachineParent;
    private GameObject cinemachine;

    private GameObject noteCanvas;
    private GameObject noteImage;
    bool noteIsShown = false;

    private Button IgnoreButton;
    private Button ReportButton;

    public RainController rain;
    private GameObject backgroundMusic;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            backgroundMusic.GetComponent<MusicController>().lowerMusicVolume();
            cinemachine.SetActive(true);
            pd.Play();
            isStart = true;

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GameObject.Find("BackgroundMusic");

        cameraScript = Camera.main.gameObject;
        pd = GetComponent<PlayableDirector>();

        noteCanvas = GameObject.Find("FirstSceneNoteCanvas");
        noteImage = GameObject.Find("Note");

        Transform[] children = noteCanvas.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children)
        {
            if (t.name == "Note")
            {
                noteImage = t.gameObject;
            }
        }


        cinemachineParent = GameObject.Find("CinemachineCamera");
        cinemachine = GameObject.Find("CM");

        Transform[] children1 = cinemachineParent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children1)
        {
            if (t.name == "CM")
            {
                cinemachine = t.gameObject;
            }
        }


    }

    void Update()
    {
        //Cutscene bitince
        if (pd.state != PlayState.Playing && isStart)
        {
            showNoteScreen();
            isStart = false;
            backgroundMusic.GetComponent<MusicController>().increaseMusicVolume();
        }

    }


    public void showNoteScreen()
    {
        noteImage.SetActive(true);
        noteIsShown = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        cameraScript.GetComponent<CameraController>().disableCameraMouse();
        GameObject.Find("Violet").GetComponent<PlayerController>().enabled = false;

        IgnoreButton = GameObject.Find("IgnoreButton").GetComponent<Button>();
        IgnoreButton.onClick.AddListener(IgnoreNote);

        ReportButton = GameObject.Find("ReportButton").GetComponent<Button>();
        ReportButton.onClick.AddListener(ReportToTeacher);

    }

    public void IgnoreNote()
    {
        rain.MakeItRain();
        closeNote();
    }

    public void ReportToTeacher()
    {
        closeNote();
    }

    public void closeNote()
    {
        noteImage.SetActive(false);

        cameraScript.GetComponent<CameraController>().enableCameraMouse();
        GameObject.Find("Violet").GetComponent<PlayerController>().enabled = true;

        Cursor.visible = false; //?


        Destroy(this); //?????

        noteIsShown = false;

        (GameObject.Find("Violet").GetComponent(typeof(Collider)) as Collider).isTrigger = false;
        (GameObject.Find("PaperParent")).SetActive(false);

        cinemachine.SetActive(false);
        qm.startQuiz();
    }





}
