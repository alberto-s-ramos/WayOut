using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableColor : MonoBehaviour
{
    /*
     * Changes the cable color for the "Lever Puzzle" easy version.
     */

    public int nrLevers;
    public Material on;
    public Material off;

    void Start()
    {
        nrLevers = 0 ;
    }

    // Update is called once per frame
    void Update()
    {
        if (nrLevers == 0)
        {
            GetComponent<Renderer>().material = off;
        }
        else if (nrLevers > 0)
        {
            GetComponent<Renderer>().material = on;
        }
    }

    public void plusOne()
    {
        nrLevers++;
        Debug.Log("+1 to " + gameObject.name + " total nr: " + nrLevers);
    }
    public void minusOne()
    {
        nrLevers--;
        Debug.Log("-1 to " + gameObject.name + " total nr: " + nrLevers);

    }
}
