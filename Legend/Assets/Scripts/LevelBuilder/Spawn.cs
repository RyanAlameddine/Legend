using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject SpawnableObject;

    public Transform Parent;

    public void SpawnObject()
    {
        (Instantiate(SpawnableObject, Parent.transform.position, Quaternion.identity) as GameObject).transform.SetParent(Parent);
    }
}
