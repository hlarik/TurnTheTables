using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource Track1, Track2, Track3;
    public int TrackSelector, TrackHistory;
    private static bool keepFadingIn, keepFadingOut;
    public static MusicController instance;

    // Start is called before the first frame update
    void Start()
    {
        TrackSelector = Random.Range(0, 3);

        if (TrackSelector == 0)
        {
            Track1.Play();
            TrackHistory = 1;
        }
        else if (TrackSelector == 1)
        {
            Track2.Play();
            TrackHistory = 2;
        }
        else if (TrackSelector == 2)
        {
            Track3.Play();
            TrackHistory = 3;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Track1.isPlaying == false && Track2.isPlaying == false && Track3.isPlaying == false)
        {
            TrackSelector = Random.Range(0, 3);

            if (TrackSelector == 0 && TrackHistory != 1)
            {
                Track1.Play();
                TrackHistory = 1;
            }
            else if (TrackSelector == 1 && TrackHistory != 2)
            {
                Track2.Play();
                TrackHistory = 2;
            }
            else if (TrackSelector == 2 && TrackHistory != 3)
            {
                Track3.Play();
                TrackHistory = 3;
            }
        }

    }

    public void FadeInCaller(float speed, float maxVolume)
    {
        instance.StartCoroutine(FadeIn(speed, maxVolume));
    }

    public void FadeOutCaller(float speed)
    {
        instance.StartCoroutine(FadeOut(speed));
    }

    IEnumerator FadeIn(float speed, float maxVolume)
    {
        keepFadingIn = true;
        keepFadingOut = false;
        Track1.volume = 0;
        float audioVolume = Track1.volume;

        while (Track1.volume < maxVolume && keepFadingIn)
        {
            audioVolume += speed;
            Track1.volume = audioVolume;
            Track2.volume = audioVolume;
            Track3.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOut(float speed)
    {
        keepFadingIn = true;
        keepFadingOut = false;
        float audioVolume = Track1.volume;

        while (Track1.volume >= speed && keepFadingIn)
        {
            audioVolume -= speed;
            Track1.volume = audioVolume;
            Track2.volume = audioVolume;
            Track3.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(transform.gameObject);
    }

    public void increaseMusicVolume()
    {
        Track1.volume = 0.3f;
        Track2.volume = 0.3f;
        Track3.volume = 0.3f;
    }

    public void lowerMusicVolume()
    {
        Track1.volume = 0.1f;
        Track2.volume = 0.1f;
        Track3.volume = 0.1f;
    }
}
