using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public string sceneName;
    public GameObject uiObject;
    bool collision;
    GlobalController globalControllerScript;

    GameObject SceneChangerScript;

    void Start()
    {
        collision = false;
        uiObject.SetActive(false);
        SceneChangerScript = GameObject.Find("BlackFade"); //.GetComponent<LevelChangerWithFade>();
        globalControllerScript = GameObject.Find("GameMaster").GetComponent<GlobalController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && collision)
        {
            //Eger scenede yoksa onlem olarak koyuyorum 
            if (SceneChangerScript != null)
            {
                //DontDestroyOnLoad(GameObject.Find("MainCharacter")); ----> Bunu sonradan yapariz!!!
                SceneChangerScript.GetComponent<LevelChangerWithFade>().ChangeLevelWithFade(sceneName);
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
            globalControllerScript.AddLeavePosInScene(SceneManager.GetActiveScene().name, GameObject.Find("Violet").transform.position);
            /*anim.SetTrigger("FadeOut");
            SceneManager.LoadScene(sceneName);*/ //--> this part of the code is trasnsfered to SceneChanger with Fade script in Canvases --> FadeCanvas --> BlackFade
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collision = true;
            uiObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        collision = false;
        uiObject.SetActive(false);
    }

    void SaveData()
    {

    }
}
