
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class RainController : MonoBehaviour
{
    private GameObject rainParentObject;
    private GameObject rainObject;
    private GameObject lightning;
    private GameObject backgroundMusic;
    public GameObject player;

 
    //For testing purposes
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MakeItRain();
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        rainParentObject = GameObject.Find("RainParent");
        backgroundMusic = GameObject.Find("BackgroundMusic");
        //The rain is inactive so that I put a active parent object to acces it
        Transform[] children = rainParentObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children)
        {
            if (t.name == "Rain")
            {
                 rainObject = t.gameObject;
            }
        }
        /////////////////
        lightning = GameObject.Find("Directional Light");

    }

    public void MakeItRain()
    {
        rainObject.SetActive(true);
        lightning.SetActive(false);
        backgroundMusic.SetActive(false);
    }

    public void MakeItStop()
    {
        rainObject.SetActive(false);
        lightning.SetActive(true);
        backgroundMusic.SetActive(true);
    }


    //Finds child object of a parent
    /*GameObject FindObject2(this GameObject parent, string name)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }*/

}
