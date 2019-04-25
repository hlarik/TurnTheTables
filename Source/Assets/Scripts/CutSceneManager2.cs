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
    public GameObject barManager;
    public GameObject moveSpot;

    private GameObject cameraScript2;

    public GameObject InteractCanvas;
    public GameObject TaskCanvas;
    public GameObject SitTextCanvas;

    PlayerController playerControllerScript;
    bool moveTowardsChair = false;

    GameObject Jannet;
    
    void Start()
    {
        playerControllerScript = GameObject.Find("Violet").GetComponent<PlayerController>();
        cameraScript2 = Camera.main.gameObject;
        pd2 = timeline2.GetComponent<PlayableDirector>();
        SitTextCanvas.SetActive(false);
        InteractCanvas.SetActive(false);
        Jannet = GameObject.Find("Jannet_Parent");
    }

    // Update is called once per frame
    void Update()
    {
        //Jannet goes back to her chair
        if (moveTowardsChair)
        {
            // play turn left animation

            float step = playerControllerScript.walkSpeed * Time.deltaTime; // calculate distance to move
            Vector3 direction = moveSpot.transform.position - Jannet.transform.position;
            direction.y = 0;
            float angle = Vector3.Angle(direction, Jannet.transform.forward);
            Jannet.transform.position = Vector3.MoveTowards(Jannet.transform.position, moveSpot.transform.position, step);
            Jannet.transform.rotation = Quaternion.Slerp(Jannet.transform.rotation, Quaternion.LookRotation(direction), 0.2f * Time.deltaTime);
            Jannet.transform.Translate(0, 0, Time.deltaTime * 1.5f);

            /*if () // angle is greater
            {

            }*/

            // rotate & walk towards 1st point



            //Jannet.gameObject.GetComponent<Transform>().rotation = 

            if (Vector3.Distance(moveSpot.transform.position, Jannet.transform.position) < 0.2f)
            {
                Debug.Log("moveTowardsChair");
                moveTowardsChair = false;
            }

            /*Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            float angle = Vector3.Angle(direction, this.transform.forward);

            if (state == "patrol" && moveSpots.Length > 0)
            {
                anim.SetBool("isInteracting", false);
                anim.SetBool("isWalking", true);
                if (Vector3.Distance(moveSpots[curMS].transform.position, transform.position) < accuracyMS)
                {
                    curMS++;
                    if (curMS >= moveSpots.Length)
                    {
                        curMS = 0;
                    }
                }

                //rotate towards waypoint
                direction = moveSpots[curMS].transform.position - transform.position;
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                this.transform.Translate(0, 0, Time.deltaTime * speed);
                */


                //Destroy(this);
            }
        }

    public void OnTriggerEnter(Collider other)
    {
        // show task popup
        TaskCanvas.SetActive(true);
        playerControllerScript.enabled = false;
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
    
    // called in timeline CutScene2 in cutSceneTrigger2.0
    public void TalkWithNPC()
    {
        //pd2 = null;
        bully.gameObject.GetComponent<InteractWithCharacter>().Interact();
        maincharacter.gameObject.GetComponent<Animator>().SetFloat("speedPercent", 0.0f);
        //Destroy(this);

        //StartCoroutine(wait());
    }

    public void EndScenario()
    {
        Debug.Log("ENdScenario");
        // enable player controller and camera mouse
        playerControllerScript.enabled = true;
        cameraScript2.GetComponent<CameraController>().enableCameraMouse();
        
        //play turn left animation
        
        StartCoroutine(wait());

       // moveTowardsChair = true;
    }

    public IEnumerator wait()
    {
       // bully.gameObject.GetComponent<InteractWithCharacter>().Interact();
       // maincharacter.gameObject.GetComponent<Animator>().SetFloat("speedPercent", 0.0f);

        //yield return new WaitForSeconds(5);
        Jannet.gameObject.transform.GetChild(0).GetComponent<JannetAnimations>().LeftTurn();

        //Vector3 y ;
        //y. = ;//392.14f; y.x = 0; y.z = 0;
        //Vector3 y2;
        //y2.y = 209.45f; y2.x = 0; y2.z = 0;
       //Jannet.transform.rotation = Quaternion.FromToRotation(Jannet.transform.rotation.y, Jannet.transform.rotation.y );//182.69);//209.45;

        yield return new WaitForSeconds(2);

        Quaternion rot = Jannet.transform.rotation;
        rot.y = 250f;
        Jannet.transform.SetPositionAndRotation(Jannet.transform.position, rot);// = 209.45;


        moveTowardsChair = true;

        //spacePressed();
    }

    /*  public IEnumerator wait()
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
      */

}