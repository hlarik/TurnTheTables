using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerStats : MonoBehaviour
{
    int friendliness;
    int strength;

    // Start is called before the first frame update
    void Start()
    {
        friendliness = 5;   //In a scale of 10
        strength = 5;       //In a scale of 10
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
