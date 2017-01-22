using UnityEngine;
using System.Collections;

public class EquippedSword : MonoBehaviour {
    string spriteName;

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
            spriteName = "";
        }
        else if (Inventory.EquippedSword.item.spriteName != spriteName)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Layering>().enabled = true;
            GetComponent<Animator>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            foreach(ImageReference reference in GameManager.Instance.images)
            {
                if(reference.ImageName == Inventory.EquippedSword.item.spriteName)
                {
                    GetComponent<SpriteRenderer>().sprite = reference.sprite;
                    spriteName = reference.ImageName;
                }
            }
        }
	}
}
