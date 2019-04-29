using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle_AnimController : MonoBehaviour
{
    string[] characters = { "Violet", "MsSusan", "MrNoah", "Maya", "Matt", "Madison", "Liam", "Jannet", "Felix", "Ethan", "Emily", "Andrew" };

    // Start is called before the first frame update
    void Start()
    {
        MakeEveryoneCheer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeEveryoneCheer()
    {
        string characterName = "Violet";
        for (int characterNum = 0; characterNum < 12; ++characterNum)
        {
            characterName = characters[characterNum];
            if (GameObject.Find(characterName) != null)
            {
                GameObject.Find(characterName).GetComponent<Animator>().SetTrigger("isCheering");
            }
            else
                Debug.Log("In MakeCharacterHappy - Character with name " + characterName + " doesn't exist in scene");
        }
    }
}
