using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
	new public string name = "New Item";
	public Sprite icon = null;
	public GameObject game_object = null;
	public bool isDefaultItem = false;

	private GameObject character;

	public void Start()
	{
		character = GameObject.Find("Character");
	}

	public virtual void Use()
	{

	}
    public string getName()
    {
		return name;
    }
}
