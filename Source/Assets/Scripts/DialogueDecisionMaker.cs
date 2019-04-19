using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDecisionMaker : MonoBehaviour
{

    List<int> dialoguesIds;
    DialogueIDs dialogueIDManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogueIDManager = GameObject.Find("DialogueIDManager").GetComponent<DialogueIDs>();
    }

    public int NumOfIgnoredIds(string dialogueName)
    {
        List<int> ignoredIds = dialogueIDManager.GetIgnoredDialogues(dialogueName);     //The original ignroed data in this dialogue
        List<int> selectedIds = dialogueIDManager.GetDialogueIDs(dialogueName);         //This is the one selected by the player
        int ignoredIdNum = 0;
        foreach (int id in selectedIds)
        {
            if (ignoredIds.Contains(id))
            {
                ignoredIdNum++;
            }
        }
        return ignoredIdNum;
    }

    public int NumOfGoodIds(string dialogueName)
    {
        List<int> goodIds = dialogueIDManager.GetGoodDialogues(dialogueName);     //The original ignroed data in this dialogue
        List<int> selectedIds = dialogueIDManager.GetDialogueIDs(dialogueName);         //This is the one selected by the player
        int goodIdNum = 0;
        foreach (int id in selectedIds)
        {
            if (goodIds.Contains(id))
            {
                goodIdNum++;
            }
        }
        return goodIdNum;
    }

    public int NumOfBadIds(string dialogueName)
    {
        List<int> badIds = dialogueIDManager.GetBadDialogues(dialogueName);         //The original ignroed data in this dialogue
        List<int> selectedIds = dialogueIDManager.GetDialogueIDs(dialogueName);         //This is the one selected by the player
        int badIdNum = 0;
        Debug.Log("Entered here " + dialogueName);
        if (badIds != null && selectedIds != null)
        {
            foreach (int id in selectedIds)
            {
                if (badIds.Contains(id))
                {
                    badIdNum++;
                }
            }
        }
        Debug.Log("Entered here " + dialogueName + "Bad id's: " + badIdNum);
        return badIdNum;
    }


}
