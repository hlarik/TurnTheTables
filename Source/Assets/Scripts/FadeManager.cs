using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    SpriteRenderer rend;
    // Start is called before the first frame update
    //void Start()
    //{
    //    rend = GetComponent<SpriteRenderer>();
    //    Color c = rend.material.color;
    //    c.a = 0f;
    //    rend.material.color = c;
    //}

    public void FadeIn(CanvasGroup cg)
    {
        StartCoroutine(StartFadingIn(cg));
    }

    public void FadeOut(CanvasGroup cg)
    {
        StartCoroutine(StartFadingOut(cg));
    }

    public IEnumerator StartFadingIn(CanvasGroup cg)
    {
        //Color c = rend.material.color;
        //c.a = 0f;
        //rend.material.color = c;

        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            //c = rend.material.color;
            //c.a = f;
            //rend.material.color = c;
            cg.alpha = f;
            yield return new WaitForSeconds(0.05f);
        }
        
    }

    public IEnumerator StartFadingOut(CanvasGroup cg)
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            //Color c = rend.material.color;
            //c.a = f;
            //rend.material.color = c;
            cg.alpha = f;
            yield return new WaitForSeconds(0.05f);
        }
    }




}
