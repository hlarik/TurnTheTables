using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SportsHallCutScene : MonoBehaviour
{
    public PlayableDirector pd;
  //  public PlayableDirector pd2; //for react to mean girl cutscene
    public Animator anim;
    private GameObject optionParentCanvas;
    private GameObject optionCanvas;

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



    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GameObject.Find("BackgroundMusic");

        pd = GetComponent<PlayableDirector>();
       // pd2 = GetComponent<PlayableDirector>();
        cameraScript = Camera.main.gameObject;

        optionParentCanvas = GameObject.Find("SportsHallCutsceneCanvas");
        optionCanvas = GameObject.Find("SportsCutsceneOption");

        Transform[] children = optionParentCanvas.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children)
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
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            backgroundMusic.GetComponent <MusicController > ().lowerMusicVolume();
            pd.Play();
            cinemachine.SetActive(true);
            isStart = true;

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
            Destroy(this);

        }
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
        rain.MakeItRain();
        closeOptionsCanvas();
        qm.startQuiz();
        Destroy(this);
    }

    public void ReportToTeacher()
    {
        closeOptionsCanvas();
        qm.startQuiz();
        Destroy(this);

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
