using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIItem : MonoBehaviour {

    public Item item;

    public void onHover()
    {
        transform.parent.FindChild("Description").GetComponent<Text>().text = item.description;
    }

    public void onExit()
    {
        transform.parent.FindChild("Description").GetComponent<Text>().text = "";
    }

    public void onClick()
    {
        
        if(item.type == ItemType.Weapon)
        {
            transform.GetComponent<Image>().enabled = !transform.GetComponent<Image>().enabled;
            GameManager.Instance.runEvent("EquipChange");
            if (transform.GetComponent<Image>().enabled) {
                Inventory.EquippedSword = this;
            }
            else
            {
                Inventory.EquippedSword = null;
            } 
        }else if(item.type == ItemType.Consumable)
        {
            GameManager.Instance.user.health += ((Consumable)item).health;
            GameManager.Instance.user.health = Mathf.Clamp(GameManager.Instance.user.health, 0, 10);
            GameManager.Instance.user.items.Remove(item.name);
        }
    }
}
