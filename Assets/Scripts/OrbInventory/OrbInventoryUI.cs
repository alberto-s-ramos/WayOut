using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class OrbInventoryUI : MonoBehaviour
{

    public Transform itemsParent;
    public GameObject OrbInventoryUI_;

    OrbInventory OrbInventory;

    OrbInventorySlot[] slots;

    Image[] infoImages;

    public GameObject currentSpherePlaceholder;

    private Transform player;


    void Start()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Tese_Scene_Intro"))
        {
            OrbInventory = OrbInventory.instance;
            OrbInventory.onItemChangedCallBack += UpdateUI;

            slots = itemsParent.GetComponentsInChildren<OrbInventorySlot>();

            player = GameObject.FindGameObjectWithTag("Player").transform;
        }



    }
    void Update()
    {
        if (currentSpherePlaceholder != null)
        {
            float dist = Vector3.Distance(new Vector3(player.position.x, currentSpherePlaceholder.transform.position.y, player.transform.position.z), currentSpherePlaceholder.transform.position);
     
              if (dist > 6.5)
              {
                    OrbInventoryUI_.SetActive(false);
              }
        }
    }



    public void closeOrbInventory()
    {
        OrbInventoryUI_.SetActive(!OrbInventoryUI_.activeSelf);
        //setCurrentSpherePlaceholder(null);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < OrbInventory.items.Count)
            {
                slots[i].AddItem(OrbInventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }

        }

    }

    void closeAllInfoImages()
    {
        for (int i = 0; i < infoImages.Length; i++)
        {
            infoImages[i].sprite = null;
            infoImages[i].enabled = false;
        }
    }

    public void setCurrentSpherePlaceholder(GameObject placeholder)
    {
        this.currentSpherePlaceholder = placeholder;
    }
    public GameObject getCurrentSpherePlaceholder()
    {
        return currentSpherePlaceholder;
    }
}
