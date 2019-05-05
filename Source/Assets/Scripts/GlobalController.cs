using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour
{

    public struct ScenePlayerPosition
    {
        public Vector3 playerPos;
        public string sceneName;
    }

    public static GlobalController Instance;

    //Stuff to save
    int strength = 50;
    int friendliness = 50;
    Transform violetPosition;
    public List<ScenePlayerPosition> playerPos = new List<ScenePlayerPosition>();
    public List<string> finishedCutSceneTriggers = new List<string>();

    public void AddLeavePosInScene(string sceneName, Vector3 pos)
    {
        bool sceneExists = false;
        for(int i = 0; i < playerPos.Count; ++i)
        {
            if(playerPos[i].sceneName == sceneName)
            {
                sceneExists = true;
                ScenePlayerPosition temp = new ScenePlayerPosition();
                temp.sceneName = sceneName;
                temp.playerPos = pos;
                playerPos[i] = temp;
            }
        }
        if (!sceneExists)
        {
            ScenePlayerPosition temp = new ScenePlayerPosition();
            temp.sceneName = sceneName;
            temp.playerPos = pos;
            playerPos.Add(temp);
        }
    }

    public void AddFinishedCutScene(string triggerName)
    {
        finishedCutSceneTriggers.Add(triggerName);
    }

    public List<string> GetFinishedTriggers()
    {
        return finishedCutSceneTriggers;
    }

    public bool isCutSceneFinished(string triggerName)
    {
        if (finishedCutSceneTriggers != null)
            for (int i = 0; i < finishedCutSceneTriggers.Count; ++i)
            {
                Debug.Log("inside global cont: " + finishedCutSceneTriggers[i]);
            }
        else
            Debug.Log("is nulllll");
        return finishedCutSceneTriggers.Contains(triggerName);
    }

    public Vector3 GetPlayerPosOnScene(string sceneName)
    {
        for (int i = 0; i < playerPos.Count; ++i)
        {
            if (playerPos[i].sceneName == sceneName)
            {
                return playerPos[i].playerPos;
            }
        }

        return Vector3.zero;
    }

    public void SavePlayerStats()
    {
        MainPlayerStats temp = GameObject.Find("Violet").GetComponent<MainPlayerStats>();
        this.strength = temp.GetStrength();
        this.friendliness = temp.GetFriendliness();
        Debug.Log("saving player stats, saving friendliness " + friendliness + " || strength " + strength);
    }

    public int GetStrength()
    {
        return strength;
    }

    public int GetFriendliness()
    {
        return friendliness;
    }

    //Also dont forget the tasks

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            //violetPosition = GameObject.Find("");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
