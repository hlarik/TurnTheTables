using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 2;
    public float runSpeed = 6;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    Animator animator;
    Transform cameraT;

    public string name;
    GlobalController globalControllerScript;

    void Start()
    {
        //DontDestroyOnLoad(this);
        transform.SetParent(this.transform, true);
        //Load data from Global control object TODO
        /*
         * 
         * */
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        //animator.SetBool("isSad", true);

        //Set cameras accordingly
        if (GameObject.Find("CinemachineVirtualCameras") != null)
            GameObject.Find("CinemachineVirtualCameras").SetActive(false);

        //Get where player left off
        globalControllerScript = GameObject.Find("GameMaster").GetComponent<GlobalController>();
        Vector3 temp = globalControllerScript.GetPlayerPosOnScene(SceneManager.GetActiveScene().name);
        if(temp != Vector3.zero)
            this.transform.position = temp;

    }

    /*
     * TODO : Save the data to the global object
     * */


    /*void PlayerIsComingBack()
    {
        // Player comes back
        this.transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
    }*/

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
    }


}
