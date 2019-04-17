using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChairTrigger : MonoBehaviour
{
    public Transform maincharacter;
    public GameObject uiObject;
    public GameObject cutScene;

    bool collision;

    private GameObject cameraScript;

    double distance = 1;
    Animator anim;

    string state = "";
    public GameObject moveSpot;
    int curMS = 0;
    public float rotSpeed = 0.2f;
    public float speed = 1.5f;
    float accuracyMS = 0.01f;
    PlayableDirector pd;

    bool hasPlayed = false;

    //UI eleements
    public GameObject LevelCompleteCanvas;

    void Start()
    {
        collision = false;
        LevelCompleteCanvas.SetActive(false);
        uiObject.SetActive(false);
        cameraScript = Camera.main.gameObject;
        anim = GetComponent<Animator>();
        pd = cutScene.GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && collision)
        {
            // disable controls
            uiObject.SetActive(false);
            GameObject.Find("Violet").GetComponent<PlayerController>().enabled = false;
            cameraScript.GetComponent<CameraController>().disableCameraMouse();
            state = "isStanding";
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
                LevelCompleteCanvas.SetActive(true);
            }
        }



        // start timeline


        /*Vector3 direction = moveSpot.transform.position - maincharacter.transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, maincharacter.transform.forward);

        if (state == "isStanding")
        {
            if (Vector3.Distance(moveSpot.transform.position, maincharacter.transform.position) > accuracyMS)
            {
                // move violet towards chair
                maincharacter.transform.position = Vector3.MoveTowards(maincharacter.transform.position, moveSpot.transform.position, Time.deltaTime * speed);
                direction = moveSpot.transform.position - transform.position;
                maincharacter.transform.rotation = Quaternion.Slerp(maincharacter.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            }
            else
            {
                state = "isSitting";
            }
        }*/
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
}
