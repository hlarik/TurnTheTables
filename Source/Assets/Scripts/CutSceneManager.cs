using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using VIDE_Data;

public class CutSceneManager : MonoBehaviour
{
    //[SerializeField]
    public GameObject timeline;
    PlayableDirector pd;
    PlayableDirector pdEmpathy;
    Animator UIanimator;
    public GameObject bullied;
    public GameObject bully;
    public GameObject maincharacter;
    private GameObject cameraScript;
    bool playerMoveTowardTarget = false;
    bool empathize = false;
    bool empathyButtonClicked = false;
    bool ignoreButtonClicked = false;
    bool talkButtonClicked = false;
    int[] ignoreDialogues = { 0, 4, 5, 6 };
    int[] empathyDialogues = { 2, 7, 8, 9 };
    int[] talkDialogues = { 1, 10 };
    int[] reportDialogues = { 3, 11, 12, 13 };
    System.Random rnd;
    PlayerController playerControllerScript;
    RainController rainController;
    FaceAnimationController faceController;
    TaskManager TaskManagerScript;
    GlobalController globalControllerScript;
    GeneralPatrolScript patrolScript;
    GeneralPatrolScript patrolScriptBully;
    BarManager barManagerScript;


    //UI eleements
    public GameObject DecisionsCanvas;

    //Empathy Camera
    public Camera empathyCamera;
    public Camera mainCamera;
    LevelChangerWithFade empathyFadeInandOut;      // for empathy fade animation 

    //To turn character towards the npc she wants to talk to 
    Vector3 delta;
    Vector3 turnSmoothVelocity;
    float turnSmoothTime = 0.2f;

    //Virtual Cameras
    public GameObject virtualCam;

    //Inner voice Chaarcter animator
    public GameObject innerVoicePanel;
    Animator innerVoiceAnimator;
    Text innverVoiceFeedback;

    //Call ignore again
    bool againIgnore;
    int empathizeOnFirstCall = 0;

    void Start()
    {
        globalControllerScript = GameObject.Find("GameMaster").GetComponent<GlobalController>();
        /*if (globalControllerScript.isCutSceneFinished(this.name))
            Destroy(this);*/
        
        patrolScript = bullied.GetComponent<GeneralPatrolScript>();
        patrolScript.enabled = false;
        patrolScriptBully = bully.GetComponent<GeneralPatrolScript>();
        patrolScriptBully.enabled = false;

        if (globalControllerScript.isCutSceneFinished(this.name))
        {
            bully.SetActive(true);
            patrolScript.enabled = true;
            patrolScriptBully.enabled = true;
            Debug.Log("EXISTS SOMEHOWWW");
            Destroy(this);
        }

        faceController = new FaceAnimationController();
        rainController = GameObject.Find("RainParent").GetComponent<RainController>();
        playerControllerScript = GameObject.Find("Violet").GetComponent<PlayerController>();
        cameraScript = Camera.main.gameObject;
        DecisionsCanvas.SetActive(false);
        UIanimator = DecisionsCanvas.transform.GetChild(0).gameObject.GetComponent<Animator>();
        innerVoiceAnimator = innerVoicePanel.gameObject.GetComponent<Animator>();
        innverVoiceFeedback = innerVoicePanel.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        if (!innverVoiceFeedback)
            Debug.Log("ERRORORORROR");
        //bullied.gameObject.GetComponent<InteractWithCharacter>().EPressed();
        ChangeToMainCamera();
        againIgnore = true;
        rnd = new System.Random();
        empathyFadeInandOut = GameObject.Find("BlackFade").GetComponent<LevelChangerWithFade>();
        TaskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        barManagerScript = GameObject.Find("BarManager").GetComponent<BarManager>();
        //empathyCamera = bullied.transform.Find("EmpathyCamera").GetComponent<Camera>();
        //Debug.Log("aklsdj  ===   " + maincharacter.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(15));
        //virtualCam = GameObject.Find("CinemachineVirtualCameras");
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
                playerControllerScript.enabled = false;

                pd = null;
                //Daha sonra yukarda disable ettiklerini acmayi unutma

                DecisionsCanvas.SetActive(true);
                UIanimator.SetBool("isOpen", true);
                globalControllerScript.AddFinishedCutScene(this.name);
            }
        }

        if (pdEmpathy != null)
        {
            //When cutscene is finished, pop up the decisions panel
            if (pdEmpathy.state != PlayState.Playing)
            {
                //Destroy(this);
                //When dialogue starts diable camera movement and character movement
                cameraScript.GetComponent<CameraController>().disableCameraMouse();
                playerControllerScript.enabled = false;

                //Daha sonra yukarda disable ettiklerini acmayi unutma
                ChangeToMainCamera();
                empathize = false;

                DecisionsCanvas.SetActive(true);
                UIanimator.SetBool("isOpen", true);

                pdEmpathy = null;

            }
        }


        if (playerMoveTowardTarget)
        {
            maincharacter.gameObject.GetComponent<Animator>().SetFloat("speedPercent", 0.5f);

            //rotation
            // maincharacter.transform.eulerAngles = Vector3.SmoothDamp(maincharacter.transform.rotation.eulerAngles, Quaternion.LookRotation(delta).eulerAngles, ref turnSmoothVelocity, turnSmoothTime);

            // Move our position a step closer to the target.
            float step = playerControllerScript.walkSpeed * Time.deltaTime; // calculate distance to move
            maincharacter.transform.position = Vector3.MoveTowards(maincharacter.transform.position, bullied.transform.position, step);
            //print("also here?");
            //if enters trigger of target
            if (bullied.gameObject.GetComponent<InteractWithCharacter>().collision)
            {
                bullied.gameObject.GetComponent<InteractWithCharacter>().EPressed();
                VD.SetNode(0);
                playerMoveTowardTarget = false;
                maincharacter.gameObject.GetComponent<Animator>().SetFloat("speedPercent", 0.0f);
                EndScenario();
                Destroy(this);
            }
        }

        if (empathyButtonClicked && !VD.isActive)
        {
            //pdEmpathy = timeline.GetComponent<PlayableDirector>();
            empathyFadeInandOut.empathyFade();
            //if (pdEmpathy != null)
           //{
                playerControllerScript.enabled = false;
                cameraScript.GetComponent<CameraController>().disableCameraMouse();
               // ChangeToEmpathyCamera();
                Invoke("callEmpathyScene", 1.2f);
                //pdEmpathy.Play();
            //}
            empathyButtonClicked = false;
        }

        if (ignoreButtonClicked && !VD.isActive)
        {
            OpenDecisionCanvas();
            ignoreButtonClicked = false;
        }

        if (talkButtonClicked && !VD.isActive)
        {
            talkButtonClicked = false;
            playerMoveTowardTarget = true;
        }
    }

    public void callEmpathyScene()
    {
        pdEmpathy = timeline.GetComponent<PlayableDirector>();
        playerControllerScript.enabled = false;
        cameraScript.GetComponent<CameraController>().disableCameraMouse();
        ChangeToEmpathyCamera();
        pdEmpathy.Play();
    }

    public void OnTriggerEnter(Collider other)
    {
        
        pd = timeline.GetComponent<PlayableDirector>();
        virtualCam.SetActive(true);
        if (pd != null && !globalControllerScript.isCutSceneFinished(this.name))
        {
            playerControllerScript.enabled = false;
            cameraScript.GetComponent<CameraController>().disableCameraMouse();
            pd.Play();
        }
        globalControllerScript.AddFinishedCutScene(this.name);
    }

    public void TalkWithNPC()
    {
        //Close canvas
        UIanimator.SetBool("isOpen", false);
        pd = null;
        pdEmpathy = null;
        //float step = maincharacter.gameObject.GetComponent<PlayerController>().walkSpeed * Time.deltaTime;
        delta = new Vector3(bullied.transform.position.x - maincharacter.transform.position.x, 0.0f, bullied.transform.position.z - maincharacter.transform.position.z);
        /*if(bullied.transform.position.x - maincharacter.transform.position.x < maincharacter.transform.position.x - bullied.transform.position.x)
            delta = new Vector3(maincharacter.transform.position.x - bullied.transform.position.x, 0.0f, bullied.transform.position.z - maincharacter.transform.position.z);
        else
            delta = new Vector3(bullied.transform.position.x - maincharacter.transform.position.x, 0.0f, maincharacter.transform.position.z - bullied.transform.position.z);*/
        maincharacter.GetComponent<InteractWithCharacter>().InnerVoiceDialogue(talkDialogues[rnd.Next(0, talkDialogues.Length)]);
        talkButtonClicked = true;
        maincharacter.GetComponent<MainPlayerStats>().SetFriendliness(maincharacter.GetComponent<MainPlayerStats>().GetFriendliness() + 1);
        barManagerScript.IncreaseFriendliness();

    }

    public void Empathsize()
    {
        //Close canvas
        UIanimator.SetBool("isOpen", false);
        empathyButtonClicked = true;
        pd = null;
        maincharacter.GetComponent<InteractWithCharacter>().InnerVoiceDialogue(empathyDialogues[rnd.Next(0, empathyDialogues.Length)]);
        empathize = true;
        if (empathizeOnFirstCall == 0)
        {
            rainController.MakeItStop();
            faceController.MakeAllCharactersHappy();
            maincharacter.GetComponent<MainPlayerStats>().SetFriendliness(maincharacter.GetComponent<MainPlayerStats>().GetFriendliness() + 1);
            barManagerScript.IncreaseFriendliness();
        }
        empathizeOnFirstCall = 1;


        //Destroy(this);
    }

    public void Ignore()
    {
        //Close canvas
        UIanimator.SetBool("isOpen", false);
        pd = null;
        if (againIgnore)
        {
            maincharacter.GetComponent<InteractWithCharacter>().InnerVoiceDialogue(ignoreDialogues[rnd.Next(0, ignoreDialogues.Length)]);
            ignoreButtonClicked = true;
            againIgnore = !againIgnore;
        }
        else
        {
            maincharacter.GetComponent<MainPlayerStats>().SetFriendliness(maincharacter.GetComponent<MainPlayerStats>().GetFriendliness() - 1);
            barManagerScript.DecreaseFriendliness();
            faceController.MakeAllCharactersSad();
            EndScenario();
            /*innerVoiceAnimator.SetBool("isOpen", true);
            StopAllCoroutines();
            //StartCoroutine(Frown());
            StartCoroutine(TypeSentence("nooo dont ignore :("));
            setCharacterPlayable();*/
            //Destroy(this);
        }
    }

    public void ReportToAnAdult()
    {
        //Close canvas
        UIanimator.SetBool("isOpen", false);
        pd = null;
        maincharacter.GetComponent<InteractWithCharacter>().InnerVoiceDialogue(reportDialogues[rnd.Next(0, reportDialogues.Length)]);
        //maincharacter.GetComponent<MainPlayerStats>().SetStrength(maincharacter.GetComponent<MainPlayerStats>().GetStrength() + 1);
        barManagerScript.IncreaseStrength();
        TaskManagerScript.AddNewTask("Report-Andrew-Matt");
        rainController.MakeItStop();
        faceController.MakeAllCharactersHappy();
        //Destroy(this);
        EndScenario();
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

    void setCharacterPlayable()
    {
        cameraScript.GetComponent<CameraController>().enableCameraMouse();
        playerControllerScript.enabled = true;
    }

    /*IEnumerator TypeSentence(string sentence)
    {
        innverVoiceFeedback.text = "";
        //maincharacter.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(15, 100f);
        maincharacter.gameObject.GetComponent<Animator>().SetBool("isSad", true);
        maincharacter.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, 100f);
        foreach (char letter in sentence.ToCharArray())
        {
            innverVoiceFeedback.text += letter;
            yield return null;
            yield return null;
            yield return null;
        }
        Invoke("CloseInnerVoice", 3);
    }*/


    void CloseInnerVoice()
    {
        maincharacter.gameObject.GetComponent<Animator>().SetBool("isSad", false);
        maincharacter.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(15, 0f);
        innerVoiceAnimator.SetBool("isOpen", false);
    }

    public void OpenDecisionCanvas()
    {
        playerControllerScript.enabled = false;
        cameraScript.GetComponent<CameraController>().disableCameraMouse();
        DecisionsCanvas.SetActive(true);
        UIanimator.SetBool("isOpen", true);
    }

    public void EndScenario()
    {
        playerControllerScript.enabled = true;
        cameraScript.GetComponent<CameraController>().enableCameraMouse();
        patrolScript.enabled = true;
        patrolScriptBully.enabled = true;
        //Destroy(timeline);
        Destroy(this);
    }



}
