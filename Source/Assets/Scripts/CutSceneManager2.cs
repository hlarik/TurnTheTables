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

    private GameObject cameraScript2;

    public GameObject InteractCanvas;
    public GameObject TaskCanvas;
    public GameObject SitTextCanvas;


    //Virtual Cameras
    void Start()
    {
        cameraScript2 = Camera.main.gameObject;
        pd2 = timeline2.GetComponent<PlayableDirector>();
        SitTextCanvas.SetActive(false);
        InteractCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        /*if (pd2 != null)
        {
            //When cutscene is finished, pop up the decisions panel
            if (pd2.state != PlayState.Playing)
            {
                
                //Destroy(this);
                //When dialogue starts disable camera movement and character movement
               // cameraScript2.GetComponent<CameraController>().disableCameraMouse();
              //  GameObject.Find("Violet").GetComponent<PlayerController>().enabled = false;

               // pd2 = null;
                //Daha sonra yukarda disable ettiklerini acmayi unutma
               // GameObject.Find("ExitText").GetComponent<PlayerController>().enabled = true;
               // GameObject.Find("Interact with character canvas").GetComponent<PlayerController>().enabled = true;

            }
        }*/
    }

    public void OnTriggerEnter(Collider other)
    {
        // show task popup
        TaskCanvas.SetActive(true);
        GameObject.Find("Violet").GetComponent<PlayerController>().enabled = false;
        cameraScript2.GetComponent<CameraController>().disableCameraMouse();
    }



    public void StartCutScene()
    {
        virtualCam.SetActive(true);
        if (pd2 != null)
        {
            // hide task popup
            TaskCanvas.SetActive(false);
            pd2.Play();
        }
    }

    public void TalkWithNPC()
    {
        StartCoroutine(wait());
    }

    public IEnumerator wait()
    {
        bully.gameObject.GetComponent<InteractWithCharacter>().Interact();
        maincharacter.gameObject.GetComponent<Animator>().SetFloat("speedPercent", 0.0f);

        yield return new WaitForSeconds(5);

        spacePressed();

        yield return new WaitForSeconds(3);

        spacePressed();
    }

    void spacePressed()
    {
        VD.Next();
    }


}