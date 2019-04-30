using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JannetAnimations : MonoBehaviour
{
    public GameObject mainPlayer;

    public void LookingDown()
    {
        mainPlayer.GetComponent<Animator>().Play("LookingDown", -1, 0f);
    }

    public void SadIdle()
    {
        mainPlayer.GetComponent<Animator>().Play("SadIdle", -1, 0f);
    }

    public void Talking()
    {
        mainPlayer.GetComponent<Animator>().Play("Talking", -1, 0f);
    }

    public void YellingOut()
    {
        GetComponent<Animator>().Play("YellingOut", -1, 0f);
    }

    public void Angry()
    {
        GetComponent<Animator>().Play("Angry", -1, 0f);
    }

    public void LeftTurn()
    {
        GetComponent<Animator>().Play("LeftTurn", -1, 0f);
    }

    public void Walking()
    {
        GetComponent<Animator>().Play("Walking", -1, 0f);
    }

    public void StandToSit()
    {
        GetComponent<Animator>().Play("StandToSit", -1, 0f);
    }

    public void AngryPoint()
    {
        GetComponent<Animator>().Play("AngryPoint", -1, 0f);
    }

}
