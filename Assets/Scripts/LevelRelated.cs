using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelRelated : MonoBehaviour
{
    public GameObject lever1, lever2, lever3, lever4, lever5, lever6, finalCable;
    public Material noglow;
    public Material glow;
    private bool canOpenDoor = false;
    //FOR HARD MODE
    public GameObject bar1, bar2, bar3;
    public bool bar1open=false, bar2open = false, bar3open = false;

    private int secondDoor = 0;
   

    public bool open()
    {
        if (secondDoor == 0)
            return false;
        else
            return true;
    }

    public void increseNumberOfTimesOpen()
    {
        secondDoor++;
    }
    public void decreaseOfTimesOpen()
    {
        secondDoor--;
    }



    public void verifyLevers()
    {
        if (lever1.GetComponent<Lever>().getState() &&
           lever2.GetComponent<Lever>().getState() &&
           lever3.GetComponent<Lever>().getState() &&
           lever4.GetComponent<Lever>().getState() &&
           lever5.GetComponent<Lever>().getState() &&
           lever6.GetComponent<Lever>().getState())
        {
            finalCable.GetComponent<ChangeColor>().toggle(true);
            UnlockDoor();
            canOpenDoor = true;
            RemoveInteractibleLever();

        }
    }

    public void UnlockDoor()
    {
        bar1.GetComponent<Animator>().Play("Bar1Open");
        bar1.GetComponent<AudioSource>().Play();

        bar2.GetComponent<Animator>().Play("Bar2Open");
        bar2.GetComponent<AudioSource>().Play();

        bar3.GetComponent<Animator>().Play("Bar3Open");
        bar3.GetComponent<AudioSource>().Play();
    }

    public bool CanOpenDoor()
    {
        return canOpenDoor;
    }

    public void RemoveInteractibleLever()
    {
     
            MonoBehaviour[] scriptsL1 = lever1.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scriptsL1)
            {
                Destroy(script);
            }
            MonoBehaviour[] scriptsL2 = lever2.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scriptsL2)
            {
                Destroy(script);
            }
            MonoBehaviour[] scriptsL3 = lever3.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scriptsL3)
            {
                Destroy(script);
            }
            MonoBehaviour[] scriptsL4 = lever4.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scriptsL4)
            {
                Destroy(script);
            }
            MonoBehaviour[] scriptsL5 = lever5.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scriptsL5)
            {
                Destroy(script);
            }
            MonoBehaviour[] scriptsL6 = lever6.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scriptsL6)
            {
                Destroy(script);
            }
        
    }

    public void updateBarState(int barNumber, bool state)
    {
        if (barNumber == 1)
            bar1open = state;
        else if (barNumber == 2)
            bar2open = state;
        else if (barNumber == 3)
            bar3open = state;
    }
    public bool getBarState(int barNumber)
    {
        if (barNumber == 1)
            return bar1open;
        else if (barNumber == 2)
            return bar2open;
        else if (barNumber == 3)
            return bar3open;
        else
            return false;
    }
    public void verifyBars()
    {
        if(bar1open && bar2open && bar3open)
        {
            finalCable.GetComponent<ChangeColor>().toggle();
            canOpenDoor = true;
        }
        else
        {
            finalCable.GetComponent<ChangeColor>().toggle(false);
            canOpenDoor = false;
        }

    }
   


}
