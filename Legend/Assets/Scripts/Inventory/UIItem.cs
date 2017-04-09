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
        
        if(item is Weapon)
        {
            transform.GetComponent<Image>().enabled = !transform.GetComponent<Image>().enabled;
            if (transform.GetComponent<Image>().enabled)
            {
                Inventory.EquippedSword = this;
            }
            else
            {
                Inventory.EquippedSword = null;
            }
            GameManager.Instance.runEvent("EquipChange");
        }else if(item is Consumable)
        {
            GameManager.Instance.user.health += ((Consumable)item).health;
            GameManager.Instance.user.health = Mathf.Clamp(GameManager.Instance.user.health, 0, 10);
            GameManager.Instance.user.items.Remove(item.name);
            Inventory.Instance.resetInv();
        }
    }
}
