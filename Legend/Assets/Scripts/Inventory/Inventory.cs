using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    static Inventory instance;

    public static Inventory Instance { get { return instance; } }

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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startx = x;
        loadItems();
    }

    void addItem(Item item)
    {
        GameObject obj = (GameObject)Instantiate(UIItem);
        obj.transform.SetParent(transform);
        foreach (ImageReference im in GameManager.Instance.images)
        {
            if (im.ImageName == item.spriteName)
            {
                obj.transform.GetChild(0).GetComponent<Image>().sprite = im.sprite;
            }
        }
        ((RectTransform)obj.transform).localScale = Vector3.one;
        ((RectTransform)obj.transform).anchoredPosition = new Vector2(x, y);
        obj.GetComponent<Image>().enabled = item.equiptstatus;
        obj.GetComponent<UIItem>().description = item.description;
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

    public void resetInv() {
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            if(obj.name != "Description")
            {
                Destroy(obj);
            }
        }
        loadItems();
    }

    void loadItems()
    {
        /*
        foreach(Item i in GameManager.Instance.user.items)
        {
            addItem(i);
        }
        */
        addItem(new Weapon("Foam", 1, WeaponPower.no, 1, "Foam Sword"));
    }
}
