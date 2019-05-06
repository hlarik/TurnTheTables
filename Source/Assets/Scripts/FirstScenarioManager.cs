
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using VIDE_Data;

public class FirstScenarioManager : MonoBehaviour
{
    public GameObject maincharacter;
    public GameObject mother;
    public GameObject virtualCam;
    public GameObject timeline;
    public GameObject[] moveSpots;
    public GameObject taskCanvas;

    GameObject MotherParent;
    GameObject Maya;
    GameObject Ethan;
    
    PlayableDirector pd;
    PlayerController playerControllerScript;
    GlobalController globalControllerScript;
    TaskManager taskManagerScript;
    Vector3 turnSmoothVelocity;
    float turnSmoothTime = 0.2f;
    float rotSpeed = 8f;
    bool sceneEnded = false;
    bool turningTowardsTarget = false;
    bool reachedTarget = false;
    int curMS = 0;

    GameObject interactCanvas;
    GameObject exitCanvas;

    private GameObject cameraScript;

    // Start is called before the first frame update
    void Start()
    {
        globalControllerScript = GameObject.Find("GameMaster").GetComponent<GlobalController>();
        pd = timeline.GetComponent<PlayableDirector>();
        playerControllerScript = GameObject.Find("Violet").GetComponent<PlayerController>();
        cameraScript = Camera.main.gameObject;
        MotherParent = GameObject.Find("Mother_Parent");
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        Maya = GameObject.Find("Maya");
        Maya.GetComponent<Animator>().SetTrigger("isTalking");
        Ethan = GameObject.Find("Ethan");
        Ethan.GetComponent<Animator>().SetTrigger("isLaughing");

        if (globalControllerScript.isCutSceneFinished(this.name))
        {
            Destroy(this);
            virtualCam.SetActive(false);
            Destroy(mother);
        }
    }

    void Update()
    {
        //if scene has ended mom goes back to her cave
        if (sceneEnded)
        {
            // turn towards target
            if (turningTowardsTarget)
            {
                Vector3 direction = moveSpots[curMS].transform.position - MotherParent.transform.position;
                MotherParent.transform.rotation = Quaternion.Slerp(MotherParent.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime * 2);
                if ((int)Quaternion.LookRotation(direction).eulerAngles.y == (int)MotherParent.transform.rotation.eulerAngles.y)
                {
                    turningTowardsTarget = false;
                }
            }
            else
            {
                mother.GetComponent<Animator>().SetTrigger("walk");
                float step = playerControllerScript.walkSpeed * Time.deltaTime / 2;
                MotherParent.transform.position = Vector3.MoveTowards(MotherParent.transform.position, moveSpots[curMS].transform.position, step);

                if (Vector3.Distance(moveSpots[curMS].transform.position, MotherParent.transform.position) < 0.1f)
                {
                    turningTowardsTarget = true;
                    curMS++;
                    if (curMS >= moveSpots.Length)
                    {
                        sceneEnded = false;
                        reachedTarget = true;
                    }
                }
            }
        }
        if (reachedTarget)
        {
            //disappear into thin air *poof*
            MotherParent.SetActive(false);
        }
    }

        public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (pd != null && !globalControllerScript.isCutSceneFinished(this.name))
            {
                playerControllerScript.enabled = false;
                cameraScript.GetComponent<CameraController>().disableCameraMouse();
                virtualCam.SetActive(true);
                pd.Play();
            }
            globalControllerScript.AddFinishedCutScene(this.name);
        }
    }

    public void TalkWithNPC()
    {
        mother.gameObject.GetComponent<InteractWithCharacter>().Interact();
        maincharacter.GetComponent<Animator>().SetFloat("speedPercent", 0.0f);
    }

    public void EndScenario()
    {
        // enable player controller and camera mouse
        playerControllerScript.enabled = true;
        cameraScript.GetComponent<CameraController>().enableCameraMouse();
        virtualCam.SetActive(false);
        sceneEnded = true;
        turningTowardsTarget = true;
        globalControllerScript.AddFinishedCutScene(this.name);
        EnableTaskCanvas();
    }

    public void EnableTaskCanvas()
    {
        Time.timeScale = 0;
        taskCanvas.SetActive(true);
        taskManagerScript.AddNewTask("Go Inside Your New School");
        playerControllerScript.enabled = false;
        cameraScript.GetComponent<CameraController>().disableCameraMouse();
    }

    public void DisableTaskCanvas()
    {
        Time.timeScale = 1;
        taskCanvas.SetActive(false);
        playerControllerScript.enabled = true;
        cameraScript.GetComponent<CameraController>().enableCameraMouse();
    }
}
