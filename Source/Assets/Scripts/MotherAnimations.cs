using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherAnimations : MonoBehaviour
{
    Animator anim;
    public GameObject mainCharacter;

    public void Talking1()
    {
        GetComponent<Animator>().Play("Talking1", -1, 0f);
    }

    public void Talking2()
    {
        GetComponent<Animator>().Play("Talking2", -1, 0f);
    }

    public void Waving()
    {
        GetComponent<Animator>().Play("Waving", -1, 0f);
    }

    public void shrugging()
    {
        mainCharacter.GetComponent<Animator>().Play("Shrugging", -1, 0f);
    }

    public void WavingViolet()
    {
        mainCharacter.GetComponent<Animator>().Play("Waving", -1, 0f);
    }

}