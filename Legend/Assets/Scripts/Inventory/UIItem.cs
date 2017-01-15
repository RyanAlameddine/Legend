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
        transform.GetComponent<Image>().enabled = !transform.GetComponent<Image>().enabled;
        if(item.type == ItemType.Weapon)
        {
            Inventory.EquippedSword = this;
        }
        GameManager.Instance.runEvent("EquipChange");
    }
}
