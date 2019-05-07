using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChairTrigger : MonoBehaviour
{
    public GameObject violetChair;
    public GameObject Violet;
    public GameObject uiObject;
    public GameObject cutScene;
    public GameObject LevelCompleteCanvas;
    public GameObject TaskCanvas;
    public GameObject[] moveSpots;

    private GameObject cameraScript;

    Animator anim;
    PlayableDirector pd;
    TaskManager taskManagerScript;
    PlayerController playerControllerScript;
    Vector3 turnSmoothVelocity;
    float turnSmoothTime = 0.2f;
    float rotSpeed = 8f;
    bool hasPlayed = false;
    bool collision;
    bool goToChair = false;
    bool turningTowardsTarget = false;
    bool reachedTarget = false;
    int curMS = 0;

    void Start()
    {
        collision = false;
        LevelCompleteCanvas.SetActive(false);
        uiObject.SetActive(false);
        cameraScript = Camera.main.gameObject;
        anim = GetComponent<Animator>();
        pd = cutScene.GetComponent<PlayableDirector>();
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        playerControllerScript = GameObject.Find("Violet").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && collision)
        {
            // disable controls
            uiObject.SetActive(false);
            GameObject.Find("Violet").GetComponent<PlayerController>().enabled = false;
            cameraScript.GetComponent<CameraController>().disableCameraMouse();
            //state = "isStanding";
            /*if (pd != null)
            {
                pd.Play();
                hasPlayed = true;
            }*/
            goToChair = true;
            turningTowardsTarget = true;
        }

        /*if (pd != null && hasPlayed)
        {
            if (pd.state != PlayState.Playing)
            {
                // when cutscene is finished popup level complete panel
                Time.timeScale = 0;
                LevelCompleteCanvas.SetActive(true);
                pd = null;
                GameObject.Find("Violet").GetComponent<PlayerController>().enabled = true;
                cameraScript.GetComponent<CameraController>().enableCameraMouse();
            }
        }*/

        if (goToChair)
        {
            // turn towards target
            if (turningTowardsTarget)
            {
                Vector3 direction = moveSpots[curMS].transform.position - Violet.transform.position;
                Violet.transform.rotation = Quaternion.Slerp(Violet.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime * 2);
                if ((int)Quaternion.LookRotation(direction).eulerAngles.y == (int)Violet.transform.rotation.eulerAngles.y)
                {
                    turningTowardsTarget = false;
                    GetComponent<Animator>().SetTrigger("atTarget");
                }
            }
            else
            {
                Violet.GetComponent<Animator>().SetFloat("speedPercent", 0.5f);
                float step = playerControllerScript.walkSpeed * Time.deltaTime / 2;
                Violet.transform.position = Vector3.MoveTowards(Violet.transform.position, moveSpots[curMS].transform.position, step);

                if (Vector3.Distance(moveSpots[curMS].transform.position, Violet.transform.position) < 0.1f)
                {
                    turningTowardsTarget = true;
                    curMS++;
                    if (curMS >= moveSpots.Length - 1)
                    {
                        goToChair = false;
                        reachedTarget = true;
                    }
                }
            }          
        }

        if (reachedTarget)
        {
            if (turningTowardsTarget)
            {
                Vector3 direction = moveSpots[curMS].transform.position - Violet.transform.position;
                Violet.transform.rotation = Quaternion.Slerp(Violet.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime * 2);
                if ((int)Quaternion.LookRotation(direction).eulerAngles.y == (int)Violet.transform.rotation.eulerAngles.y)
                {
                    turningTowardsTarget = false;
                }
            }
            else
            {
                GetComponent<Animator>().SetTrigger("violetSit");
                Violet.GetComponent<Animator>().SetBool("atTarget", true); // <-- bunu yap
                
                //Time.timeScale = 0;
                //GameObject.Find("Violet").GetComponent<PlayerController>().enabled = true;
                //cameraScript.GetComponent<CameraController>().enableCameraMouse();
                reachedTarget = false;
                LevelCompleteCanvas.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collision = true;
            uiObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collision = false;
            uiObject.SetActive(false);
        }
    }
    
    public void EnableNewTaskCanvas()
    {
        TaskCanvas.SetActive(true);
        taskManagerScript.AddNewTask("Go To Sports Hall");
        Time.timeScale = 0;
    }

    public void DisableNewTaskCanvas()
    {
        Time.timeScale = 1;
        TaskCanvas.SetActive(false);
        Violet.GetComponent<Animator>().SetBool("atTarget", false);
        GetComponent<Animator>().SetTrigger("atTarget");
        GameObject.Find("Violet").GetComponent<PlayerController>().enabled = true;
        cameraScript.GetComponent<CameraController>().enableCameraMouse();

        Destroy(this);
    }

    public void DisableTaskCanvas()
    {
        Time.timeScale = 1;
        LevelCompleteCanvas.SetActive(false);
        EnableNewTaskCanvas();
    }
}
