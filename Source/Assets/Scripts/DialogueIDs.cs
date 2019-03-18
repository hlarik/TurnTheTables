using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIDs : MonoBehaviour
{
    // Thsi will be used when the user selects options from dialogues
    public struct SelectedDialogueID
    {
        public string dialogueName;
        public List<int> ids;
    }

    // This will state whether the selection of the user is good or bad
    public struct DialogueStatus
    {
        public int id;
        public int status;       // 0 --> nice || 1 --> okay || 2 --> neutral || 3 --> not so good || 4 --> bad
    }

    //This will be used to hold all doalgue
    public struct AllDialogues
    {
        public string dialogueName;
        public List<DialogueStatus> dialogueIdWithStatus;
    }

    public List<SelectedDialogueID> dialogues;
    public List<AllDialogues> allDialogues;

    //So the script doesnt get destroyed 
    public static DialogueIDs Instance;

    void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        dialogues = new List<SelectedDialogueID>();
        allDialogues = new List<AllDialogues>();
        AddAllDialogues();
    }

    public void AddAllDialogues()
    {
        //Add first cutscene dialogue
        
    }

    public void AddDialogue(string name)
    {
        //check whether a dialogue already exists with the same name
        foreach (SelectedDialogueID dialogue in dialogues)
        {
            if (dialogue.dialogueName == name)
            {
                Debug.Log("Dialogue with name " + name + " already exists.");
                return;
            }
        }
        SelectedDialogueID temp = new SelectedDialogueID();
        temp.dialogueName = name;
        temp.ids = new List<int>();
        dialogues.Add(temp);
        Debug.Log(name + " added");
    }

    public void AddDialogueID(string name, int ID)
    {
        foreach (SelectedDialogueID dialogue in dialogues)
        {
            if(dialogue.dialogueName == name)
            {
                if (dialogue.ids.Contains(ID))
                {
                    return;
                }
                else
                {
                    dialogue.ids.Add(ID);
                    Debug.Log("id " + ID + " added to " + name);
                    break;
                }
            }
        }

        
    }

    public List<int> GetDialogueIDs(string dialogueName)
    {
        foreach (SelectedDialogueID dialogue in dialogues)
        {
            if (dialogue.dialogueName == name)
            {
                return dialogue.ids;
            }
        }
        //Debug.Log("dialogue with name " + dialogueName + " doesn't exist");
        return null;
    } 
}
