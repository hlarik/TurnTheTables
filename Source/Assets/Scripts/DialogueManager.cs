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

    Camera rawImageCamera;
    Texture renderToTextureImage;

    public AudioSource audioSource;

    GameObject DialogueIDManager;

    string currentDialogueName;

    bool openAgaian;
    DialogueDecisionMaker dialogueEventHandler;
    PlayerController PlayerControllerScript;
    DialogueDecisionMaker postCutSceneDecisionMakerScript;
    FaceAnimationController faceControllerScript;
    RainController rainScript;
    BarManager barManagerScript;
    int prevID;     //This is for when increasing strength and friendliness, so it doesnt count the same id twice

    private void Start()
    {
        DialogueIDManager = GameObject.Find("DialogueIDManager");
        cameraScript = Camera.main.gameObject;
        DialogueUI.SetActive(false);
        currentDialogueName = "";
        openAgaian = true;
        dialogueEventHandler = GameObject.Find("PostDialogueEventHandler").GetComponent<DialogueDecisionMaker>();
        PlayerControllerScript = GameObject.Find("Violet").GetComponent<PlayerController>();
        postCutSceneDecisionMakerScript = GameObject.Find("PostDialogueEventHandler").GetComponent<DialogueDecisionMaker>();
        barManagerScript = GameObject.Find("BarManager").GetComponent<BarManager>();
        faceControllerScript = new FaceAnimationController();
        rainScript = GameObject.Find("RainParent").GetComponent<RainController>();
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
        if (cameraScript != null)
            cameraScript.GetComponent<CameraController>().enableCameraMouse();

        animator.SetBool("isOpen", false);
        if (PlayerControllerScript != null)
            PlayerControllerScript.enabled = true;
        /*VD.OnNodeChange -= UpdateUI;
        //VD.OnEnd -= EndDialogue;
        VD.EndDialogue();*/
    }

    private void Update()
    {
        if (VD.isActive)
        {
            //aklimiza gelirse duzletelim
            if (Input.GetKeyDown(KeyCode.Space) && !VD.nodeData.isPlayer)
            {
                VD.Next();
                //VD.SetNode(0); //directly pass to node bilmem kac
            }
        }
    }

    // Start is called before the first frame update
    void StartDialogue(VIDE_Assign npcDialogue)
    {
        //When dialogue starts diable camera movement and character movement
        cameraScript.GetComponent<CameraController>().disableCameraMouse();

        if( PlayerControllerScript != null  )
            PlayerControllerScript.enabled = false;
        currentDialogueName = npcDialogue.assignedDialogue;
        
        //If dialogue already exists
        if ( !DialogueIDManager.GetComponent<DialogueIDs>().DialogueExists(currentDialogueName) )
            DialogueIDManager.GetComponent<DialogueIDs>().AddDialogue(currentDialogueName);

        VD.OnNodeChange += UpdateUI;

        VD.BeginDialogue(npcDialogue);

        VD.OnEnd += EndDialogue;
        DialogueUI.SetActive(true);
        animator.SetBool("isOpen", true);



        //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAspeaking" + npcDialogue.tag);

        //NPCname.text = npcDialogue.alias;
    }

    // Update is called once per frame
    void UpdateUI(VD.NodeData data)
    {
        NPCname.text = data.tag;

        //Make deciisons according to the player's actions
        if (data != null &&
            DialogueIDManager.GetComponent<DialogueIDs>().GetBadDialogues(currentDialogueName) != null &&
            DialogueIDManager.GetComponent<DialogueIDs>().GetIgnoredDialogues(currentDialogueName) != null &&
            DialogueIDManager.GetComponent<DialogueIDs>().GetGoodDialogues(currentDialogueName) != null &&
            prevID != data.nodeID)
        //!DialogueIDManager.GetComponent<DialogueIDs>().GetDialogueIDs(currentDialogueName).Contains(data.nodeID))
        {
            prevID = data.nodeID;
            if (DialogueIDManager.GetComponent<DialogueIDs>().GetBadDialogues(currentDialogueName).Contains(data.nodeID))
            {
                GameObject.Find("Violet").GetComponent<MainPlayerStats>().SetStrength(GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetStrength() - 1);
                GameObject.Find("Violet").GetComponent<MainPlayerStats>().SetFriendliness(GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetFriendliness() - 1);
                Debug.Log("Player's Friendliness: " + GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetFriendliness());
                Debug.Log("Player's Strength: " + GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetStrength());
            }
            if (DialogueIDManager.GetComponent<DialogueIDs>().GetIgnoredDialogues(currentDialogueName).Contains(data.nodeID))
            {
                GameObject.Find("Violet").GetComponent<MainPlayerStats>().SetStrength(GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetStrength() - 1);
                Debug.Log("Player's Friendliness: " + GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetFriendliness());
                Debug.Log("Player's Strength: " + GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetStrength());
            }
            if (DialogueIDManager.GetComponent<DialogueIDs>().GetGoodDialogues(currentDialogueName).Contains(data.nodeID))
            {
                GameObject.Find("Violet").GetComponent<MainPlayerStats>().SetStrength(GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetStrength() + 1);
                barManagerScript.IncreaseStrength();
                Debug.Log("Player's Friendliness: " + GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetFriendliness());
                Debug.Log("Player's Strength: " + GameObject.Find("Violet").GetComponent<MainPlayerStats>().GetStrength());
                rainScript.MakeItStop();
                faceControllerScript.MakeAllCharactersHappy();
            }
        }

        DialogueIDManager.GetComponent<DialogueIDs>().AddDialogueID(currentDialogueName, data.nodeID);

        if (data.tag == "Violet")
        {
            rawImageCamera = (Camera)GameObject.FindGameObjectWithTag("Player").transform.Find("frontcamera").gameObject.GetComponent<Camera>();
        }
        else
        {
            rawImageCamera = GameObject.FindGameObjectWithTag(VD.nodeData.tag).transform.Find("frontcamera").gameObject.GetComponent<Camera>();
        }
        renderToTextureImage = rawImageCamera.targetTexture;
        DialogueUI.transform.Find("RawImage").GetComponent<RawImage>().texture = renderToTextureImage;

        if (data.audios[data.commentIndex] != null)
        {
            audioSource.Stop();
            audioSource.clip = data.audios[data.commentIndex];
            audioSource.Play();
        }
        else
            audioSource.Stop();

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

            StopAllCoroutines();
            StartCoroutine(TypeSentence(data.comments[data.commentIndex]));

            //NPCmessage.text = data.comments[data.commentIndex];

        }
    }

    void EndDialogue(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= EndDialogue;
        VD.EndDialogue();

        if (cameraScript != null)
            cameraScript.GetComponent<CameraController>().enableCameraMouse();

        animator.SetBool("isOpen", false);
        if (PlayerControllerScript != null)
            PlayerControllerScript.enabled = true;
        DialogueUI.SetActive(false);

        CheckDialogueEndStatus(currentDialogueName);

        //If bad deciisons were made during the dialogue
        if (postCutSceneDecisionMakerScript.NumOfBadIds(currentDialogueName) > 0)
        {
            faceControllerScript.MakeAllCharactersSad();
            rainScript.MakeItRain();
            DialogueIDManager.GetComponent<DialogueIDs>().DeleteDialogue(currentDialogueName);
        }
        currentDialogueName = "";
    }

    public void PlayerOnChose(int option)
    {
        VD.nodeData.commentIndex = option;
        if (Input.GetMouseButtonUp(0))
        {
            VD.Next();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        NPCmessage.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            NPCmessage.text += letter;
            yield return null;
        }
    }

    //Check what to do after dialogue
    public void CheckDialogueEndStatus(string dialogueName)
    {
        if (dialogueName == "InnerVoiceFeedback")
        {
            if (VD.isActive) //??
            {
                if (openAgaian && VD.nodeData.nodeID == 0) //If it is the ignore case
                    GameObject.Find("CutSceneTrigger_1").GetComponent<CutSceneManager>().OpenDecisionCanvas();

                if (VD.nodeData.nodeID == 0)
                {
                    openAgaian = !openAgaian;
                }
            }
        }
    }
}
