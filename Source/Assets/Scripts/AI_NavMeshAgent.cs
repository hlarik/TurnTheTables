using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_NavMeshAgent : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    Vector3 point;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            setPositionToMove();
        }
    }

    public void setPositionToMove()
    {
        point = new Vector3(-0.4f, 1.24498f, 13.8f);
    }
}
