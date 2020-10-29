using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Loader : MonoBehaviour
{

    public void Load()
    {
        StartCoroutine(CSVDownloader.DownloadData(AfterDownload));
    }

    public void AfterDownload(string data)
    {
        if (null == data)
        {
            Debug.LogError("Was not able to download data or retrieve stale data.");
        }
        else
        {
            StartCoroutine(ProcessData(data));
        }
    }


    public IEnumerator ProcessData(string data)
    {
        yield return new WaitForEndOfFrame();
        /*
         * Returns the value of the 2nd column of the last line ("H" or "E");
         */
        string[] lastLine = data.Split('\n');
        string[] lastMode = lastLine[lastLine.Length - 1].Split(',');

       Debug.Log(lastLine[lastLine.Length - 1]);
        Debug.Log(lastMode[1]);

        
        if (!lastMode[1].Equals("Difficulty ") && !lastMode[1].Equals(""))
        {
            //Debug.Log("Wasn't empty");
            GetComponent<Menu>().setDifficulty(lastMode[1]);
        }
        
        
        /*
         * When the Google Sheets is empty.
         */
         
        else if (lastMode[1].Equals("Difficulty ") || lastMode[1].Equals(""))
        {
            Debug.Log(" ><<<<<< Was empty");
           // GetComponent<Menu>().setDifficulty("B1");
            
            int randomDifficulty = randomizeDifficulty();
            if (randomDifficulty == 0)
                GetComponent<Menu>().setDifficulty("A1");
            else if (randomDifficulty == 1)
                GetComponent<Menu>().setDifficulty("A2");
            else if (randomDifficulty == 2)
                GetComponent<Menu>().setDifficulty("B1");
            else if (randomDifficulty == 3)
                GetComponent<Menu>().setDifficulty("B2");
            

        }
        
        

    }

    public int randomizeDifficulty()
    {
        return Random.Range(0, 4);
    }



}