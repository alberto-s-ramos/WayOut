using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Lever : MonoBehaviour
{
    // TRUE = LeverUP | FALSE = LeverDown
    public bool state;
    public GameObject electricity;
    public GameObject cable;

    // HARD MODE
    public GameObject lever1, lever2, lever3, lever4, lever5, lever6;


    void Start()
    {
        if (state)
        {
            GetComponent<Animator>().Play("LeverUp");
            electricity.SetActive(true);
            cable.GetComponent<ChangeColor>().toggle();
        }

    }

    public void moveLever()
    {
        if (gameObject.activeSelf)
        {
            if (state)
            {
                GetComponent<Animator>().Play("LeverDown");
                electricity.SetActive(false);
                state = false;
            }
            else if (!state)
            {
                GetComponent<Animator>().Play("LeverUp");
                electricity.SetActive(true);
                state = true;
            }
            GetComponent<AudioSource>().Play();
            cable.GetComponent<ChangeColor>().toggle();
            if (!SceneManager.GetActiveScene().name.Equals("A2") && !SceneManager.GetActiveScene().name.Equals("B2"))
            {
                GameObject.Find("LevelRelated").GetComponent<LevelRelated>().verifyLevers();
            }

        }
    }


    //Called by Interactable in Hard Mode.
    public void moveLeverHard()
    {
        moveLever();

        if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
        {
            if (gameObject.name.Equals("Lever1Placed"))
            {
                //Lever 1 only moves itself
                lever2.GetComponent<Lever>().moveLever();
            }
            else if (gameObject.name.Equals("Lever2Placed"))
            {
                //Lever 2 only moves itself
            }
            else if (gameObject.name.Equals("Lever3Placed"))
            {
                //Lever 3 moves levers: 3,6
                lever6.GetComponent<Lever>().moveLever();
            }
            else if (gameObject.name.Equals("Lever4Placed"))
            {
                //Lever 4 moves levers: 4,5
                //lever1.GetComponent<Lever_v2>().moveLever();
                lever5.GetComponent<Lever>().moveLever();

            }
            else if (gameObject.name.Equals("Lever5Placed"))
            {
                //Lever 5 moves levers: 4,5,6
                lever4.GetComponent<Lever>().moveLever();
                lever6.GetComponent<Lever>().moveLever();

            }
            else if (gameObject.name.Equals("Lever6Placed"))
            {
                //Lever 6 moves levers: 3,5,6
                lever3.GetComponent<Lever>().moveLever();
                lever5.GetComponent<Lever>().moveLever();

            }

        }

        GameObject.Find("LevelRelated").GetComponent<LevelRelated>().verifyLevers();

    }

    public bool getState()
    {
        return state;
    }

}
