using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class InnerVoiceFeedbackManager : MonoBehaviour
{

    /// dialogue system
    public delegate void NPCEvent(VIDE_Assign dialogue);
    public static NPCEvent NPCDialogue;
    int selectedButton;


    // Start is called before the first frame update
    void Start()
    {
        selectedButton = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Ignore_Post_Cutscene()
    {
        //Check if we vd is already assigned, burda bir sorun vaaaaar!!!! cunku zaten dialog varken tekrar cagiriyor
        if (!VD.isActive)
        {
            if (this.GetComponent<VIDE_Assign>() == null)
                Debug.Log("No dialogue assigned");
            else
            {
                selectedButton = 0;
                NPCDialogue(this.GetComponent<VIDE_Assign>());
            }
        }
    }

    public void ReportToAnAdult()
    {

    }

    public void Empathize()
    {

    }

    public void Talk()
    {

    }

    public void SetSelectedIndex(int index)
    {
        selectedButton = index;
    }

    public int GetSelectedIndex()
    {
        // -1 --> if not assigned
        // 0 --> Ignore
        // 1 --> Report
        // 2 --> Emprthize
        // 3 --> Talk
        return selectedButton;
    }
}
