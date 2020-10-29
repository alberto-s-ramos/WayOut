using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanInteract : MonoBehaviour
{
    /*
     * When Inventory/Notebook/Instructions is opened, the player cannot interact with objects.
     */

    public GameObject inventory, notebook, instructions, orbSelection;

    bool canInteract = false;

    void Update()
    {
        if (inventory.activeSelf || notebook.activeSelf || instructions.activeSelf || orbSelection.activeSelf)
        {
            canInteract = false;
        }
        else if(!inventory.activeSelf && !notebook.activeSelf && !instructions.activeSelf && !orbSelection.activeSelf)
        {
            canInteract = true;
        }
  
    }

    public bool canIInteract()
    {
        return canInteract;
    }

    public void setCanInteract(bool state)
    {
        canInteract = state;
    }
    

    
}
