using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndrewMattCutSceneGestureController : MonoBehaviour
{
    Animator andrewAnim;
    Animator violetAnim;
    
    void Start()
    {
        andrewAnim = GameObject.Find("Andrew").GetComponent<Animator>();
        violetAnim = GameObject.Find("Violet").GetComponent<Animator>();
    }
    
    public void MakeAndrewSad()
    {
        andrewAnim.SetTrigger("isSadTalking");
    }

    public void MakeAndrewGrateful()
    {
        andrewAnim.SetTrigger("isGrateful");
    }

    public void MakeAndrewSadIdle()
    {
        andrewAnim.SetTrigger("isSadIdle");
    }

    public void MakeAndrewTalk()
    {
        andrewAnim.SetTrigger("isTalking");
    }

    public void MakeVioletTalk()
    {
        violetAnim.SetTrigger("talking");
    }

    public void MakeVioletHeadGesture()
    {
        violetAnim.SetTrigger("HeadGesture");
    }

    public void MakeVioletHappyHandGesture()
    {
        violetAnim.SetTrigger("HappyHandGesture");
    }
}
