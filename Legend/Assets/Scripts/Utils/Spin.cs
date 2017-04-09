using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

    public float degreesPerSecond;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + degreesPerSecond * Time.deltaTime);
	}
}
