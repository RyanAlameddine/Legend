using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class User {

    public string name;
    public int health = 3;
    public int level = 0;
    public List<string> items = new List<string>();
}
