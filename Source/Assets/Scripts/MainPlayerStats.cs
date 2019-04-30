using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerStats : MonoBehaviour
{
    int friendliness = 50;
    int strength = 50;

    // Start is called before the first frame update
    void Start()
    {
        /*friendliness = 50;   //In a scale of 100
        strength = 50;       //In a scale of 100*/
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
