using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    GameObject UIItem;
    [SerializeField]
    float x;
    [SerializeField]
    float y;
    [SerializeField]
    float increment;
    [SerializeField]
    float increments;
    float startx;
    float starty;

    void Start()
    {
        startx = x;
        starty = y;
        loadItems();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void addItem(Item item)
    {
        GameObject obj = (GameObject)Instantiate(UIItem);
        obj.transform.SetParent(transform);
        foreach (ImageReference im in GameManager.Instance.images)
        {
            if (im.ImageName == item.spriteName)
            {
                int test = obj.transform.childCount;
                obj.transform.GetChild(0).GetComponent<Image>().sprite = im.sprite;
            }
        }
        ((RectTransform)obj.transform).localScale = Vector3.one;
        ((RectTransform)obj.transform).anchoredPosition = new Vector2(x, y);
        obj.GetComponent<Image>().enabled = item.equiptstatus;
        if ((x - startx) / increment > increments)
        {
            x = startx;
            y -= increment;
        }
        else
        {
            x += increment;
        }
    }

    void loadItems()
    {
        addItem(new Weapon("Foam Sword", 1, WeaponPower.no, 10, "Foam Sword"));
    }
}
