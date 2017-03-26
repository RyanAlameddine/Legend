using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    static Inventory instance;

    public static Inventory Instance { get { return instance; } }

    public static UIItem EquippedSword {
        get
        {
            return equippedSword;
        }
        set
        {
            if (equippedSword == value)
            {
                equippedSword = null;
                return;
            }
            //if(equippedSword != null) equippedSword.onClick();
            equippedSword = value;
        }
    }
    static UIItem equippedSword;
    bool setEquipped;

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
        starty = y;
        loadItems();
    }

    void addItem(Item item)
    {
        GameObject obj = (GameObject)Instantiate(UIItem);
        obj.transform.SetParent(transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
        ((RectTransform)obj.transform).localScale = Vector3.one;
        ((RectTransform)obj.transform).anchoredPosition = new Vector2(x, y);
        obj.GetComponent<Image>().enabled = item.equiptstatus;
        obj.GetComponent<UIItem>().item = item;
        if(Inventory.equippedSword != null && item == Inventory.equippedSword.item && !setEquipped)
        {
            obj.GetComponent<UIItem>().onClick();
            setEquipped = true;
        }
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
        for(int i = 0; i < transform.childCount; i++)
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
        setEquipped = false;
        foreach(string i in GameManager.Instance.user.items)
        {
            addItem(GameManager.Instance.itemReferences[i]);
        }
        x = startx;
        y = starty;
    }
}
