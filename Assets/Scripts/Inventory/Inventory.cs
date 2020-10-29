using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found.");
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int space = 6;
    private bool unlocked = false;
    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                GameObject.Find("GameManager").GetComponent<UIFader>().FadeIn(10);
                return false;
            }
            items.Add(item);

            /*
             * Hard mode:
             */
            if(item.name.Equals("RedOrb")|| item.name.Equals("GreenOrb") ||
               item.name.Equals("WhiteOrb") || item.name.Equals("BlueOrb"))
            {
                OrbInventory.instance.Add(item);
            }

            if(onItemChangedCallBack!=null)
                onItemChangedCallBack.Invoke();
        }
        return true;

    }
    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    public void Remove(string name)
    {
        if (isUnlocked())
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (name.Equals(items[i].getName()))
                {
                    /*
                    * Hard mode:
                    */
                    if (items[i].name.Equals("RedOrb") || items[i].name.Equals("GreenOrb") ||
                       items[i].name.Equals("WhiteOrb") || items[i].name.Equals("BlueOrb"))
                    {
                        OrbInventory.instance.Remove(items[i]);
                    }
                    Remove(items[i]);
                    break;
                }
            }
        }
    }

    public bool hasItem(string name)
    {
        if (isUnlocked())
        {
            for(int i=0; i<items.Count; i++)
            {
                if (name.Equals(items[i].getName())){
                    return true;
                }
            }
        }
        return false;
    }

  

    public bool isUnlocked()
    {
        return unlocked;
    }

    public void Unlock()
    {
        unlocked = true;
    }
}
