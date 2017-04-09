using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class User {

    public string name;
    public float health = 10;
    public int level = 0;
    public List<string> items = new List<string>();
}
