using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuilderSpawn : MonoBehaviour {

    public List<GameObject> Objects;

    public Transform Parent;

    public void SpawnObject()
    {
        if (Parent.childCount == 0)
        {
            for (int i = 0; i < Objects.Count; i++){
                GameObject obj = (Instantiate(Objects[i], Parent.transform.position, Quaternion.identity) as GameObject);
                obj.transform.SetParent(Parent);
            }
        }
    }
}
