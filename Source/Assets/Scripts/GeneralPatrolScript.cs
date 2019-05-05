using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPatrolScript : MonoBehaviour
{
    public Transform player;

    double distance = 1;
    Animator anim;

    string state = "patrol";
    public GameObject[] moveSpots;
    int curMS = 0;
    float rotSpeed = 5f;
    public float speed; // yurume/kosma hizi
    public float speedPercent; // 0'sa duruyor, 0.5'se yuruyor, 1'se kosuyor
    float accuracyMS = 1.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if (state == "patrol" && moveSpots.Length > 0)
        {
            //anim.SetBool("isInteracting", false);
            anim.SetFloat("speedPercent", speedPercent);
            //anim.SetBool("isWalking", true);
            if (Vector3.Distance(moveSpots[curMS].transform.position, transform.position) < accuracyMS)
            {
                curMS++;
                if (curMS >= moveSpots.Length)
                {
                    curMS = 0;
                }
            }

            //rotate towards waypoint
            direction = moveSpots[curMS].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }

        if (Vector3.Distance(player.position, this.transform.position) < distance && (angle < 270 || state == "interacting"))
        {
            state = "interacting";
            //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            anim.SetFloat("speedPercent", 0);
            //anim.SetBool("isWalking", false);
            //anim.SetBool("isInteracting", true);
        }
        else
        {
            anim.SetFloat("speedPercent", speedPercent);
            state = "patrol";
        }
    }
}
