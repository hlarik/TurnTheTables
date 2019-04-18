using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerWithFade : MonoBehaviour
{
    public Animator anim;
    string changeToScene;

    // Start is called before the first frame update
    void Start()
    {
        changeToScene = "";
    }

    public void ChangeLevelWithFade(string levelName)
    {
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
}
