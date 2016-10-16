using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
    public ItemType type;
    public bool equiptstatus = false;
    public string description = "";
    public string name = "";
    public int cost = 0;
    public int count = 1;
    public Sprite sprite;

    public void togglequpited()
    {
        equiptstatus = !equiptstatus;
    }
}
