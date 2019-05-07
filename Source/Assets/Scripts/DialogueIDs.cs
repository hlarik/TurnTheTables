﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DialogueIDs : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////////
    string path = "Assets/Resources/DialogueIDs/dialogueids.txt";
    TextAsset textAsset;
    string text;
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
        textAsset = (TextAsset)Resources.Load("DialogueIDs/dialogueids", typeof(TextAsset)); // TextAsset taskAsset = Resources.Load<TextAsset>("DialogueIDs/dialogueids");
        if (textAsset == null)
            Debug.Log("TASK ASSET IS NULL");

        text = textAsset.text;

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
        ReadFileIntoArray2();
    }

    void ReadFileIntoArray2()
    {
        List<int> tempInt = new List<int>();
        AllDialogues temp = new AllDialogues();

        //Add Gariban
        temp.dialogueName = "Gariban";
        tempInt = new List<int>() { 10, 22};
        temp.badDialogues = tempInt;
        tempInt = new List<int>() { 4, 12 };
        temp.ignoredDialogues = tempInt;
        tempInt = new List<int>() { 8, 13, 16, 25};
        temp.goodDialogues = tempInt;
        allDialogues.Add(temp);

        //Add Madison
        temp.dialogueName = "Madison";
        tempInt = new List<int>() { 4 };
        temp.badDialogues = tempInt;
        tempInt = new List<int>() {};
        temp.ignoredDialogues = tempInt;
        tempInt = new List<int>() {3 };
        temp.goodDialogues = tempInt;
        allDialogues.Add(temp);

        //Add Emily
        temp.dialogueName = "Emily";
        tempInt = new List<int>() { 2};
        temp.badDialogues = tempInt;
        tempInt = new List<int>() {};
        temp.ignoredDialogues = tempInt;
        tempInt = new List<int>() { 3};
        temp.goodDialogues = tempInt;
        allDialogues.Add(temp);

        //Add Matt
        temp.dialogueName = "Matt";
        tempInt = new List<int>() { 4};
        temp.badDialogues = tempInt;
        tempInt = new List<int>() { };
        temp.ignoredDialogues = tempInt;
        tempInt = new List<int>() { 5, 6};
        temp.goodDialogues = tempInt;
        allDialogues.Add(temp);

        //Add Jannet
        temp.dialogueName = "Jannet";
        tempInt = new List<int>() { 3, 5};
        temp.badDialogues = tempInt;
        tempInt = new List<int>() { };
        temp.ignoredDialogues = tempInt;
        tempInt = new List<int>() { 7};
        temp.goodDialogues = tempInt;
        allDialogues.Add(temp);
    }

    void ReadFileIntoArray()
    {
        StringReader read = null;
        read = new StringReader(textAsset.text);

        StreamReader inp_stm = new StreamReader(path);
        bool name = true;
        bool badId = false;
        bool neutralId = false;
        bool goodId = false;
        List<int> tempInt = new List<int>();
        AllDialogues temp = new AllDialogues();

        //while (!inp_stm.EndOfStream)
        string inp_ln;
        inp_ln = read.ReadLine();
        while (inp_ln != null)
        {
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
            inp_ln = read.ReadLine();
            Debug.Log(inp_ln);
        }
        
        inp_stm.Close();
    }

    public List<int> GetBadDialogues(string dialogueName)
    {
        foreach (AllDialogues dialogue in allDialogues)
        {
            if (dialogue.dialogueName == dialogueName)
            {
                return dialogue.badDialogues;
            }
        }

        return null;
    }

    public List<int> GetIgnoredDialogues(string dialogueName)
    {
        foreach (AllDialogues dialogue in allDialogues)
        {
            if (dialogue.dialogueName == dialogueName)
            {
                return dialogue.ignoredDialogues;
            }
        }

        return null;
    }

    public List<int> GetGoodDialogues(string dialogueName)
    {
        foreach (AllDialogues dialogue in allDialogues)
        {
            if (dialogue.dialogueName == dialogueName)
            {
                return dialogue.goodDialogues;
            }
        }

        return null;
    }

    public bool DialogueExists(string dialogueName)
    {
        foreach (SelectedDialogueID dialogue in dialogues)
        {
            if (dialogue.dialogueName == dialogueName)
            {
                return true;
            }
        }

        return false;
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
            if (dialogue.dialogueName == name)
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
            if (dialogue.dialogueName == dialogueName)
            {
                return dialogue.ids;
            }
        }
        return null;
    }

    public void DeleteDialogue(string dialogueName)
    {
        foreach (SelectedDialogueID dialogue in dialogues)
        {
            if (dialogue.dialogueName == dialogueName)
            {
                dialogues.Remove(dialogue);
            }
        }
    }
}
