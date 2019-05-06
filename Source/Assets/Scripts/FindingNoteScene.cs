using System;
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
   // private CameraController cameraScript;
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

    private GameObject exitTxt;  ///?????????
    private GameObject dialogTxt;

    GlobalController globalControllerScript; //???
    private GameObject frontCamera;

    BarManager bar;
    TaskManager task;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          

            cinemachine.SetActive(true);

            if(!globalControllerScript.isCutSceneFinished(this.name))
             {
                 backgroundMusic.GetComponent<MusicController>().lowerMusicVolume();
                 GameObject.Find("Violet").GetComponent<PlayerController>().enabled = false;
                 pd.Play();
                 globalControllerScript.AddFinishedCutScene(this.name);
                 isStart = true;

                 //cameraScript.GetComponent<CameraController>().enableCameraMouse();
                 Cursor.lockState = CursorLockMode.None;
                 Cursor.lockState = CursorLockMode.Confined;
                 Cursor.visible = true; //
            }
            else
            {
              
                noteImage.SetActive(false);
                cameraScript.GetComponent<CameraController>().enableCameraMouse();
                GameObject.Find("Violet").GetComponent<PlayerController>().enabled = true;

                /*Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true; //?*/
               

                noteIsShown = false;

                (GameObject.Find("Violet").GetComponent(typeof(Collider)) as Collider).isTrigger = false;
                //(GameObject.Find("PaperParent")).SetActive(false);

                cinemachine.SetActive(false);

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                Destroy(this); //?????
            }
            //  pd.Play();


        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //???
        /*Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; //?*/
        ///////

        frontCamera = GameObject.Find("frontcamera");
        frontCamera.SetActive(false);
       // cameraScript = GameObject.Find("MainCamera").GetComponent<CameraController>();
        cameraScript = Camera.main.gameObject;

        exitTxt = GameObject.Find("Enter thru Door Canvas");
        dialogTxt = GameObject.Find("Interact with character canvas");
        exitTxt.SetActive(false);
        dialogTxt.SetActive(false);

        bar = GameObject.Find("BarManager").GetComponent<BarManager>();
        task = GameObject.Find("TaskManager").GetComponent<TaskManager>();

        backgroundMusic = GameObject.Find("BackgroundMusic");

        //cameraScript = Camera.main.gameObject;
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

        globalControllerScript = GameObject.Find("GameMaster").GetComponent<GlobalController>();

    }

    void Update()
    {
        //Cutscene bitince
        if (pd.state != PlayState.Playing && isStart)
        {
            showNoteScreen();
            isStart = false;
            backgroundMusic.GetComponent<MusicController>().increaseMusicVolume();
            globalControllerScript.AddFinishedCutScene(this.name);

        }
        ///////
        /*if (Input.GetKeyDown(KeyCode.M)|| Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true; //?
        }*/
        ///////
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }


    public void showNoteScreen()
    {
        noteImage.SetActive(true);
        noteIsShown = true;

        /*Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;*/

        cameraScript.GetComponent<CameraController>().disableCameraMouse();
        GameObject.Find("Violet").GetComponent<PlayerController>().enabled = false;

        IgnoreButton = GameObject.Find("IgnoreButton").GetComponent<Button>();
        IgnoreButton.onClick.AddListener(IgnoreNote);

        ReportButton = GameObject.Find("ReportButton").GetComponent<Button>();
        ReportButton.onClick.AddListener(ReportToTeacher);

    }

    public void IgnoreNote()
    {
        bar.DecreaseStrength();
        rain.MakeItRain();
        closeNote();
    }

    public void ReportToTeacher()
    {
        rain.MakeItStop();
        task.AddNewTask("Report-Violet-Someone"); //Kim yapayım?
        bar.IncreaseStrength();
        closeNote();
    }

    public void closeNote()
    {
        noteImage.SetActive(false);

        cameraScript.GetComponent<CameraController>().enableCameraMouse();
        cameraScript.GetComponent<CameraController>().enabled = true;
        GameObject.Find("Violet").GetComponent<PlayerController>().enabled = true;

       // Cursor.visible = true; //?


        //Destroy(this); //?????

        noteIsShown = false;

        (GameObject.Find("Violet").GetComponent(typeof(Collider)) as Collider).isTrigger = false;
        (GameObject.Find("PaperParent")).SetActive(false);

        cinemachine.SetActive(false);
        qm.startQuiz();
        if (!globalControllerScript.isCutSceneFinished(this.name))
        {

        
            /*Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true; //*/
            Destroy(this);
        }
    }





}
