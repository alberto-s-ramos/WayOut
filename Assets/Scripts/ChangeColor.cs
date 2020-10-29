using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    /*
     * Changes the cable color for the "Lever Puzzle" easy version.
     */

    public Material matOn;
    public Material matOff;

    public bool on = false;

    void Start()
    {
      
        foreach (Renderer r in GetComponentsInChildren<Renderer>()){
            r.material = matOff;
        }
    }

    void Update()
    {
        if (on){
            foreach (Renderer r in GetComponentsInChildren<Renderer>()){
                r.material = matOn;
            }
        }else if (!on){
            foreach (Renderer r in GetComponentsInChildren<Renderer>()){
                r.material = matOff;
            }
        }
    }

    public void toggle()
    {
        if (on)
        {
            on = false;
        }
        else if (!on)
        {
            on = true;
        }
    }

    public void toggle(bool onORoff)
    {
        if (onORoff)
            on = true;
        else if (!onORoff)
            on = false;
    }

    public void toggle(Material newMatOn)
    {
        matOn = newMatOn;
        if (on)
            on = false;
        else if (!on)
            on = true;
    }
    public void turnOff()
    {
        on = false;
    }

}
