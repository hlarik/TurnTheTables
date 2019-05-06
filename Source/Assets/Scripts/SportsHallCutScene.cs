﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class SportsHallCutScene : MonoBehaviour
{
    public PlayableDirector pd;
    public Animator anim;
    private GameObject optionParentCanvas;
    private GameObject optionCanvas;

    // private CameraController cameraScript;
    private GameObject cameraScript;

    private GameObject cinemachineParent;
    private GameObject cinemachine;

    private Button IgnoreButton;
    private Button ReportButton;
    private Button ReactButton;

    public QuizManagement qm;

    public RainController rain;

    public ReactToMeanGirlCutScene reactCutScene;

    bool isStart = false;

    bool isQuizNotStarted = false;

    private GameObject backgroundMusic;
    BarManager bar;
    TaskManager task;

    private GameObject QuestionCanvas;
    private GameObject QPanel;

    bool isReportClicked = false;


    private GameObject exitTxt;  ///?????????
    private GameObject dialogTxt;

    GlobalController globalControllerScript; //???

    private GameObject frontCamera;


    // Start is called before the first frame update
    void Start()
    {
       // cameraScript = GameObject.Find("MainCamera").GetComponent<CameraController>();
        frontCamera = GameObject.Find("frontcamera");
        GameObject.Find("DialogPanelOfMeanGirl").SetActive(false);
        frontCamera.SetActive(false);
        exitTxt = GameObject.Find("Enter thru Door Canvas");
        dialogTxt = GameObject.Find("Interact with character canvas");
        exitTxt.SetActive(false);
        dialogTxt.SetActive(false);

        backgroundMusic = GameObject.Find("BackgroundMusic");
        bar = GameObject.Find("BarManager").GetComponent<BarManager>();
        task = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        pd = GetComponent<PlayableDirector>();

        cameraScript = Camera.main.gameObject;

        optionParentCanvas = GameObject.Find("SportsHallCutsceneCanvas");
        optionCanvas = GameObject.Find("SportsCutsceneOption");

        Transform[] children3 = optionParentCanvas.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children3)
        {
            if (t.name == "SportsCutsceneOption")
            {
                optionCanvas = t.gameObject;
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

        //??????
        globalControllerScript = GameObject.Find("GameMaster").GetComponent<GlobalController>();

        //Cursor.visible = true;

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           // backgroundMusic.GetComponent <MusicController > ().lowerMusicVolume();
           // pd.Play();
            //cinemachine.SetActive(true);
            //isStart = true;

            if (!globalControllerScript.isCutSceneFinished(this.name))
            {
                GameObject.Find("Violet").GetComponent<PlayerController>().enabled = false;
                backgroundMusic.GetComponent<MusicController>().lowerMusicVolume();
                pd.Play();
                globalControllerScript.AddFinishedCutScene(this.name);
                isStart = true;
                //Destroy(this);
               // Cursor.lockState = CursorLockMode.None;
               // Cursor.lockState = CursorLockMode.Confined;
                //Cursor.visible = true; //?
            }
            else
            {
                //Cursor.visible = false;
               // cameraScript.GetComponent<CameraController>().enabled = true;
                optionCanvas.SetActive(false);
                cameraScript.GetComponent<CameraController>().enableCameraMouse();
                GameObject.Find("Violet").GetComponent<PlayerController>().enabled = true;
                GameObject.Find("KötüKız").SetActive(false);
                GameObject.Find("SoccerBall4").SetActive(false);
                cinemachineParent.SetActive(false);

                //Cursor.lockState = CursorLockMode.Confined;
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true; //
               

                Destroy(this);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pd.state != PlayState.Playing && isStart)
        {
            showOptionsCanvas();
            isStart = false;
        }

        if(reactCutScene.cutSceneFinishedCheck())
        {

            if(isQuizNotStarted == false)
            {
                qm.startQuiz();
            }
            rain.MakeItRain();
            isQuizNotStarted = true;
            cinemachine.SetActive(false);
            backgroundMusic.GetComponent<MusicController>().increaseMusicVolume();

            cameraScript.GetComponent<CameraController>().enabled = true;
            cameraScript.GetComponent<CameraController>().enableCameraMouse();
            //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            //Cursor.lockState = CursorLockMode.None;
            // Cursor.visible = true;
            cinemachine.SetActive(false);

         

        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }


    public void showOptionsCanvas()
    {
        optionCanvas.SetActive(true);

        ReactButton = GameObject.Find("ReactButton").GetComponent<Button>();
        ReactButton.onClick.AddListener(ReactToBadGirl);

        IgnoreButton = GameObject.Find("IgnoreButton").GetComponent<Button>();
        IgnoreButton.onClick.AddListener(IgnoreNote);

        ReportButton = GameObject.Find("ReportButton").GetComponent<Button>();
        ReportButton.onClick.AddListener(ReportToTeacher);

    }


    public void IgnoreNote()
    {
        bar.DecreaseStrength();
        rain.MakeItRain();
        closeOptionsCanvas();
        qm.startQuiz();
        //Destroy(this);
    }

    public void ReportToTeacher()
    {
     //   if(isReportClicked == false)
        task.AddNewTask("Report-Violet-Jannet");
        isReportClicked = true;
        bar.IncreaseStrength();
        closeOptionsCanvas();
        qm.startQuiz();
        //Destroy(this);

    }

    public void ReactToBadGirl()
    {
        backgroundMusic.GetComponent<MusicController>().lowerMusicVolume();
        closeOptionsCanvas();
        reactCutScene.PlayCutScene();
    }

    public void closeOptionsCanvas()
    {
        optionCanvas.SetActive(false);
        GameObject.Find("KötüKız").SetActive(false);
        GameObject.Find("SoccerBall4").SetActive(false);
        cinemachine.SetActive(false);
        backgroundMusic.GetComponent<MusicController>().increaseMusicVolume();

    }

}
