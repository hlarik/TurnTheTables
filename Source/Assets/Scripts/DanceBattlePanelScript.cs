using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DanceBattlePanelScript : MonoBehaviour
{
    PlayableDirector baleWin;
    PlayableDirector baleLose;
    PlayableDirector breakdanceWin;
    PlayableDirector breakdanceLose;

    PlayableDirector entrance;
    bool entranceFinished = false;
    bool Won = false;

    PlayableDirector selected;
    PlayableDirector JannetVioletTalk;
    bool violetTalkTrigger = true;

    GameObject SceneChangerScript;

    // Start is called before the first frame update
    void Start()
    {
        baleWin = GameObject.Find("Bale_Win").GetComponent<PlayableDirector>();
        baleLose = GameObject.Find("Bale_Lose").GetComponent<PlayableDirector>();
        breakdanceWin = GameObject.Find("Breakdance_Win").GetComponent<PlayableDirector>();
        breakdanceLose = GameObject.Find("Breakdance_Lose").GetComponent<PlayableDirector>();
        entrance = GameObject.Find("DanceBattle_Timeline").GetComponent<PlayableDirector>();
        JannetVioletTalk = GameObject.Find("Win").GetComponent<PlayableDirector>();
        SceneChangerScript = GameObject.Find("BlackFade");
    }

    void Update()
    {
        if (entrance.state != PlayState.Playing && !entranceFinished)
        {
            OpenDanceSelectionCanvas();
            entranceFinished = true;
        }

        if (selected != null && selected.state != PlayState.Playing)
        {
            GameObject.Find("Violet").GetComponent<PlayerController>().enabled = true;
            Camera.main.GetComponent<CameraController>().enableCameraMouse();
            GameObject virtualCams = GameObject.Find("VirtualCameras");
            for (int i = 0; i < virtualCams.transform.childCount; i++)
            {
                virtualCams.transform.GetChild(i).gameObject.SetActive(false);
            }
            if (!Won)
            {
                if (SceneChangerScript != null)
                {
                    //DontDestroyOnLoad(GameObject.Find("MainCharacter")); ----> Bunu sonradan yapariz!!!
                    SceneChangerScript.GetComponent<LevelChangerWithFade>().ChangeLevelWithFade("FirstFloor");
                }
                else
                {
                    SceneManager.LoadScene("FirstFloor");
                }
            }
            else if(violetTalkTrigger)
            {
                JannetVioletTalk.Play();
                violetTalkTrigger = false;
            }

        }
    }

    public void OpenDanceSelectionCanvas()
    {
        this.GetComponent<Animator>().SetBool("isOpen", true);
        GameObject.Find("Violet").GetComponent<PlayerController>().enabled = false;
        Camera.main.GetComponent<CameraController>().disableCameraMouse();

    }

    public void CloseDanceSelectionCanvas()
    {
        this.GetComponent<Animator>().SetBool("isOpen", false);
    }

    public void SelectBale()
    {
        CloseDanceSelectionCanvas();
        if (GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetStrength() > 60)
        {
            Won = true;
            selected = baleWin;
            baleWin.Play();
        }
        else
        {
            Won = false;
            selected = baleLose;
            baleLose.Play();
        }
    }

    public void SelectBreakdance()
    {
        CloseDanceSelectionCanvas();
        if (GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetStrength() > 60)
        {
            Won = true;
            selected = breakdanceWin;
            breakdanceWin.Play();
        }
        else
        {
            Won = false;
            selected = breakdanceLose;
            breakdanceLose.Play();
        }
    }


}
