using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaussed;

    // Start is called before the first frame update
    void Start()
    {
        isPaussed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //According to when you want the game to pause, defien it here
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaussed = !isPaussed;
        }

        if (isPaussed)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
