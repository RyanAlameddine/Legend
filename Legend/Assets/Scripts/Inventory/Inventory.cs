using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
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

    void Start () {
        startx = x;
        starty = y;
        loadItems();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void addItem(Item item)
    {
        GameObject obj = (GameObject)Instantiate(UIItem);
        obj.transform.SetParent(transform);
        SetSize((RectTransform)obj.transform, Vector2.one);
        ((RectTransform)obj.transform).anchoredPosition = new Vector2(x, y);
        if ((x-startx) / increment > increments)
        {
            x = startx;
            y -= increment;
        }else
        {
            x += increment;
        }
    }

    void loadItems()
    {
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
        addItem(new Item());
    }

    void SetSize(this RectTransform trans, Vector2 newSize)
    {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }
}
