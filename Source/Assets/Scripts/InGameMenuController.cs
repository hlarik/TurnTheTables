using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    Animator anim;
    TaskManager taskManagerScript;
    GameObject SceneChangerScript;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SceneChangerScript = GameObject.Find("BlackFade"); //.GetComponent<LevelChangerWithFade>();
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
       
    }

    public void UnPauseGame()
    {
       
    }

    public void OpenTasks()
    {
        taskManagerScript.ActivateDeactivateTaskManager();
    }

    public void ExitGame()
    {
        ResumeGame();
        SceneChangerScript.GetComponent<LevelChangerWithFade>().ChangeLevelWithFade("MainMenu");
    }
}
