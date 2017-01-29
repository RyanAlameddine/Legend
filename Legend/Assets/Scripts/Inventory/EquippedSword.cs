using UnityEngine;
using System.Collections;

public class EquippedSword : MonoBehaviour {
    string itemName;

    public void Start()
    {
        GameManager.Instance.AddClass(this);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Layering>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
	
    [Event("EquipChange")]
	public void updateSword () {
        if(Inventory.EquippedSword == null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Layering>().enabled = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            itemName = "";
        }
        else if (Inventory.EquippedSword.item.name != name)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Layering>().enabled = true;
            GetComponent<Animator>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;

            if (GameManager.Instance.itemReferences.ContainsKey(itemName))
            {
                var reference = GameManager.Instance.itemReferences[itemName];
                if (reference == Inventory.EquippedSword.item)
                {
                    GetComponent<SpriteRenderer>().sprite = reference.sprite;
                    itemName = reference.name;
                }
            }
            
        }
	}
}
