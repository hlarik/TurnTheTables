using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DanceBattleJannetViioletTalking : MonoBehaviour
{
    Animator violetAnimator;
    Animator jannetAnimator;
    PlayableDirector finishingDance = null;
    GameObject SceneChangerScript;

    void Start()
    {
        violetAnimator = GameObject.Find("Violet").GetComponent<Animator>();
        jannetAnimator = GameObject.Find("Jannet").GetComponent<Animator>();
        SceneChangerScript = GameObject.Find("BlackFade");

    }

    private void Update()
    {
        if(finishingDance!= null && finishingDance.state != PlayState.Playing)
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
    }

    public void CallVDInsideJannet()
    {
        GameObject.Find("Jannet").GetComponent<InteractWithCharacter>().JannetOpenVDLastScene();
    }

    public void VioletHappyHandGesture()
    {
        violetAnimator.SetTrigger("HappyHandGesture");
    }

    public void VioletAcknowledgeGesture()
    {
        violetAnimator.SetTrigger("acknowledge");
    }

    public void VioletHandsTalk()
    {
        violetAnimator.SetTrigger("talking");
    }

    public void VioletHeadGesture()
    {
        violetAnimator.SetTrigger("HeadGesture");
    }

    public void VioletLetsDance()
    {
        violetAnimator.SetTrigger("LetsDance");
    }

    public void JannetYelling()
    {
        jannetAnimator.SetTrigger("yelling");
    }

    public void JannetAcknowledge()
    {
        jannetAnimator.SetTrigger("acknowledge");
    }

    public void JannetShy()
    {
        jannetAnimator.SetTrigger("shy");
    }

    public void CallLastDance()
    {
        finishingDance = GameObject.Find("LastDance").GetComponent<PlayableDirector>();
        finishingDance.Play();
    }

}
