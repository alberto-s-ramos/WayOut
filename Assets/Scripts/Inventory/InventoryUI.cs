using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;
    public GameObject inventoryUI;
    public GameObject Notebook;

    Inventory inventory;

    InventorySlot[] slots;

    Image[] infoImages;


    void Start()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Tese_Scene_Intro"))
        {
            inventory = Inventory.instance;
            inventory.onItemChangedCallBack += UpdateUI ;

            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
            infoImages = new Image[slots.Length];
            for (int i=0; i<slots.Length;i++)
            {
                infoImages[i] = slots[i].transform.Find("InfoImage").GetComponent<Image>();
            }

        }

    }

    void Update()
    {
        if (Input.GetButtonUp("Inventory") && Inventory.instance.isUnlocked() && Time.timeScale==1)
        {
            Debug.Log("INVENTORY<<<<<<<<<");
            closeAllInfoImages();
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            if (Notebook.activeSelf)
            {
                Notebook.SetActive(!Notebook.activeSelf);
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            closeAllInfoImages();
            inventoryUI.SetActive(false);
            if (Notebook.activeSelf)
            {
                Notebook.SetActive(!Notebook.activeSelf);
            }
        }
        
    }

    public void closeInventory()
    {
        if (Inventory.instance.isUnlocked())
        {
            closeAllInfoImages();
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            if (Notebook.activeSelf)
            {
                Notebook.SetActive(!Notebook.activeSelf);
            }
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }

        }

    }

    void closeAllInfoImages()
    {
        for(int i=0; i < infoImages.Length; i++)
        {
            infoImages[i].sprite=null;
            infoImages[i].enabled = false;
        }
    }
}
