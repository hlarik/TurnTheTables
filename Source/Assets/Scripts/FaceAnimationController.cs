using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimationController : MonoBehaviour
{
    string[] characters = {"Violet", "MsSusan", "MrNoah", "Maya" , "Matt", "Madison", "Liam", "Jannet" , "Felix", "Ethan", "Emily", "Andrew"};

    public void MakeCharacterHappy(string characterName)
    {
        if (GameObject.Find(characterName) != null)
            GameObject.Find(characterName).GetComponent<Animator>().SetBool("isHappy", true);
        else
            Debug.Log("In MakeCharacterHappy - Character with name " + characterName + " doesn't exist in scene");
    }

    public void MakeCharacterSad(string characterName)
    {
        if (GameObject.Find(characterName) != null)
            GameObject.Find(characterName).GetComponent<Animator>().SetBool("isSad", true);
        else
            Debug.Log("In MakeCharacterHappy - Character with name " + characterName + " doesn't exist in scene");
    }

    public void MakeCharacterShocked(string characterName)
    {
        if (GameObject.Find(characterName) != null)
            GameObject.Find(characterName).GetComponent<Animator>().SetBool("isShocked", true);
        else
            Debug.Log("In MakeCharacterHappy - Character with name " + characterName + " doesn't exist in scene");
    }

    public void MakeCharacterNormal(string characterName)
    {
        if (GameObject.Find(characterName) != null)
        {
            GameObject.Find(characterName).GetComponent<Animator>().SetBool("isSad", false);
            GameObject.Find(characterName).GetComponent<Animator>().SetBool("isHappy", false);
            GameObject.Find(characterName).GetComponent<Animator>().SetBool("isShocked", false);
        }
        else
            Debug.Log("In MakeCharacterHappy - Character with name " + characterName + " doesn't exist in scene");
    }

    public void MakeAllCharactersHappy()
    {
        string characterName = "Violet";
        for (int characterNum = 0; characterNum < 12; ++characterNum) {
            characterName = characters[characterNum];
            if (GameObject.Find(characterName) != null)
            {
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isSad", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isHappy", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isShocked", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isHappy", true);
            }
            else
                Debug.Log("In MakeCharacterHappy - Character with name " + characterName + " doesn't exist in scene");
        }
    }

    public void MakeAllCharactersSad()
    {
        string characterName = "Violet";
        for (int characterNum = 0; characterNum < 12; ++characterNum)
        {
            characterName = characters[characterNum];
            if (GameObject.Find(characterName) != null)
            {
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isSad", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isHappy", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isShocked", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isSad", true);
            }
            else
                Debug.Log("In MakeCharacterHappy - Character with name " + characterName + " doesn't exist in scene");
        }
    }

    public void MakeAllCharactersShocked()
    {
        string characterName = "Violet";
        for (int characterNum = 0; characterNum < 12; ++characterNum)
        {
            characterName = characters[characterNum];
            if (GameObject.Find(characterName) != null)
            {
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isSad", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isHappy", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isShocked", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isShocked", true);
            }
            else
                Debug.Log("In MakeCharacterHappy - Character with name " + characterName + " doesn't exist in scene");
        }
    }

    public void MakeAllCharactersNormal()
    {
        string characterName = "Violet";
        for (int characterNum = 0; characterNum < 12; ++characterNum)
        {
            characterName = characters[characterNum];
            if (GameObject.Find(characterName) != null)
            {
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isSad", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isHappy", false);
                GameObject.Find(characterName).GetComponent<Animator>().SetBool("isShocked", false);
            }
            else
                Debug.Log("In MakeCharacterHappy - Character with name " + characterName + " doesn't exist in scene");
        }
    }
}
