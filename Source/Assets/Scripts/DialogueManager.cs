using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class DialogueManager : MonoBehaviour
{

    public GameObject DialogueUI;

    public Text NPCmessage;
    public Text NPCname;

    public Text[] dialogueOptions;

    public Animator animator;

    private GameObject cameraScript;

    public GameObject NPCTextField;
    public GameObject PlayerOption;

    private void Start()
    {
        cameraScript = Camera.main.gameObject;
        DialogueUI.SetActive(false);
    }

    private void OnEnable()
    {
        //DialogueUI.SetActive(false);
        InteractWithCharacter.NPCDialogue += StartDialogue;
    }


    private void OnDisable()
    {
        //DialogueUI.SetActive(true);
        InteractWithCharacter.NPCDialogue -= StartDialogue;

        if (DialogueUI != null)
        {
            EndDialogue(null);
        }
        
        /*VD.OnNodeChange -= UpdateUI;
        //VD.OnEnd -= EndDialogue;
        VD.EndDialogue();*/
    }

    void Update()
    {
        if (VD.isActive)
        {
            if(Input.anyKeyDown && !VD.nodeData.isPlayer)
            {
                VD.Next();
            }
        }
    }

    // Start is called before the first frame update
    void StartDialogue(VIDE_Assign npcDialogue)
    {
        //When dialogue starts diable camera movement and character movement
        cameraScript.GetComponent<CameraController>().disableCameraMouse();
        GameObject.Find("MainCharacter").GetComponent<PlayerController>().enabled = false;

        VD.OnNodeChange += UpdateUI;
        VD.BeginDialogue(npcDialogue);
        VD.OnEnd += EndDialogue;
        DialogueUI.SetActive(true);
        animator.SetBool("isOpen", true);

        //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAspeaking" + npcDialogue.tag);

        NPCname.text = npcDialogue.alias;
    }

    // Update is called once per frame
    void UpdateUI(VD.NodeData data)
    {
        if (data.isPlayer)
        {
            //If the player is talking, set the choices enabled and disable the npc talk text
            NPCTextField.SetActive(false);
            PlayerOption.SetActive(true);

            for (int i = 0; i < dialogueOptions.Length; ++i)
            {
                if (i < data.comments.Length)
                {
                    dialogueOptions[i].transform.parent.gameObject.SetActive(true);
                    dialogueOptions[i].text = data.comments[i];
                }
                else
                {
                    dialogueOptions[i].transform.parent.gameObject.SetActive(false);
                    //NPCmessage.text = data.comments[data.commentIndex];
                }
            }
        }
        else
        {
            NPCTextField.SetActive(true);
            PlayerOption.SetActive(false);

            NPCmessage.text = data.comments[data.commentIndex];

        }
    }

    void EndDialogue(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= EndDialogue;
        VD.EndDialogue();

        cameraScript.GetComponent<CameraController>().enableCameraMouse();
        animator.SetBool("isOpen", false);
        GameObject.Find("MainCharacter").GetComponent<PlayerController>().enabled = true;
        DialogueUI.SetActive(false);
    }

    public void PlayerOnChose(int option)
    {
        VD.nodeData.commentIndex = option;
        if (Input.GetMouseButtonUp(0))
        {
            VD.Next();
        }
    }
}
