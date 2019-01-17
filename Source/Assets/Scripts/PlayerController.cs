using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool isGrounded;
    public bool isCrouching;

    public float speed;
    public float walkSpeed = 0.05f;
    public float runSpeed = 0.1f;
    public float crouchSpeed = 0.025f;
    public float rotationSpeed = 3;
    public float jumpHeight = 300;

    Rigidbody rb;
    Animator anim;
    CapsuleCollider colSize;

    //collider sizes
    private float colHeight;
    private Vector3 colCenter;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        colSize = GetComponent<CapsuleCollider>();
        isGrounded = true;
        colHeight = colSize.height;
        colCenter = colSize.center;
        //print(colHeight + " " + colCenter);
	}
	
	// Update is called once per frame
	void Update () {

        //Toggle Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                isCrouching = false;
                anim.SetBool("isCrouching", false);
                colSize.height = colHeight;
                colSize.center = colCenter;
            }
            else
            {
                isCrouching = true;
                anim.SetBool("isCrouching", true);
                speed = crouchSpeed;
                colSize.height = colHeight/2;
                colSize.center = new Vector3( 0, colCenter.y/2, 0);
            }
        }

        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal") * rotationSpeed;

        transform.Translate( 0, 0, z);
        transform.Rotate(0, y, 0);

        /*if (Input.GetKey(KeyCode.A))
        {
            //rb.AddForce(-300*speed, 0, 0);
            transform.Translate(-speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //rb.AddForce(300*speed, 0, 0);
            transform.Translate(speed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //rb.AddForce(0, 0, 300*speed);
            transform.Translate(0, 0, speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //rb.AddForce(0, 0, -300*speed);
            transform.Translate(0, 0, -speed);
        }*/

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            
            rb.AddForce(0, jumpHeight, 0);
            isCrouching = false;
            isGrounded = false;
            
        }

        if (isCrouching)
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if (!isCrouching)
        {
            speed = walkSpeed;
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
    }

    void OnCollisionEnter()
    {
        isGrounded = true;
    }
}
