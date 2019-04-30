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

    // Start is called before the first frame update
    private void Start()
    {
        playerStats = GameObject.Find("Violet").GetComponent<MainPlayerStats>();
        currentScoreFr = Convert.ToDouble(playerStats.GetFriendliness());
        currentScoreSt = Convert.ToDouble(playerStats.GetStrength());
        UpdateFriendlinessBar();
        UpdateStrengthBar();
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
            playerStats.SetFriendliness(Convert.ToInt32(currentScoreSt));
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


