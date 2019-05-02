using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using VIDE_Data;

public class GossipCutscene : MonoBehaviour
{
    //[SerializeField]
    public GameObject timeline;
    PlayableDirector pd;
    public GameObject maincharacter;
    public GameObject virtualCam;
    public GameObject barManager;

    private GameObject cameraScript;
    PlayerController playerControllerScript;

    GameObject Jannet;
    GameObject Maya;
    GameObject Liam;
    GameObject Emily;
    GameObject Madison;
    GameObject Felix;
    
    void Start()
    {
        playerControllerScript = GameObject.Find("Violet").GetComponent<PlayerController>();
        cameraScript = Camera.main.gameObject;
        pd = timeline.GetComponent<PlayableDirector>();

        Maya = GameObject.Find("Maya");
        Maya.GetComponent<Animator>().SetTrigger("isRubbingArm");
        Liam = GameObject.Find("Liam");
        Liam.GetComponent<Animator>().SetTrigger("isSittingCheering");
        Felix = GameObject.Find("Felix");
        Felix.GetComponent<Animator>().SetTrigger("isDancing");
        Emily = GameObject.Find("Emily");
        Emily.GetComponent<Animator>().SetTrigger("isSittingTalking");
        Madison = GameObject.Find("Madison");
        Madison.GetComponent<Animator>().SetTrigger("isTalking");
        Jannet = GameObject.Find("Jannet");
        Jannet.GetComponent<Animator>().SetTrigger("isTalking");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        playerControllerScript.enabled = false;
        cameraScript.GetComponent<CameraController>().disableCameraMouse();
    }

    public void StartCutScene()
    {
        //virtualCam.SetActive(true);
        if (pd != null)
        {
            pd.Play();
        }
    }

    public void EndScenario()
    {
        // enable player controller and camera mouse
        playerControllerScript.enabled = true;
        cameraScript.GetComponent<CameraController>().enableCameraMouse();

        //virtualCam.SetActive(false);
    }
}