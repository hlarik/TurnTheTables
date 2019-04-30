
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class FirstScenarioTrigger : MonoBehaviour
{
    public GameObject player;
    public PlayableDirector pd;
    //public TimelineAsset timeline;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pd.Play();
            Destroy(this);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        //timeline = GetComponent<TimelineAsset>();
        pd = GetComponent<PlayableDirector>();
    }

}
