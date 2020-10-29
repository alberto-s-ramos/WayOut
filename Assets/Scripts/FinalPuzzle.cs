using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FinalPuzzle : MonoBehaviour
{
    public string sequence = "";

    private float attempts = 0;
    private float buttonsPowered = 0;

    private TicToc tictoc;

    public GameObject crystals;
    private GameObject gameManager;

    /*
     * Materials
     */
    public Material transparent;
    public Material green;
    public Material blue;
    public Material white;
    public Material red;

    public List<Material> materials;

    /*
     * Sphere Placeholders
     */
    public GameObject SP1;
    public GameObject SP2;
    public GameObject SP3;
    public GameObject SP4;

    public List<GameObject> SPs;

    private float spheresPlacedEasy = 0;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
        {
             tictoc = gameObject.GetComponent<TicToc>();
        }
        gameManager = GameObject.Find("GameManager");
        materials.Add(blue); materials.Add(white);
        materials.Add(red); materials.Add(green);

        SPs.Add(SP1); SPs.Add(SP2);
        SPs.Add(SP3); SPs.Add(SP4);


    }

    public void VerifySequenceHard(string ButtonName)
    {
        if (ButtonName.Equals("Button2") && sequence.Equals("") && SP2.GetComponent<SpherePlaceholder_HardVersion>().verifySphere())
        {
            /*
             * If its the Hard Version, the player has 15seconds to complete the
             * sequence. Otherwise, it will reset.
             */
            if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
            {
                //Timer starts.
                tictoc.startCounting();
            }
            crystals.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = materials[SP2.GetComponent<SpherePlaceholder_HardVersion>().getCurrentPosition()];
            sequence = "1";
        }else if (ButtonName.Equals("Button4") && sequence.Equals("1") && SP4.GetComponent<SpherePlaceholder_HardVersion>().verifySphere())
        {
            crystals.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().material = materials[SP4.GetComponent<SpherePlaceholder_HardVersion>().getCurrentPosition()];
            sequence = "1 2";
        }else if (ButtonName.Equals("Button3") && sequence.Equals("1 2") && SP3.GetComponent<SpherePlaceholder_HardVersion>().verifySphere())
        {
            crystals.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material = materials[SP3.GetComponent<SpherePlaceholder_HardVersion>().getCurrentPosition()];
            sequence = "1 2 3";

        }else if (ButtonName.Equals("Button1") && sequence.Equals("1 2 3") && SP1.GetComponent<SpherePlaceholder_HardVersion>().verifySphere())
        {
            crystals.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[SP1.GetComponent<SpherePlaceholder_HardVersion>().getCurrentPosition()];
            sequence = "1 2 3 4";
         
            if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
            {
                tictoc.stopCounting(false);
            }
            transform.Find("FinalWall").GetComponent<AudioSource>().Play();
            transform.Find("FinalWall").GetComponent<Animator>().Play("Open");
            gameManager.GetComponent<CognitiveLoad>().triggerSecondPuzzle(false);
            GameObject.Find("GameManager").GetComponent<Menu>().finishGame();

        }
        else
        {   
            //If the players fails the sequence. 
            resetSequence();
        }
    }
    public void VerifySequence(string ButtonName)
    {
        if (ButtonName.Equals("Button2") && sequence.Equals("") && SP2.GetComponent<SpherePlaceholder_HardVersion>().hasCorrectColor())
        { 
            crystals.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = white;
            sequence = "1";
        }
        else if (ButtonName.Equals("Button4") && sequence.Equals("1") && SP4.GetComponent<SpherePlaceholder_HardVersion>().hasCorrectColor()) 
        {
            crystals.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().material = green;
            sequence = "1 2";
        }
        else if (ButtonName.Equals("Button3") && sequence.Equals("1 2") && SP3.GetComponent<SpherePlaceholder_HardVersion>().hasCorrectColor())
        {
            crystals.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material = red;
            sequence = "1 2 3";
        }
        else if (ButtonName.Equals("Button1") && sequence.Equals("1 2 3") && SP1.GetComponent<SpherePlaceholder_HardVersion>().hasCorrectColor())
        {
            crystals.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = blue;
            sequence = "1 2 3 4";
            //Open Door
            transform.Find("FinalWall").GetComponent<AudioSource>().Play();
            transform.Find("FinalWall").GetComponent<Animator>().Play("Open");

            gameManager.GetComponent<CognitiveLoad>().triggerSecondPuzzle(false);
            GameObject.Find("GameManager").GetComponent<Menu>().finishGame();

        }
        else
        {   
             //If the players fails the sequence. 
            resetSequence();
        }
    }


    public void UnlockButton()
    {
        buttonsPowered++;
        if (buttonsPowered == 4)
        {
            GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(13);
        }
    }

    public void resetSequence()
    {
        if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
        {
            tictoc.stopCounting(false);
            for (int i = 0; i < SPs.Count; i++)
            {
                SPs[i].GetComponent<SpherePlaceholder_HardVersion>().reset();
            }
            ShiftSequence();
        }
        sequence = "";
        for (int i = 0; i < crystals.transform.childCount; i++)
        {
            crystals.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = transparent;

        }

        attempts++;
        if (attempts == 3)
        {
            if (SceneManager.GetActiveScene().name.Equals("A1") || SceneManager.GetActiveScene().name.Equals("B1"))
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(11);
            else if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(14);
            attempts = 0;
        }
    }
    public  void OrbPickedUpFromSP()
    {
        if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
        {
            for (int i = 0; i < SPs.Count; i++)
            {
                SPs[i].GetComponent<SpherePlaceholder_HardVersion>().reset();
            }
        }
        sequence = "";
        for (int i = 0; i < crystals.transform.childCount; i++)
        {
            crystals.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = transparent;

        }
    }

    public void ShiftSequence()
    {
        if (SceneManager.GetActiveScene().name.Equals("A2") || SceneManager.GetActiveScene().name.Equals("B2"))
        {
            for (int i=0; i<SPs.Count; i++)
            {
                int newPosition = updatePosition(SPs[i].GetComponent<SpherePlaceholder_HardVersion>().getCurrentPosition());
                SPs[i].GetComponent<SpherePlaceholder_HardVersion>().ChangeCorrectMat(materials[newPosition], newPosition);
            }
        }
    }

    public int updatePosition(int currentPos)
    {
        int finalPosition = currentPos;
        if (currentPos == 3)
            finalPosition = 0;
        else if (currentPos < 3)
            finalPosition++;
        return finalPosition;
    }

    public void placeSphereEasy()
    {
      

        if(SP1.GetComponent<SpherePlaceholder_HardVersion>().hasCorrectColor()&&
            SP2.GetComponent<SpherePlaceholder_HardVersion>().hasCorrectColor() &&
            SP3.GetComponent<SpherePlaceholder_HardVersion>().hasCorrectColor() &&
            SP4.GetComponent<SpherePlaceholder_HardVersion>().hasCorrectColor())
        {
            GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(13);
        }
    }
}
