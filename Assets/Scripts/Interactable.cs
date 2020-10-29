using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Interactable : MonoBehaviour
{

    public float radius;
    private Transform player;
    private Animator playerAnim;

    private bool isMouseOver = false;

    private bool eigibleforclick = false;

    //For last puzzle only:
    public Material dungeonMat;
    public GameObject finalButton;
    public GameObject orbInventory;
    public GameObject spherePlaceholder;
    private GameObject CanvasForOrbInv;


    //Cognitive Load
    private Attention attention;
    private GameObject gameManager;
    private int NRTimesSecondDoorOpened = 0;
    private int NRTimesFirstDoorOpened = 0;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerAnim = player.transform.GetChild(0).gameObject.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        radius = 6.5f;
        attention = new Attention(0, 0);
        CanvasForOrbInv = GameObject.Find("Canvas");

        if (tag.Equals("LeverPlaceholder"))
        {
            if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                RemoveInteractible();
            }
        }
    }
    private void Update()
    {
        float dist = Vector3.Distance(new Vector3(player.position.x, this.transform.position.y, player.position.z), this.transform.position);
        if (dist > radius && isMouseOver)
        {
            finalizeTime();
            if (tag.Equals("FinalButton"))
                eigibleforclick = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    public void OnMouseEnter()
    {

        float dist = Vector3.Distance(new Vector3(player.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < radius )
        {
            initiateTime();
            if (tag.Equals("FinalButton"))
                eigibleforclick = true;
        }
    }

    public void OnMouseExit()
    {
        float dist = Vector3.Distance(new Vector3(player.position.x, this.transform.position.y, player.position.z), this.transform.position);
        finalizeTime();
        if (tag.Equals("FinalButton"))
            eigibleforclick = false;

    }

    public void OnMouseOver()
    {
        float dist = Vector3.Distance(new Vector3(player.position.x, this.transform.position.y, player.position.z), this.transform.position);
        if (dist < radius)
        {
            //CLICK 
            if (Input.GetMouseButtonUp(0) && gameManager.GetComponent<CanInteract>().canIInteract())
            {
                gameManager.GetComponent<CognitiveLoad>().addInteraction();
                Interact();
            }
        }

    }

    public void Interact()
    {
        if (tag.Equals("Lever"))
        {
            gameManager.GetComponent<CognitiveLoad>().addLeverInteraction();
            if (SceneManager.GetActiveScene().name.Equals("A1") || SceneManager.GetActiveScene().name.Equals("B1"))
                GetComponent<Lever>().moveLever();
            else if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
                GetComponent<Lever>().moveLeverHard();

        }
        else if (tag.Equals("DoorLever"))
        {
            playerAnim.Play("LeverPush");
            GameObject.Find("DoorLever").GetComponent<AudioSource>().Play();
            GameObject.Find("DoorLeverButton").GetComponent<Animator>().Play("ButtonPush", -1, 0);
          
            if (GameObject.Find("LevelRelated").GetComponent<LevelRelated>().CanOpenDoor())
            {
                if (SceneManager.GetActiveScene().name.Equals("A1") || SceneManager.GetActiveScene().name.Equals("B1"))
                {
                    GameObject.Find("SecondDoor").GetComponent<AudioSource>().Play();
                    GameObject.Find("R").GetComponent<Animator>().Play("OpenR");
                    GameObject.Find("LevelRelated").GetComponent<LevelRelated>().increseNumberOfTimesOpen();
                    RemoveInteractible();
                }
                else if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
                {
                    GetComponent<SecondPuzzleFinalLever>().interact();
                }
                if (NRTimesSecondDoorOpened == 0)
                {
                    gameManager.GetComponent<CognitiveLoad>().triggerFirstPuzzle(false);
                    gameManager.GetComponent<CognitiveLoad>().triggerSecondPuzzle(true);
                    NRTimesSecondDoorOpened++;
                }
            }
            else if (!GameObject.Find("LevelRelated").GetComponent<LevelRelated>().open())
            {
                //Message: [3]DoorBlocked
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(3);
            }
            //else if (GameObject.Find("LevelRelated").GetComponent<LevelRelated>().open()) { }
        }
        else if (tag.Equals("LeverPlaceholder"))
        {
            if (Inventory.instance.hasItem("Lever"))
            {

                GetComponent<AudioSource>().Play();
                transform.GetChild(0).gameObject.SetActive(true);
                playerAnim.Play("LeverPush");
                Inventory.instance.Remove("Lever");
                RemoveInteractible();
                                  
            }
            else
            {
                //Message: [2]MissingLever
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(2);
            }
        }
        else if (tag.Equals("DoorLock"))
        {
            if (Inventory.instance.hasItem("KeyGolden"))
            {
                transform.GetChild(0).gameObject.SetActive(true);
                playerAnim.Play("OpenDoor");
                GameObject.Find("FirstDoor").GetComponent<Animator>().Play("FirstDoorOpen");
                GameObject.Find("FirstDoor").GetComponent<AudioSource>().Play();
                Inventory.instance.Remove("KeyGolden");
                if (NRTimesFirstDoorOpened == 0)
                {
                    gameManager.GetComponent<CognitiveLoad>().triggerFirstPuzzle(true) ;
                    NRTimesSecondDoorOpened++;
                }
                RemoveInteractible();
            }
            else
            {
                playerAnim.Play("DoorLocked");
                GetComponent<AudioSource>().Play();
                //Message: [1]FindKey
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(1);
            }
        }
        else if (tag.Equals("SpherePlaceholder"))
        {
            gameManager.GetComponent<CognitiveLoad>().addOrbInteraction();
            CanvasForOrbInv.GetComponent<OrbInventoryUI>().setCurrentSpherePlaceholder(this.gameObject);
            orbInventory.SetActive(true);
            

        }
        else if (tag.Equals("FinalButton") && eigibleforclick)
        {
           if (spherePlaceholder.GetComponent<SpherePlaceholder_HardVersion>().HasSphere())
            {
                playerAnim.Play("LeverPush");
                transform.GetComponent<AudioSource>().Play();
                transform.GetComponent<Animator>().Play("Press", -1, 0);
                if(SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
                    GameObject.Find("Puzzle2").GetComponent<FinalPuzzle>().VerifySequenceHard(transform.name);
                else if (SceneManager.GetActiveScene().name.Equals("A1") || SceneManager.GetActiveScene().name.Equals("B1"))
                    GameObject.Find("Puzzle2").GetComponent<FinalPuzzle>().VerifySequence(transform.name);

            }
            else
            {
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(6);
            }
            eigibleforclick = false;
        }

    
    }

    public void RemoveInteractible()
    {
        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
        finalizeTime();
        foreach (MonoBehaviour script in scripts)
        {
            Destroy(script);
        }
    }


    /*
     * Timestamps for the interaction times. Used to calculate Cognitive Load.
     */

    public void initiateTime()
    {
        isMouseOver = true;
        gameManager.GetComponent<CognitiveLoad>().setInteractionActive(true);



    }
    public void finalizeTime()
    {
        isMouseOver = false;
        gameManager.GetComponent<CognitiveLoad>().setInteractionActive(false);

        attention.resetTime();
    }




}
