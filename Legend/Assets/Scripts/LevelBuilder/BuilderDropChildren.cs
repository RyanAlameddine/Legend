using UnityEngine;
using System.Collections;

public class BuilderDropChildren : MonoBehaviour {

	public void Drop()
    {
        transform.DetachChildren();
    }
}
