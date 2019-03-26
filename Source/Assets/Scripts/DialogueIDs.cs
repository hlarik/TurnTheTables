﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DialogueIDs : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////////
    string path = "Assets/Resources/DialogueIDs/dialogueids.txt";
    /*public TextAsset TextFile;
    string[] linesInFile;*/
    //////////////////////////////////////////////////////////////////////////////

    // Thsi will be used when the user selects options from dialogues
    public struct SelectedDialogueID
    {
        public string dialogueName;
        public List<int> ids;
    }

    // This will state whether the selection of the user is good or bad
    /*public struct DialogueStatus
    {
        public int id;
        public int status;       // 0 --> bad || 1 --> neutral || 2 --> good
    }*/

    //This will be used to hold all doalgue
    public struct AllDialogues
    {
        public string dialogueName;
        public List<int> badDialogues;
        public List<int> goodDialogues;
        public List<int> ignoredDialogues;
    }

    public List<SelectedDialogueID> dialogues;
    public List<AllDialogues> allDialogues;

    //So the script doesnt get destroyed 
    public static DialogueIDs Instance;

    void Start()
    {
        /*linesInFile = TextFile.text.Split('\n');
        Debug.Log(TextFile.text);*/
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
        ReadFileIntoArray();
    }

    void ReadFileIntoArray()
    {
        StreamReader inp_stm = new StreamReader(path);
        bool name = true;
        bool badId = false;
        bool neutralId = false;
        bool goodId = false;
        List<int> tempInt = new List<int>();
        AllDialogues temp = new AllDialogues();

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();

            if (inp_ln.Equals(";;"))
            {
                temp.goodDialogues = tempInt;
                allDialogues.Add(temp);
                tempInt = new List<int>();
                name = true;
                badId = false;
                neutralId = false;
                goodId = false;
            }
            else if (inp_ln.Equals(";") && badId == true && neutralId == false && goodId == false)
            {
                temp.badDialogues = tempInt;
                tempInt = new List<int>();
                name = false;
                badId = false;
                neutralId = true;
                goodId = false;
            }
            else if (inp_ln.Equals(";") && badId == false && neutralId == true && goodId == false)
            {
                temp.ignoredDialogues = tempInt;
                tempInt = new List<int>();
                name = false;
                badId = false;
                neutralId = false;
                goodId = true;
            }
            else if (name)
            {
                temp.dialogueName = inp_ln;
                name = false;
                badId = true;
                neutralId = false;
                goodId = false;
            }
            else if (badId)
            {
                tempInt.Add(Int32.Parse(inp_ln));
            }
            else if (neutralId)
            {
                tempInt.Add(Int32.Parse(inp_ln));
            }
            else if (goodId)
            {
                tempInt.Add(Int32.Parse(inp_ln));
            }
        }

        /*foreach(AllDialogues i in allDialogues)
        {
            Debug.Log("scenario name " + i.dialogueName);
            foreach (int a in i.badDialogues)
                Debug.Log("Bad dialogues = " + a);
            foreach (int a in i.ignoredDialogues)
                Debug.Log("Ignore dialogues = " + a);
            foreach (int a in i.goodDialogues)
                Debug.Log("Good dialogues = " + a);
        }*/

        inp_stm.Close();
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
        //Debug.Log(name + " added");
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
                    //Debug.Log("id " + ID + " added to " + name);
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
