using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ReactToMeanGirlCutScene : MonoBehaviour
{
    public PlayableDirector pd;
    public GameObject kotuKız;

    private GameObject cinemachineParent;
    private GameObject cinemachine;

    private GameObject backgroundMusic;

    bool isStart = false;
    bool isFinieshed = false;
    

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GameObject.Find("BackgroundMusic");

        pd = GetComponent<PlayableDirector>();

        cinemachineParent = GameObject.Find("CinemachineCamera");
        cinemachine = GameObject.Find("CM");

        Transform[] children1 = cinemachineParent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children1)
        {
            if (t.name == "CM")
            {
                cinemachine = t.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Cutscene bitince
        if (pd.state != PlayState.Playing && isStart)
        {
            isFinieshed = true;
            backgroundMusic.GetComponent<MusicController>().increaseMusicVolume();
            kotuKız.SetActive(false);

        }
    }

    public void PlayCutScene()
    {
        backgroundMusic.GetComponent<MusicController>().lowerMusicVolume();
        isStart = true;
        kotuKız.SetActive(true);
        cinemachine.SetActive(true);
        pd.Play();

    }

    public bool cutSceneFinishedCheck()
    {
        return isFinieshed;
    }
}
