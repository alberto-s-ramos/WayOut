using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button removeButton;

    private GameObject player;
    private GameObject gameManager;

    public Image infoImage;

    public Sprite[] itemInfo; // icons array

    private void Start()
    {
        player = GameObject.Find("Player");
        LoadIcons();

    }

    /*
     * Loads the itemInfo sprites to an array of sprites.
     */
    public void LoadIcons()
    {
        object[] loadedIcons = Resources.LoadAll("ItemInfo", typeof(Sprite));
        itemInfo = new Sprite[loadedIcons.Length];
        for (int x = 0; x < loadedIcons.Length; x++)
        {
            itemInfo[x] = (Sprite)loadedIcons[x];
        }

    }
    public Sprite findSpriteWithName(string spriteName)
    {
        foreach(Sprite sprite in itemInfo)
        {
            if (sprite.name.Equals(spriteName))
            {
                return sprite;
            }
        }
        return null;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null; 
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }


    public void OnRemoveButton()
    {
        /*
        * Drops the removed item by spawning it infront of the player.
        */
        Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y + 4, player.transform.position.z);
        Instantiate(item.game_object, position + (player.transform.forward * 2), transform.rotation);
        if(item.name.Equals("RedOrb")||item.name.Equals("WhiteOrb")||
           item.name.Equals("BlueOrb") || item.name.Equals("GreenOrb"))
        {
            OrbInventory.instance.Remove(item);
        }
        Inventory.instance.Remove(item);
    }



    public void UseItem()
    {
        if (item != null)
        {
            if (item.name.Equals("Notebook"))
            {
                GameObject.Find("GameManager").GetComponent<UIFader>().OpenNotebook();
            }
        }
    }


    public void MouseEntered() {
        /*
         * Displays info about the item.
         */
        if (item != null)
        {
            if (item.name.Equals("Notebook"))
            {
                infoImage.GetComponent<Image>().sprite = findSpriteWithName("itemInfoNotebook");
                infoImage.GetComponent<Image>().enabled = true;
            }else if (item.name.Equals("Lever"))
            {
                infoImage.GetComponent<Image>().sprite = findSpriteWithName("itemInfoLever");
                infoImage.GetComponent<Image>().enabled = true;
            }else if (item.name.Equals("GlowingSphere") ||
                      item.name.Equals("WhiteOrb") || item.name.Equals("RedOrb") ||
                      item.name.Equals("GreenOrb") || item.name.Equals("BlueOrb"))
            {
                infoImage.GetComponent<Image>().sprite = findSpriteWithName("itemInfoOrb");
                infoImage.GetComponent<Image>().enabled = true;
            }
            else if (item.name.Equals("KeyGolden"))
            {
                infoImage.GetComponent<Image>().sprite = findSpriteWithName("itemInfoKey");
                infoImage.GetComponent<Image>().enabled = true;
            }
        }
    }

    public void MouseExit()
    {
        if (item != null)
        {
            infoImage.GetComponent<Image>().sprite = null;
            infoImage.GetComponent<Image>().enabled = false;
        }
    }

}
