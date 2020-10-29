using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpherePlaceholder_HardVersion : MonoBehaviour
{
    public string rightColor;
    private bool hasSphere;
    private bool correctColor;

    public GameObject finalButton;
    public Material material; //color
    public Material noColorMat; // no color


    public Canvas canvas;

    private Item currentSphere;
    private string currentSphereName = "";

    public GameObject pillar;
    public GameObject initialPillar;

    public int currentPos;

    private Material[] mats;

    private float spheresPlacedEasy = 0;


    void Start()
    {
        hasSphere = false;
        correctColor = false;
        currentSphere = null;
        mats = pillar.GetComponent<Renderer>().materials;
    }


    public void placeSphere(Item item)
    {
        //Disable all Children
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child.activeSelf)
                child.GetComponent<ItemPickUp>().PickUp();
            if (child != null)
            {
                child.SetActive(false);
            }
        }

        if (item.name.Equals("WhiteOrb"))
        {
            transform.GetChild(0).gameObject.SetActive(true);

        }else if(item.name.Equals("RedOrb"))
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (item.name.Equals("GreenOrb"))
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (item.name.Equals("BlueOrb"))
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }

        transform.GetComponent<AudioSource>().Play();
        hasSphere = true;
        Inventory.instance.Remove(item.name);
        currentSphereName = item.name;
        this.currentSphere = item;
        if (SceneManager.GetActiveScene().name.Equals("A1") || SceneManager.GetActiveScene().name.Equals("B1"))
        {
            if (item.name.Equals(rightColor))
            {
                verifySphere_v0(item);
                GameObject.Find("Puzzle2").GetComponent<FinalPuzzle>().placeSphereEasy();
                //RemoveInteractible();
            }
            else if (!item.name.Equals(rightColor))
            {
                reset();
            }
        }
        canvas.GetComponent<OrbInventoryUI>().closeOrbInventory();


    }

    public bool verifySphere()
    {
        if (hasSphere)
        {
            if (currentSphereName.Equals(rightColor))
            {
                correctColor = true;
                verifyColor();
                return true;
            }
            else
            {
                correctColor = false;
                return false;
            }

        }
        else
            return false;

    }
    public void verifySphere_v0(Item item)
    {
        if (hasSphere)
        {
            if (item.name.Equals(rightColor))
            {
                correctColor = true;
                verifyColor();
            }
            else
                correctColor = false;
            this.currentSphere = item;

        }

    }

    public void reset()
    {
        transform.parent.Find("Cable").GetComponent<ChangeColor>().turnOff();

        Material[] FinalButtonMaterials = finalButton.GetComponent<Renderer>().sharedMaterials;
        FinalButtonMaterials[1] = noColorMat;
        finalButton.GetComponent<Renderer>().sharedMaterials = FinalButtonMaterials;

        //finalButton.transform.GetChild(0).gameObject.SetActive(true);

        Material[] PillarMaterials = pillar.GetComponent<Renderer>().sharedMaterials;
        PillarMaterials[1] = noColorMat;
        pillar.GetComponent<Renderer>().sharedMaterials = PillarMaterials;
    }



    public void RemoveInteractible()
    {
        gameObject.GetComponent<Interactable>().RemoveInteractible();
        MonoBehaviour[] scriptsL1 = transform.Find(currentSphereName).GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scriptsL1)
        {
            Destroy(script);
        }
    }

    public void ChangeCorrectMat(Material newMat, int currentPos)
    {
        rightColor = newMat.name;
        setCurrentPosition(currentPos);
        material = newMat;
        Material[] InitialPillarMaterials = initialPillar.GetComponent<Renderer>().sharedMaterials;
        InitialPillarMaterials[1] = newMat;
        initialPillar.GetComponent<Renderer>().sharedMaterials = InitialPillarMaterials;
    }

    public void verifyColor()
    {
        transform.parent.Find("Cable").GetComponent<ChangeColor>().toggle(material);

        Material[] FinalButtonMaterials = finalButton.GetComponent<Renderer>().sharedMaterials;
        FinalButtonMaterials[1] = material;
        finalButton.GetComponent<Renderer>().sharedMaterials = FinalButtonMaterials;

        //finalButton.transform.GetChild(0).gameObject.SetActive(true);

        Material[] PillarMaterials = pillar.GetComponent<Renderer>().sharedMaterials;
        PillarMaterials[1] = material;
        pillar.GetComponent<Renderer>().sharedMaterials = PillarMaterials;
    }

    public int getCurrentPosition()
    {
        return currentPos;
    }
    public void setCurrentPosition(int newPos)
    {
        currentPos = newPos;
    }

    public bool HasSphere()
    {
        return hasSphere;
    }
    public void setHasSphere(bool has)
    {
        hasSphere = has;
    }
    public bool hasCorrectColor()
    {
        return correctColor;
    }
}
