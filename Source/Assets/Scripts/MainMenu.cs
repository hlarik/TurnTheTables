using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public Button NewGame;
    public Button LoadGame;
    public Button Settings;
    public Button Credits;
    public Button Exit;
    public Button Tips;
    public Animator anim1;

    //just for try, change this
    public static int sceneIndex = 1;

    public void Start()
    {
        anim1.SetBool("isOpen", false);
        LoadGame.interactable = false;
    }


    public void ClickNewGame()
    {
        SceneManager.LoadScene("Outdoors");
    }

    public void ClickLoadGame()
    {
        if (LoadGame.IsInteractable())
        {
            //change here
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void ClickSettings()
    {

    }

    public void ClickCredits()
    {

    }

    public void ClickExit()
    {
        Application.Quit();
    }


    public void ClickTips()
    {
        anim1.SetBool("isOpen", !anim1.GetBool("isOpen"));
    }

    public void SetEnableLoad()
    {
        LoadGame.interactable = true;
    }

}
