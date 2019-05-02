using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerWithFade : MonoBehaviour
{
    public Animator anim;
    string changeToScene;
    GlobalController globalControllerScript;
    MusicController musicControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        changeToScene = "";
        globalControllerScript = GameObject.Find("GameMaster").GetComponent<GlobalController>();
        musicControllerScript = GameObject.Find("BackgroundMusic").GetComponent<MusicController>();
        musicControllerScript.FadeInCaller(0.1f, 0.7f);
    }

    public void ChangeLevelWithFade(string levelName)
    {
        globalControllerScript.SavePlayerStats();
        musicControllerScript.FadeOutCaller(0.1f);
        if (levelName == "")
        {
            Debug.Log("No scene desfined jsadj");
        }
        else
        {
            anim.SetTrigger("FadeOut");
            changeToScene = levelName;
        }
    }

    public void ChangeLevel()
    {
        if (changeToScene == "")
        {
            Debug.Log("No scene desfined jsadj");
        }
        else
        {
            SceneManager.LoadScene(changeToScene);
        }

    }

    public void empathyFade()
    {
        anim.SetTrigger("empathySelected");
    }
}
