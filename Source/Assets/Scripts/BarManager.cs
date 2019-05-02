using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BarManager : MonoBehaviour
{

    public RawImage currentFriendlinessBar;
    public Text ratioTextFr;
    public RawImage currentStrengthBar;
    public Text ratioTextSt;

    private double currentScoreFr;
    private double currentScoreSt;
    private double maxPoint = 100;
    private double hitPoint = 1;

    public GameObject FloatingTextObject;
    public Animator anim;
    MainPlayerStats playerStats;
    GlobalController globalControllerScript;

    // Start is called before the first frame update
    private void Start()
    {
        globalControllerScript = GameObject.Find("GameMaster").GetComponent<GlobalController>();
        playerStats = GameObject.Find("Violet").GetComponent<MainPlayerStats>();
        currentScoreFr = Convert.ToDouble(globalControllerScript.GetFriendliness());
        currentScoreSt = Convert.ToDouble(globalControllerScript.GetStrength());
        Debug.Log("In BarManager: " + currentScoreFr + "\t" + currentScoreSt);
        UpdateFriendlinessBar();
        UpdateStrengthBar();
        Debug.Log("In BarManager: " + currentScoreFr + "\t" + currentScoreSt);
    }

    //***** Friendliness Bar Control *****

    // Update the score of friendliness bar
    public void UpdateFriendlinessBar()
    {
        double ratio = currentScoreFr / maxPoint;
        currentFriendlinessBar.rectTransform.localScale = new Vector3((float)ratio, 1, 1);
        ratioTextFr.text = (ratio * 100).ToString() + '%';
    }

    // Increase friendliness
    public void IncreaseFriendliness()
    {
        currentScoreFr += hitPoint;
        if (currentScoreFr > maxPoint)
        {
            currentScoreFr = maxPoint;
        }
        else
        {
            UpdateFriendlinessBar();
            playerStats.SetFriendliness(Convert.ToInt32(currentScoreFr));
            FloatingTextObject.GetComponent<TextMeshProUGUI>().color = Color.red;
            ShowFloatingText(true);
        }
    }

    // Decrease friendliness
    public void DecreaseFriendliness()
    {
        currentScoreFr -= hitPoint;
        if (currentScoreFr < 0)
        {
            currentScoreFr = 0;
        }
        else
        {
            UpdateFriendlinessBar();
            playerStats.SetFriendliness(Convert.ToInt32(currentScoreFr));
            FloatingTextObject.GetComponent<TextMeshProUGUI>().color = Color.red;
            ShowFloatingText(false);
        }
    }

    //***** Strength Bar Control *****

    // Update the score of strength bar
    public void UpdateStrengthBar()
    {
        double ratio = currentScoreSt / maxPoint;
        currentStrengthBar.rectTransform.localScale = new Vector3((float)ratio, 1, 1);
        ratioTextSt.text = (ratio * 100).ToString() + '%';
    }

    // Increase strength
    public void IncreaseStrength()
    {
        currentScoreSt += hitPoint;
        if (currentScoreSt > maxPoint)
        {
            currentScoreSt = maxPoint;
        }
        else
        {
            UpdateStrengthBar();
            playerStats.SetStrength(Convert.ToInt32(currentScoreSt));
            FloatingTextObject.GetComponent<TextMeshProUGUI>().color = Color.blue;
            ShowFloatingText(true);
        }     
    }

    // Decrease strength
    public void DecreaseStrength()
    {
        currentScoreSt -= hitPoint;
        if (currentScoreSt < 0)
        {
            currentScoreSt = 0;
        }
        else
        {
            UpdateStrengthBar();
            playerStats.SetFriendliness(Convert.ToInt32(currentScoreSt));
            FloatingTextObject.GetComponent<TextMeshProUGUI>().color = Color.blue;
            ShowFloatingText(false);
        }
    }

    public void ShowFloatingText(bool increase)
    {
        if (increase)
        {
            FloatingTextObject.GetComponent<TextMeshProUGUI>().text = "+1";
        }
        else
        {
            FloatingTextObject.GetComponent<TextMeshProUGUI>().text = "-1";
        }      
        anim.SetTrigger("open");
    }
}


