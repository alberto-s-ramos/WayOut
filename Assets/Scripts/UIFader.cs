using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIFader : MonoBehaviour
{

    public Canvas canvas;
    private List<CanvasGroup> displayMessages;
    public int displayedNR=-1;

    public GameObject notebook;
    public GameObject note2;
    public GameObject note3;


    //Cognitive Load
    private Attention attention = new Attention(0, 0);

    void Start()
    {
        displayMessages = new List<CanvasGroup>();
        foreach (CanvasGroup cg in canvas.GetComponentsInChildren<CanvasGroup>())
        {
            displayMessages.Add(cg);
        }
    }

    public void FadeIn(int messageNR)
    {
        gameObject.GetComponent<CognitiveLoad>().addNotification();
        StopAllCoroutines();
        foreach (CanvasGroup cg in displayMessages)
        {
            StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 0f));
        }
        displayedNR = messageNR;
        CanvasGroup uiElement = displayMessages[messageNR];
        initiateTime();
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1f));
    }

    public void FadeOut()
    {
        finalizeTime();
        StartCoroutine(FadeCanvasGroupRemove(displayMessages[displayedNR], displayMessages[displayedNR].alpha, 0f));
        if (displayedNR == 12)
        {
            if (SceneManager.GetActiveScene().name.Equals("A1") || SceneManager.GetActiveScene().name.Equals("B1"))
                FadeIn(14);
            else if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
                FadeIn(15);
        }

        else
            displayedNR = -1;

       
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

        yield return new WaitForSeconds(5);

        if(displayedNR != -1 || attention.getInitialTime() != 0)
        {
            FadeOut();
        }
        /*
        if (displayedNR != -1)
        {
            if (attention.getInitialTime() != 0)
            {
                finalizeTime();
            }
           //StartCoroutine(FadeCanvasGroupRemove(displayMessages[displayedNR], displayMessages[displayedNR].alpha, 0f));
        }
        */
    }

    public IEnumerator FadeCanvasGroupRemove(CanvasGroup cg, float start, float end, float lerpTime = 0.6f)
    {
        float timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        //finalizeTime();
        while (true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }
        displayedNR = -1;
    }

    public void initiateTime()
    {
        //attention.setInitialTime(gameObject.GetComponent<CognitiveLoad>().getCurrentTime());
        gameObject.GetComponent<CognitiveLoad>().setNotiActive(true);
//        Debug.Log("Noti Poped");

    }
    public void finalizeTime()
    {
        //attention.setEndTime(gameObject.GetComponent<CognitiveLoad>().getCurrentTime());
        //gameObject.GetComponent<CognitiveLoad>().addAttentionNoti(attention);
        gameObject.GetComponent<CognitiveLoad>().setNotiActive(false);
        //Debug.Log("Noti Gone");

       // attention.resetTime();
    }

    public void OpenNotebook()
    {
        notebook.SetActive(true);
    }
    public void CloseNotebook()
    {
        notebook.SetActive(false);
    }

    public void addNote(int nr)
    {
        if(nr == 2)
        {
            note2.SetActive(true);
        }
        else if (nr == 3)
        {
            note3.SetActive(true);
        }
    }



}
