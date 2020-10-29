using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{

    private GameObject player;
    public Item item;
    public GameObject backpack;
    private GameObject chest;
    public float radius = 5f;

    private bool isMouseOver = false;



    private Animator playerAnim;

    //Hard Mode
    public bool onSpherePlaceholder;


    //Cognitive Load
    private Attention attention;
    private GameObject gameManager;


    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.transform.GetChild(0).gameObject.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
        radius = 6.5f;
        attention = new Attention(0, 0);

        chest = GameObject.Find("Chest");

    }

    private void Update()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist > radius && isMouseOver)
        {
            finalizeTime();
        }
    }


    public void OnMouseEnter()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < radius)
        {
            initiateTime();

        }
    }

    public void OnMouseExit()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        //if (dist < radius)
        //{
                finalizeTime();
        //}
    }



    public void OnMouseOver()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < 5)
        {
            //CLICK
            if (Input.GetMouseButtonUp(0) && gameManager.GetComponent<CanInteract>().canIInteract() && Time.timeScale==1)
            {
                gameManager.GetComponent<CognitiveLoad>().addInteraction();
                PickUp();
            }
        }
        
    }


    public void PickUp()
    {
        if (gameObject.tag.Equals("Backpack"))
        {
            Inventory.instance.Unlock();
            playerAnim.Play("GrabBackpack");
            destroySelf();
            backpack.SetActive(true);
            backpack.GetComponent<AudioSource>().Play();
            //Message: [4]BackpackUnlocked
            GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(4);

        }
        else if (gameObject.tag.Equals("Key"))
        {
            if (Inventory.instance.isUnlocked())
            {
                playerAnim.Play("PickUpFront");
                chest.GetComponent<AudioSource>().Play();
                bool wasPickedUp = Inventory.instance.Add(item);
                if (wasPickedUp)
                    destroySelf();
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(0);
            }

        }
        else if(gameObject.tag.Equals("Lever"))
        {
            if (Inventory.instance.isUnlocked())
            {
                playerAnim.Play("PickUpFront");
                chest.GetComponent<AudioSource>().Play();
                bool wasPickedUp = Inventory.instance.Add(item);
                if (wasPickedUp)
                    destroySelf();
            }
            else{
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(0);
            }
        }

        else if (gameObject.tag.Equals("GlowingSphere"))
        {
            if (Inventory.instance.isUnlocked())
            {
                playerAnim.Play("PickUpFront");
                chest.GetComponent<AudioSource>().Play();
                bool wasPickedUp = Inventory.instance.Add(item);
                if (wasPickedUp)
                    destroySelf();
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(0);
            }
        }
        else if (gameObject.tag.Equals("Notebook"))
        {
            if (Inventory.instance.isUnlocked())
            {
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(7);
                playerAnim.Play("PickUpFront");
                chest.GetComponent<AudioSource>().Play();
                bool wasPickedUp = Inventory.instance.Add(item);
                if (wasPickedUp)
                    destroySelf();
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(0);
            }
        }
        else if (gameObject.tag.Equals("Note"))
        {
            if (Inventory.instance.isUnlocked() && Inventory.instance.hasItem("Notebook"))
            {
                playerAnim.Play("PickUpFront");
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(9);

                if (gameObject.name.Equals("Note2"))
                    gameManager.GetComponent<UIFader>().addNote(2);
                else if(gameObject.name.Equals("Note3"))
                    gameManager.GetComponent<UIFader>().addNote(3);
                destroySelf();
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(8);
            }
        }

        /*
         * Hard version: Blue, Green, Red, White orbs.
         */
        else if (gameObject.tag.Equals("Orb"))
        {
            if (Inventory.instance.isUnlocked())
            {
                playerAnim.Play("PickUpFront");
                chest.GetComponent<AudioSource>().Play();
                bool wasPickedUp = Inventory.instance.Add(item);

                if (wasPickedUp && !onSpherePlaceholder)
                    destroySelf();
                else if (wasPickedUp && onSpherePlaceholder)
                {
                    transform.parent.GetComponent<SpherePlaceholder_HardVersion>().setHasSphere(false);
                    transform.parent.GetComponent<SpherePlaceholder_HardVersion>().reset() ;
                    GameObject.Find("Puzzle2").GetComponent<FinalPuzzle>().OrbPickedUpFromSP();

                    gameObject.SetActive(false);

                }
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(0);
            }
        }


    }

    public void destroySelf()
    {
        finalizeTime();
        Destroy(gameObject);
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
