using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Menu : MonoBehaviour
{

    private List<CanvasGroup> displayMessages;
    private List<CanvasGroup> notifications;

    public Button startButton, aboutUsButton, helpButton;
    public GameObject sendResults;

    public Camera initialCam, mainCam, finalCam;
    public Canvas menu;
    public Canvas notiCanvas;
    public Image fadeImgMenu, imageFinal;
    public GameObject aboutUsText, instructions;
    public InputField inputCodeName;
    public GameObject fullScreen;

    public GameObject quitButton;

    public string difficulty = "";

    public Canvas pauseCanvas;
    public GameObject pauseButton;


    private bool started = false, finish = false;

    void Start()
    { 
        if (SceneManager.GetActiveScene().name.Equals("Tese_Scene_Intro"))
        {
            Loader load = GetComponent<Loader>();
            load.Load();
        }
       
        displayMessages = new List<CanvasGroup>();
        foreach (CanvasGroup cg in menu.GetComponentsInChildren<CanvasGroup>())
        {
            displayMessages.Add(cg);
        }
            notifications = new List<CanvasGroup>();
            foreach (CanvasGroup cg in notiCanvas.GetComponentsInChildren<CanvasGroup>())
            {
                notifications.Add(cg);
                cg.blocksRaycasts = false;
            }

       /*
        * Starts "Tese_Scene_Intro".
        */
        if (menu.isActiveAndEnabled)
        {
            fadeImgMenu.canvasRenderer.SetAlpha(1.0f);
            imageFinal.canvasRenderer.SetAlpha(0.0f);
            sendResults.GetComponent<CanvasGroup>().blocksRaycasts = false;
            if (started == false)
            {
                //BLOCKING QUESTIONNAIRE
                displayMessages[5].gameObject.SetActive(false);
                Fade(fadeImgMenu, 1.5f, 0f);
                initialCam.enabled = true;
                mainCam.enabled = false;
                finalCam.enabled = false;
            }
        }

       /*
        * Starts on "Tese_Scene" & "Tese_Scene_Hard".
        */
        else if (!menu.isActiveAndEnabled && !SceneManager.GetActiveScene().name.Equals("Tese_Scene_Intro"))
        {
          StartGameWithoutMenu();
        }

  }
    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            closeInstructions();
        }
    }


    /*
    * ------------------- TESE_SCENE_INTRO -------------------
    */

    /*
     * Invoked when the scene Tese_Scene_Intro.
     */
    public void StartGameFromIntro()
        {
        if (started == false && startButton.GetComponent<CanvasGroup>().alpha == 1)
        {
            startButton.interactable = false;
            aboutUsButton.interactable = false;
            foreach (CanvasGroup cg in displayMessages)
            {
                StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 0f));
            }
            changeRaycastOfNoti(true);
            Fade(fadeImgMenu, 6f, 1f);
            initialCam.GetComponent<Animator>().Play("ChangePos");
            StartCoroutine(CoroutineBeginChangeScene(6f));
        }
    }


    public bool hasStarted()
    {
        return started;
    }
    IEnumerator CoroutineBegin(float secs)
    {
        yield return new WaitForSeconds(secs);
        mainCam.enabled = true;
        initialCam.enabled = false;
        finalCam.enabled = false;
        Fade(fadeImgMenu, 3f, 0f);
        started = true;
        helpButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);

        GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(12);
        
        menu.gameObject.SetActive(false);
        fadeImgMenu.gameObject.SetActive(false);
    }

    IEnumerator CoroutineBeginChangeScene(float secs)
    {
        yield return new WaitForSeconds(secs);

        startButton.gameObject.GetComponent<CanvasGroup>().interactable = false;
        startButton.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        if (difficulty.Equals("A1"))
            GameObject.Find("ProgressSceneLoader").GetComponent<ProgressSceneLoader>().LoadScene("A2");
        else if (difficulty.Equals("A2"))
            GameObject.Find("ProgressSceneLoader").GetComponent<ProgressSceneLoader>().LoadScene("B1");
        else if (difficulty.Equals("B1"))
            GameObject.Find("ProgressSceneLoader").GetComponent<ProgressSceneLoader>().LoadScene("B2");
        else if (difficulty.Equals("B2"))
            GameObject.Find("ProgressSceneLoader").GetComponent<ProgressSceneLoader>().LoadScene("A1");



    }


    /*
     * ABOUT US
     */
    public void AboutUs()
    {
        if (started == false)
        {
            foreach (CanvasGroup cg in displayMessages)
            {
                StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 0f));
            }
            StartCoroutine(FadeCanvasGroup(aboutUsText.GetComponent<CanvasGroup>(), aboutUsText.GetComponent<CanvasGroup>().alpha, 1f));
        }
    }
    public void BackToMenu()
    {
        if (started == false)
        {
            StartCoroutine(FadeCanvasGroup(aboutUsText.GetComponent<CanvasGroup>(), aboutUsText.GetComponent<CanvasGroup>().alpha, 0f));
            StartCoroutine(FadeCanvasGroup(startButton.GetComponent<CanvasGroup>(), startButton.GetComponent<CanvasGroup>().alpha, 1f));
            StartCoroutine(FadeCanvasGroup(aboutUsButton.GetComponent<CanvasGroup>(), aboutUsButton.GetComponent<CanvasGroup>().alpha, 1f));
            StartCoroutine(FadeCanvasGroup(fullScreen.GetComponent<CanvasGroup>(), aboutUsButton.GetComponent<CanvasGroup>().alpha, 1f));

        }
    }


/*
 * ------------------- A1 & A2 -------------------
 */

   /*
    * START:
    * Invoked when the scene A1 or A2 is initiated.
    */
    public void StartGameWithoutMenu()
    {
        if (started == false)
        {
            startButton.interactable = false;
            aboutUsButton.interactable = false;
            foreach (CanvasGroup cg in displayMessages)
            {
                StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 0f));
            }
            changeRaycastOfNoti(true);
            StartCoroutine(CoroutineBegin(0f));
        }
    }


   /*
    * INVENTORY:
    * Closes the inventory/backpack.
    */
    public void closeInventory()
    {
        GameObject.Find("Inventory").SetActive(false);
    }


   /*
    * FINISH:
    * Functions related with the end game.
    */
    public void finishGame()
    {
        finish = true;
        gameObject.GetComponent<CanInteract>().setCanInteract(false);
        menu.gameObject.SetActive(true);
        sendResults.GetComponent<CanvasGroup>().blocksRaycasts = true;
        imageFinal.canvasRenderer.SetAlpha(0.0f);
        Fade(imageFinal, 4f, 1f);
        StartCoroutine(CoroutineEnd(4f));
    }

    public bool hasFinished()
    {
        return finish;
    }

    IEnumerator CoroutineEnd(float secs)
    {
        yield return new WaitForSeconds(secs);

        //i=4 So only the "Quit" button and "Thanks" message show up
        for(int i=4;i<6;i++)
        {
            if (!displayMessages[i].gameObject.name.Equals("FullScreen"))
            {
                // i=4 Send Results, i=5 Thanks
                StartCoroutine(FadeCanvasGroup(displayMessages[i], displayMessages[i].alpha, 1f));
            }
        }
        Fade(imageFinal, 4f, 0f);
        mainCam.enabled = false;
        initialCam.enabled = false;
        finalCam.enabled = true;
        finish = true;

        sendResults.GetComponent<CanvasGroup>().interactable = true;
        helpButton.gameObject.SetActive(false);
        helpButton.gameObject.GetComponent<Button>().interactable = false;
        pauseButton.gameObject.SetActive(false);
        pauseButton.gameObject.GetComponent<Button>().interactable = false;

        Cursor.visible = true;
    }

               /*
                * QUESTIONNAIRE:
                * Related to the end game, deals with the final panel, where
                * the randomly generated code name is displayed.
                */

                public void enableQuestionnaire(string codeName)
                {
                    StartCoroutine(sendCoroutineQuestionnaire(0.1f, codeName));
                }

                IEnumerator sendCoroutineQuestionnaire(float secs, string codeName)
                {
                    yield return new WaitForSeconds(secs);
                    sendResults.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    sendResults.GetComponent<CanvasGroup>().interactable = true;


                    for (int i = 4; i < 6; i++)
                    {
                          StartCoroutine(FadeCanvasGroup(displayMessages[i], displayMessages[i].alpha, 0f));
                        
                    }
                    for (int i = 6; i < displayMessages.Count; i++)
                    {
                       StartCoroutine(FadeCanvasGroup(displayMessages[i], displayMessages[i].alpha, 1f));
                       displayMessages[i].blocksRaycasts = true;
                       displayMessages[i].interactable = true;
        
                    }
            inputCodeName.text = codeName;
    }


    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    /*
     * FADES:
     * Fades in/out menu components.
     */
    void Fade(Image img, float seconds, float alpha)
    {
        img.CrossFadeAlpha(alpha, seconds, false);
    }


    public IEnumerator FadeCanvasGroupRemove(CanvasGroup cg, float start, float end, float lerpTime = 1f)
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


   /*
    * INSTRUCTIONS:
    * Opens/closes intructions.
    */
    public void help()
    {
        if (instructions.active == true)
            closeInstructions();
        else if (instructions.active == false)
            openInstructions();
    }

    public void openInstructions()
    {
        if( Time.timeScale == 1)
            instructions.SetActive(true);

        
    }

    public void closeInstructions()
    {
        if (Time.timeScale == 1)
            instructions.SetActive(false);
    }


   /*
    * RAYCASTS:
    * Block/unblocks raycast notifications.
    */
    public void changeRaycastOfNoti(bool state)
    {
        foreach (CanvasGroup cg in notiCanvas.GetComponentsInChildren<CanvasGroup>())
        {
            cg.blocksRaycasts = state;
        }
    }

   /*
    * DIFFICULTY:
    * Invoked by the class "Loader" to set the string "difficulty" based on the
    * data colected from Google Sheets.
    */
    public void setDifficulty(string new_difficulty)
    {
        difficulty = new_difficulty;
    }
    public int randomizeDifficulty()
    {
        return Random.Range(0, 4);
    }


    public void PauseGame()
    {
        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }


}
