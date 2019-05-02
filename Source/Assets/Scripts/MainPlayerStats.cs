using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerStats : MonoBehaviour
{
    GlobalController globalControllerScript;
    int friendliness;
    int strength;

    // Start is called before the first frame update
    void Start()
    {
        /*friendliness = 50;   //In a scale of 100
        strength = 50;       //In a scale of 100*/
        //friendliness = globalControllerScript.GetFriendliness();
        //strength = globalControllerScript.GetStrength();
        globalControllerScript = GameObject.Find("GameMaster").GetComponent<GlobalController>();
        friendliness = globalControllerScript.GetFriendliness();
        strength = globalControllerScript.GetStrength();
        Debug.Log("In MainPlayerStats start() | friendliness: " + friendliness + "\tstrength" + strength);
    }

    public void SetFriendliness(int friendliness)
    {
        this.friendliness = friendliness;
    }

    public int GetFriendliness()
    {
        return this.friendliness;
    }

    public void SetStrength(int strength)
    {
        this.strength = strength;
    }

    public int GetStrength()
    {
        return this.strength;
    }
}
