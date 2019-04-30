using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuController : MonoBehaviour
{
    Animator anim;
    TaskManager taskManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        taskManagerScript = GameObject.Find("TaskManager").GetComponent<TaskManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickMenuButton()
    {
        anim.SetBool("isOpen", true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        anim.SetBool("isOpen", false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Debug.Log("Time scale 00000");
    }

    public void UnPauseGame()
    {
        Debug.Log("Time scale 111111");
        
    }

    public void OpenTasks()
    {
        taskManagerScript.ActivateDeactivateTaskManager();
    }
}
