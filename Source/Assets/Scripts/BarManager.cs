using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{

    public RawImage currentFriendlinessBar;
    public Text ratioTextFr;
    public RawImage currentStrengthBar;
    public Text ratioTextSt;

    private double currentScoreFr = 50;
    private double currentScoreSt = 50;
    private double maxPoint = 100;
    private double hitPoint = 1;

    public GameObject FloatingTextObject;

   

    // Start is called before the first frame update
    private void Start()
    {
        UpdateFriendlinessBar();
        UpdateStrengthBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IncreaseFriendliness();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            IncreaseStrength();
        }
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
        if(currentScoreFr > maxPoint)
        {
            currentScoreFr = maxPoint;
        }
        UpdateFriendlinessBar();
        if (FloatingTextObject)
        {
            ShowFloatingText();
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
        UpdateFriendlinessBar();
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
        UpdateStrengthBar();

        if (FloatingTextObject)
        {
             ShowFloatingText();
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
        UpdateStrengthBar();
    }

    public void ShowFloatingText()
    {
        Instantiate(FloatingTextObject, transform.position, Quaternion.identity);
    }
}


