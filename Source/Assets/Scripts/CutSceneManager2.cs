using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using VIDE_Data;

public class CutSceneManager2 : MonoBehaviour
{
    //[SerializeField]
    public GameObject timeline2;
    PlayableDirector pd2;
    Animator UIanimator2;
    public GameObject maincharacter;
    public GameObject bully;
    public GameObject virtualCam;
    public GameObject[] moveSpots;
    public GameObject JannetChair;
    public GameObject TaskCanvas;
    public GameObject SitTextCanvas;

    private GameObject cameraScript2;

    GameObject Emily;
    GameObject Madison;
    GameObject Liam;
    GameObject Ethan;
    GameObject Felix;

    PlayerController playerControllerScript;
    GameObject Jannet;
    TaskManager taskManagerScript;
    Vector3 turnSmoothVelocity;
    float turnSmoothTime = 0.2f;
    float rotSpeed = 8f;

    bool moveTowardsChair = false;
    bool turningTowardsTarget = false;
    bool reachedTarget = false;
    int curMS = 0;

    void Start()
    {
        playerControllerScript = GameObject.Find("Violet").GetComponent<PlayerController>();
        cameraScript2 = Camera.main.gameObject;
        pd2 = timeline2.GetComponent<PlayableDirector>();
        SitTextCanvas.SetActive(false);
        Jannet = GameObject.Find("Jannet_Parent");
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        Emily = GameObject.Find("Emily");
        Emily.GetComponent<Animator>().SetTrigger("isSittingTalking");
        Madison = GameObject.Find("Madison");
        Madison.GetComponent<Animator>().SetTrigger("isSittingTalking");
        Liam = GameObject.Find("Liam");
        Liam.GetComponent<Animator>().SetTrigger("isWriting");
        Ethan = GameObject.Find("Ethan");
        Ethan.GetComponent<Animator>().SetTrigger("isPlayingGuitar");
        Felix = GameObject.Find("Felix");
        Felix.GetComponent<Animator>().SetTrigger("isSittingCheering");
    }

    // Update is called once per frame
    void Update()
    {
        //Jannet goes back to her chair
        if (moveTowardsChair)
        {
            // turn towards target
            if (turningTowardsTarget)
            {
                Vector3 direction = moveSpots[curMS].transform.position - Jannet.transform.position;//moveSpot.transform.position - Jannet.transform.position;
                Jannet.transform.rotation = Quaternion.Slerp(Jannet.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime * 2);
                if ((int)Quaternion.LookRotation(direction).eulerAngles.y == (int)Jannet.transform.rotation.eulerAngles.y)//|| Jannet.transform.rotation.Equals(direction))
                {
                    turningTowardsTarget = false;
                }
            }
            else
            {
                bully.GetComponent<Animator>().SetFloat("speedPercent", 0.5f);
                bully.GetComponent<Animator>().SetTrigger("Turn");
                float step = playerControllerScript.walkSpeed * Time.deltaTime / 2;
                Jannet.transform.position = Vector3.MoveTowards(Jannet.transform.position, moveSpots[curMS].transform.position, step);

                if (Vector3.Distance(moveSpots[curMS].transform.position, Jannet.transform.position) < 0.1f)
                {
                    turningTowardsTarget = true;
                    curMS++;
                    if (curMS >= moveSpots.Length-1)
                    {
                        moveTowardsChair = false;
                        bully.GetComponent<Animator>().SetBool("atTarget", true);
                        reachedTarget = true;
                        JannetChair.GetComponent<Animator>().SetTrigger("JannetAtTarget");
                    }
                }
            }
        }
        if (reachedTarget)
        {
            if (turningTowardsTarget)
            {
                Vector3 direction = moveSpots[curMS].transform.position - Jannet.transform.position;
                Jannet.transform.rotation = Quaternion.Slerp(Jannet.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime * 2);
                if ((int)Quaternion.LookRotation(direction).eulerAngles.y == (int)Jannet.transform.rotation.eulerAngles.y)
                {
                    turningTowardsTarget = false;
                }
            }
            else
            {
                JannetChair.GetComponent<Animator>().SetTrigger("JannetSit");
                bully.GetComponent<Animator>().SetTrigger("Turn");
                Destroy(this);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0;
        TaskCanvas.SetActive(true);
        taskManagerScript.AddNewTask("Find A Seat To Sit");
        playerControllerScript.enabled = false;
        cameraScript2.GetComponent<CameraController>().disableCameraMouse();
    }

    public void StartCutScene()
    {
        Time.timeScale = 1;
        virtualCam.SetActive(true);
        if (pd2 != null)
        {
            // hide task popup
            TaskCanvas.SetActive(false);
            pd2.Play();
        }
    }
    
    // called in timeline CutScene2 in cutSceneTrigger2.0
    public void TalkWithNPC()
    {
        bully.gameObject.GetComponent<InteractWithCharacter>().Interact();
        maincharacter.gameObject.GetComponent<Animator>().SetFloat("speedPercent", 0.0f);
    }

    public void EndScenario()
    {
        // enable player controller and camera mouse
        playerControllerScript.enabled = true;
        cameraScript2.GetComponent<CameraController>().enableCameraMouse();
        
        virtualCam.SetActive(false);
        turningTowardsTarget = true;
        moveTowardsChair = true;
    }
}