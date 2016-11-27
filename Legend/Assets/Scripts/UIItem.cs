using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIItem : MonoBehaviour {

    public string description;

    public void onHover()
    {
        transform.parent.FindChild("Description").GetComponent<Text>().text = description;
    }

    public void onExit()
    {
        transform.parent.FindChild("Description").GetComponent<Text>().text = "";
    }

    public void onClick()
    {
        transform.GetChild(0).GetComponent<Image>().enabled = true;
    }
}
