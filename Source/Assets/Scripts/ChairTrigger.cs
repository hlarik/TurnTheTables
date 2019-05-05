using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChairTrigger : MonoBehaviour
{
    public Transform maincharacter;
    public GameObject uiObject;
    public GameObject cutScene;

    bool collision;

    private GameObject cameraScript;
    Animator anim;
    PlayableDirector pd;
    bool hasPlayed = false;
    
    public GameObject LevelCompleteCanvas;
    public GameObject TaskCanvas;

    TaskManager taskManagerScript;

    void Start()
    {
        collision = false;
        LevelCompleteCanvas.SetActive(false);
        uiObject.SetActive(false);
        cameraScript = Camera.main.gameObject;
        anim = GetComponent<Animator>();
        pd = cutScene.GetComponent<PlayableDirector>();
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManager>();
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
            if (pd != null)
            {
                pd.Play();
                hasPlayed = true;
            }
        }

        if (pd != null && hasPlayed)
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
       // GameObject.Find("Violet").GetComponent<PlayerController>().enabled = true;
       // cameraScript.GetComponent<CameraController>().enableCameraMouse();
        Destroy(this);
    }

    public void DisableTaskCanvas()
    {
        Time.timeScale = 1;
        LevelCompleteCanvas.SetActive(false);
        EnableNewTaskCanvas();
    }
}
