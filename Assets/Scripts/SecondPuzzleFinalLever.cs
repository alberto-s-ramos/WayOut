using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPuzzleFinalLever : MonoBehaviour
{
    public bool mainDoorOpen = false;
    public bool roomDoorOpen = true;

    private int nrTimesMainDoorOpen = 0;

    public void interact()
    {
        if (!mainDoorOpen)
        {
            Debug.Log("opening main door");
            openMainDoor();
        }
        else if (mainDoorOpen)
        {
            Debug.Log("closing main door");
            closeMainDoor();
        }
       
    }

    public void openMainDoor()
    {
        GameObject.Find("SecondDoor").GetComponent<AudioSource>().Play();
        GameObject.Find("R").GetComponent<Animator>().Play("OpenR");
       // GameObject.Find("LevelRelated").GetComponent<LevelRelated>().increseNumberOfTimesOpen();
        mainDoorOpen = true;
        nrTimesMainDoorOpen++;
        /*
        if (nrTimesMainDoorOpen == 1)
        {
            GameObject.Find("LevelRelated").GetComponent<LevelRelated>().RemoveInteractibleLever();
        }
        */
        closeRoomDoor();
    }

    public void closeMainDoor()
    {
        GameObject.Find("SecondDoor").GetComponent<AudioSource>().Play();
        GameObject.Find("R").GetComponent<Animator>().Play("CloseR");
        //GameObject.Find("LevelRelated").GetComponent<LevelRelated>().increseNumberOfTimesOpen();
        mainDoorOpen = false;
        openRoomDoor();
    }

    public void closeRoomDoor()
    {
        GameObject.Find("RoomBar").GetComponent<AudioSource>().Play();
        GameObject.Find("RoomBar").GetComponent<Animator>().Play("RoomBarClose");

        GameObject.Find("RoomDoor").GetComponent<AudioSource>().Play();
        GameObject.Find("RoomDoor").GetComponent<Animator>().Play("RoomDoorClose");

        roomDoorOpen=false;
    }

    public void openRoomDoor()
    {
        GameObject.Find("RoomBar").GetComponent<AudioSource>().Play();
        GameObject.Find("RoomBar").GetComponent<Animator>().Play("RoomBarOpen");

        GameObject.Find("RoomDoor").GetComponent<AudioSource>().Play();
        GameObject.Find("RoomDoor").GetComponent<Animator>().Play("RoomDoorOpen");
        roomDoorOpen = true;

    }


}
