using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SendResults : MonoBehaviour
{
    public String difficulty;
    public String codeName;
    public float gameplayTime;
    public float totalAttentionTime;
    public float activeTime;

    public float nrObjects;
    public float timeObjects;

    public float nrInventory;
    public float timeInventory;

    public float nrInstructions;
    public float timeInstructions;

    public float nrNotebook;
    public float timeNotebook;

    public float nrNoti;
    public float timeNoti;

    public float FirstPuzzleStart;
    public float NumberOfLeversInteracted;
    public float FirstPuzzleEnd;

    public float SecondPuzzleStart;
    public float NumberOfSPsInteracted; //SpherePlaceholders interacted
    public float timeOrbSelection;
    public float SecondPuzzleEnd;


    public float MoveInteractInterfaceNoti;
    public float InteractInterfaceNoti;


    private float timesSent = 0;

    private static string mailToSend = "";
    private static string mailToSendHeader = "";


    IEnumerator Post(String codeName, float gameplayTime, float totalAttentionTime, float activeTime,
                     float nrObjects, float timeObjects,
                     float nrInventory, float timeInventory,
                     float nrInstructions, float timeInstructions,
                     float nrNotebook, float timeNotebook,
                     float nrNoti, float timeNoti,
                     float firstPstart, float leversInteracted, float firstPend,
                     float secondPstart, float SPsInteracted, float timeOrbSelection, float secondPend,
                     float moveInteractInterfaceNoti, float interactInterfaceNoti, System.Action<string> onCompleted)
                     {
            string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScoClvjc2R135IkX_JfDqwgyiefPVmLVzo2LTygX7pjBdVmbg/formResponse";


            WWWForm form = new WWWForm();

            form.AddField("entry.424479294", difficulty);
            form.AddField("entry.2129862063", codeName);
            form.AddField("entry.447347864", string.Format("{0:N3}", gameplayTime));
            form.AddField("entry.1700729462", string.Format("{0:N3}", totalAttentionTime));
            form.AddField("entry.1467846298", string.Format("{0:N3}", activeTime));

            form.AddField("entry.631271591", string.Format("{0:N3}", nrObjects));
            form.AddField("entry.1037281516", string.Format("{0:N3}", timeObjects));

            form.AddField("entry.1669387425", string.Format("{0:N3}", nrInventory));
            form.AddField("entry.477570250", string.Format("{0:N3}", timeInventory));

            form.AddField("entry.885483786", string.Format("{0:N3}", nrInstructions));
            form.AddField("entry.1494527538", string.Format("{0:N3}", timeInstructions));

            form.AddField("entry.652159525", string.Format("{0:N3}", nrNotebook));
            form.AddField("entry.27797654", string.Format("{0:N3}", timeNotebook));

            form.AddField("entry.2023639691", string.Format("{0:N3}", nrNoti));
            form.AddField("entry.1312920164", string.Format("{0:N3}", timeNoti));

            form.AddField("entry.373874347", string.Format("{0:N3}", firstPstart));
            form.AddField("entry.1428133993", string.Format("{0:N3}", leversInteracted));
            form.AddField("entry.927916864", string.Format("{0:N3}", firstPend));

            form.AddField("entry.1547734252", string.Format("{0:N3}", timeOrbSelection));
    
            form.AddField("entry.657756888", string.Format("{0:N3}", secondPstart));
            form.AddField("entry.269140260", string.Format("{0:N3}", SPsInteracted));
            form.AddField("entry.92975017", string.Format("{0:N3}", secondPend));

            form.AddField("entry.1482704938", string.Format("{0:N3}", moveInteractInterfaceNoti));
            form.AddField("entry.2083460319", string.Format("{0:N3}", interactInterfaceNoti));

            timesSent ++;


            byte[] rawData = form.data;
            WWW www = new WWW(BASE_URL, rawData);
            yield return www;

            onCompleted("");

    }

    public void AfterForm(string s)
    {
        gameObject.GetComponent<Menu>().enableQuestionnaire(codeName);

    }



    public void Send(String difficulty, String codeName, float gameplayTime, float totalAttentionTime, float activeTime,
                     float nrObjects, float timeObjects,
                     float nrInventory, float timeInventory,
                     float nrInstructions, float timeInstructions,
                     float nrNotebook, float timeNotebook,
                     float nrNoti, float timeNoti,
                     float firstPstart, float leversInteracted, float firstPend,
                     float secondPstart, float SPsInteracted, float timeOrbSelection, float secondPend,
                     float moveInteractInterfaceNoti, float interactInterfaceNoti)
    {
        
        TranslateDataToString(difficulty, codeName, gameplayTime, totalAttentionTime, activeTime,
                      nrObjects, timeObjects,
                      nrInventory, timeInventory,
                      nrInstructions, timeInstructions,
                      nrNotebook, timeNotebook,
                      nrNoti, timeNoti,
                      firstPstart, leversInteracted, firstPend,
                      secondPstart, SPsInteracted, timeOrbSelection, secondPend,
                      moveInteractInterfaceNoti, interactInterfaceNoti);

        this.difficulty = difficulty;
        this.codeName = codeName;
        this.gameplayTime = gameplayTime;
        this.totalAttentionTime = totalAttentionTime;
        this.activeTime = activeTime;

        this.nrObjects = nrObjects;
        this.timeObjects = timeObjects;

        this.nrInventory = nrInventory;
        this.timeInventory = timeInventory;

        this.nrInstructions = nrInstructions;
        this.timeInstructions = timeInstructions;

        this.nrNotebook = nrNotebook;
        this.timeNotebook = timeNotebook;

        this.nrNoti = nrNoti;
        this.timeNoti = timeNoti;

        this.FirstPuzzleStart = firstPstart;
        this.NumberOfLeversInteracted = leversInteracted;
        this.FirstPuzzleEnd = firstPend;

        this.SecondPuzzleStart = secondPstart;
        this.NumberOfSPsInteracted = SPsInteracted;
        this.timeOrbSelection = timeOrbSelection;
        this.SecondPuzzleEnd = secondPend;

        this.MoveInteractInterfaceNoti = moveInteractInterfaceNoti;
        this.InteractInterfaceNoti = interactInterfaceNoti;


        if (timesSent == 0)
        {
            StartCoroutine(Post(codeName, gameplayTime, totalAttentionTime, activeTime,
                      nrObjects, timeObjects,
                      nrInventory, timeInventory,
                      nrInstructions, timeInstructions,
                      nrNotebook, timeNotebook,
                      nrNoti, timeNoti,
                      firstPstart, leversInteracted, firstPend,
                      secondPstart, SPsInteracted, timeOrbSelection, secondPend,
                      moveInteractInterfaceNoti, interactInterfaceNoti, AfterForm));
        }

    }

    public static void TranslateDataToString(String difficulty, String codeName, float gameplayTime, float totalAttentionTime, float activeTime,
                     float nrObjects, float timeObjects,
                     float nrInventory, float timeInventory,
                     float nrInstructions, float timeInstructions,
                     float nrNotebook, float timeNotebook,
                     float nrNoti, float timeNoti,
                     float firstPstart, float leversInteracted, float firstPend,
                     float secondPstart, float SPsInteracted, float timeOrbSelection, float secondPend,
                     float moveInteractInterfaceNoti, float interactInterfaceNoti)
    {
        mailToSendHeader = codeName;
        mailToSend =
                  "Difficulty: " + difficulty + "\n" +  
                  "Player Code Name: " + codeName + "\n" +
                  "Total GamePlay Time: " + gameplayTime + "\n" +
                  "Total Attention Time: " + totalAttentionTime + "\n" +
                  "Total Active Time: " + activeTime + "\n" +

                  "\n" +

                  "Nr Objects Interacted with: " + nrObjects + "\n" +
                  "Total Time spent interacting with objects: " + timeObjects + "\n" +

                   "\n" +

                  "Nr of Times Inventory Was opened: " + nrInventory + "\n" +
                  "Total Time spent with Inventory opened: " + timeInventory + "\n" +

                  "\n" +

                  "Nr of Times Instructions were opened: " + nrInstructions + "\n" +
                  "Total Time spent on instructions: " + timeInstructions + "\n" +

                  "\n" +

                  "Nr of Times Notebook was opened: " + nrNotebook + "\n" +
                  "Total Time spent on Notebook: " + timeNotebook + "\n" +

                  "\n" +

                  "Nr Notifications shown: " + nrNoti + "\n" +
                  "Total Time notifications were on screen: " + timeNoti + "\n" +

                  "\n"+

                  "First Puzzle Start: " + firstPstart + "\n" +
                  "Number of Levers interacted: " + leversInteracted + "\n" +
                  "First Puzzle End: " + firstPend + "\n" +

                  "\n" +

                  "Second Puzzle Start: " + secondPstart + "\n" +
                  "Number of SPs interacted: " + SPsInteracted + "\n" +
                  "Total Time OrbSelection were on screen: " + timeOrbSelection + "\n" +
                  "Second Puzzle End: " + secondPend + "\n" +

                  "\n"+

                  "MoveInteractInterfaceNoti: "+ moveInteractInterfaceNoti+"\n"+
                  "InteractInterfaceNoti: " + interactInterfaceNoti + "\n" 

                  ;





    }

  
 



}
