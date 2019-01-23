using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JasperPatrol : MonoBehaviour
{
    public Transform player;

    double distance = 1;
    Animator anim;

    string state = "patrol";
    public GameObject[] moveSpots;
    int curMS = 0;
    public float rotSpeed = 0.2f;
    public float speed = 1.5f;
    public float accuracyMS = 5.0f;

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
            anim.SetBool("isInteracting", false);
            anim.SetBool("isWalking", true);
            if (Vector3.Distance(moveSpots[curMS].transform.position, transform.position) < accuracyMS)
            {
                curMS++;
                if ( curMS >= moveSpots.Length )
                {
                    curMS = 0;
                }
            }

            //rotate towards waypoint
            direction = moveSpots[curMS].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }

        if (Vector3.Distance(player.position, this.transform.position) < distance && (angle < 60 || state == "interacting"))
        {
            state = "interacting";
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            anim.SetBool("isWalking", false);
            anim.SetBool("isInteracting", true);
        }
        else
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isInteracting", false);
            state = "patrol";
        }
    }
}
