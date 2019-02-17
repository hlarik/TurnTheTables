using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherAnimations : MonoBehaviour
{
    Animator anim;
    public GameObject mainPlayer;
    public void Talk1()
    {
        GetComponent<Animator>().Play("Talking1", -1, 0f);
    }

    public void TalkMain()
    {
        // mainPlayer = GetComponent<GameObject>();
        mainPlayer.GetComponent<Animator>().Play("Shrugging", -1, 0f);

    }

    //
    /*  public void Talk2()
      {
          anim.Play("New cube Animation");
      }*/
}