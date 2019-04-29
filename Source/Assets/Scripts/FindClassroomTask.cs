using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClassroomTask : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("GaneMaster").GetComponent<GlobalController>().isCutSceneFinished(this.name))
            Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("TaskPopupCanvas").transform.GetChild(0).GetComponent<InGameMenuController>().ClickMenuButton();
        }
        Destroy(this);
        GameObject.Find("GaneMaster").GetComponent<GlobalController>().AddFinishedCutScene(this.name);
    }


}
