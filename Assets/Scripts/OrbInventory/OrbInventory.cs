using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbInventory : MonoBehaviour
{

    #region Singleton
    public static OrbInventory instance;

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

    public int space = 4;
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

            if (onItemChangedCallBack != null)
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
            for (int i = 0; i < items.Count; i++)
            {
                if (name.Equals(items[i].getName()))
                {
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
