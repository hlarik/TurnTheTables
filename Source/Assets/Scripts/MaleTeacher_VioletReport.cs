using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaleTeacher_VioletReport : MonoBehaviour
{
    Vector3 turnSmoothVelocity;
    float turnSmoothTime = 0.2f;

    Animator anim;
    Quaternion initialRotation;
    bool turningTowardsInitialRot;
    float rotSpeed = 2f;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "Staffroom")
        {
            this.GetComponent<Animator>().SetBool("reportEnd", false);
            this.GetComponent<Animator>().SetBool("violetReporting", false);
            if (GameObject.Find("MsSusan_Chair") != null)
            {
                GameObject.Find("MsSusan_Chair").GetComponent<Animator>().SetBool("SusanSit", false);
                GameObject.Find("MsSusan_Chair").GetComponent<Animator>().SetBool("SusanStand", false);
            }
            anim.SetBool("inStaffroom", true);
        }
        else
            anim.SetBool("inStaffroom", false);

        turningTowardsInitialRot = false;
        initialRotation = this.transform.rotation;
    }

    public void activateGesture()
    {
        anim.SetTrigger("gesture");
    }

    public void turnToInitialRoattion()
    {
        turningTowardsInitialRot = true;
        //transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, initialRotation.eulerAngles, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, initialRotation, rotSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (turningTowardsInitialRot)
        {
            //Debug.Log(this.transform.rotation.eulerAngles + "\t\t");
            //transform.eulerAngles = Vector3.SmoothDamp(this.transform.rotation.eulerAngles, initialRotation.eulerAngles, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, initialRotation, rotSpeed * Time.deltaTime);
            //if (transform.rotation == initialRotation || transform.rotation.Equals(initialRotation))
            if (Vector3.Distance(transform.eulerAngles, initialRotation.eulerAngles) <= 0.01f)
            {
                turningTowardsInitialRot = false;
            }
        }
    }


}
