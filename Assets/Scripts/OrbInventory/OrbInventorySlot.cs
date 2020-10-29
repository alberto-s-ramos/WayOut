using UnityEngine;
using UnityEngine.UI;

public class OrbInventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;

    private GameObject player;
    private Animator playerAnim;
    private GameObject gameManager;

    private GameObject currentSpherePlaceholder;


    public Sprite[] itemInfo; // icons array

    private void Start()
    {
        player = GameObject.Find("Player");
        playerAnim = player.transform.GetChild(0).gameObject.GetComponent<Animator>();
        currentSpherePlaceholder = GameObject.Find("Canvas").GetComponent<OrbInventoryUI>().getCurrentSpherePlaceholder();
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
        foreach (Sprite sprite in itemInfo)
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
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }


    public void OnRemoveOrb()
    {
        OrbInventory.instance.Remove(item);
    }



    public void UseItem()
    {
        if (item != null)
        {

            currentSpherePlaceholder = GameObject.Find("Canvas").GetComponent<OrbInventoryUI>().getCurrentSpherePlaceholder();
            playerAnim.Play("PickUpFront");
            currentSpherePlaceholder.GetComponent<SpherePlaceholder_HardVersion>().placeSphere(item);

        }
    }



}
