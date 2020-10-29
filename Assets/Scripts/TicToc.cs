using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TicToc : MonoBehaviour
{
    public float currentTime=0f;
    public float startingTime=20f;

    public GameObject TimerTF;
    private CanvasGroup cg;

    public bool startTimer = false;
    private FinalPuzzle fp;

    private bool playTicToc = false;
    public GameObject soundObject;


    void Start()
    {
        currentTime = startingTime;
        cg = TimerTF.GetComponent<CanvasGroup>();
        fp = gameObject.GetComponent<FinalPuzzle>();
    }

    void Update()
    {
        if (startTimer)
        {
            currentTime -= Time.deltaTime;
            TimerTF.GetComponent<TextMeshProUGUI>().SetText(currentTime.ToString("0"));
            if (currentTime <= 0)
            {
                stopCounting(true);
            }
        }
    }

    /*
     * Called when the first button of Puzzle 2 is pressed.
     */
    public void startCounting()
    {
        startTimer = true;
        FadeIn();
        playTicToc = true;
        StartCoroutine(PlayTicTocSound());
    }

    /*
     * Called when the timer ends / the player fails the sequence / the player gets the sequence right
     */
    public void stopCounting(bool resetTime)
    {
        startTimer = false;
        playTicToc = false;
        FadeOut();
        currentTime = startingTime;
        if (resetTime)
        {
            fp.resetSequence();
        }
    }


    IEnumerator PlayTicTocSound()
    {
        while (playTicToc)
        {
            if (soundObject.GetComponent<AudioSource>().enabled == true)
            {
                AudioSource myAudioSource = soundObject.GetComponent<AudioSource>();
                myAudioSource.PlayOneShot(myAudioSource.clip);
                yield return new WaitForSeconds(1);
            }
        }
    }

    /*
     * Fade In / Out the textfield with the clock
     */
    public void FadeIn()
    {    
        StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 1f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 0f));
    }


    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.6f)
    {
        float timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }
    }

   
}
