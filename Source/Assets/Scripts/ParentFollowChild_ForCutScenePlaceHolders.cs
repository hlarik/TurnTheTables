using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFollowChild_ForCutScenePlaceHolders : MonoBehaviour
{
    GameObject child;

    void Start()
    {
        child = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = child.transform.position;
        this.transform.rotation = child.transform.rotation;
    }
}
