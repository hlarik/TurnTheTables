using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public string sceneName;
    public GameObject uiObject;
    bool collision;

    void Start()
    {
        collision = false;
        uiObject.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && collision )
        {
            //DontDestroyOnLoad(GameObject.Find("MainCharacter")); ----> Bunu sonradan yapariz!!!
            SceneManager.LoadScene(sceneName);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") )
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
}
