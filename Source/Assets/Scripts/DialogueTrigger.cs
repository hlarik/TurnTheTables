using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject cutScene;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayableDirector pd = cutScene.GetComponent<PlayableDirector>();
            if(pd != null)
            {
                pd.Play();
            }
            /*TriggerDialogue();*/
            Destroy(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        
    }
}

