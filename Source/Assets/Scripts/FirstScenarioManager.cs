
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using VIDE_Data;

public class FirstScenarioManager : MonoBehaviour
{
    public GameObject maincharacter;
    public GameObject mother;
    public GameObject virtualCam;
    public GameObject timeline;
    PlayableDirector pd;

    private GameObject cameraScript;
    
    PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        pd = timeline.GetComponent<PlayableDirector>();
        playerControllerScript = GameObject.Find("Violet").GetComponent<PlayerController>();
        cameraScript = Camera.main.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerControllerScript.enabled = false;
            cameraScript.GetComponent<CameraController>().disableCameraMouse();
            virtualCam.SetActive(true);
            pd.Play();
        }
    }

    public void TalkWithNPC()
    {
        mother.GetComponent<InteractWithCharacter>().Interact();
        maincharacter.GetComponent<Animator>().SetFloat("speedPercent", 0.0f);
        VD.SetNode(0);
        Debug.Log("in talk");
    }

    public void EndScenario()
    {
        // enable player controller and camera mouse
        playerControllerScript.enabled = true;
        cameraScript.GetComponent<CameraController>().enableCameraMouse();

        virtualCam.SetActive(false);
        pd = null;
        //Destroy(this);
    }

}
