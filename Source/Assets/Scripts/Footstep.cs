using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource Track1;
    public int pressedOne; //w:0 & a:1 && s:2 && d:3
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Track1.Play();
            pressedOne = 0;

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Track1.Play();
            pressedOne = 1;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Track1.Play();
            pressedOne = 2;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Track1.Play();
            pressedOne = 3;
        }

        if (pressedOne == 0 && Input.GetKeyUp(KeyCode.W))
        {
            Track1.Stop();
        }

        if (pressedOne == 1 && Input.GetKeyUp(KeyCode.A))
        {
            Track1.Stop();
        }

        if (pressedOne == 2 && Input.GetKeyUp(KeyCode.S))
        {
            Track1.Stop();
        }

        if (pressedOne == 3 && Input.GetKeyUp(KeyCode.D))
        {
            Track1.Stop();
        }



    }
}
