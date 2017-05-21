using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class TransitionToLevelEffect : MonoBehaviour {
    bool running = false;
    float randomness = 0;
    [SerializeField]
    float maxRandomness = 3;
    float xoffset;
    float yoffset;
    float zoffset;
    Vector3 startPosition;
    
	void Start () {
        GameManager.Instance.AddClass(this);
	}
	
    [Event("TransitionToLevel")] Fix THIS
	void StartTransition(string parameter)
    {
        running = true;
        startPosition = transform.position;
    }

    void Update()
    {
        if (running)
        {
            randomness = Mathf.Lerp(randomness, maxRandomness, .01f);
            xoffset = Random.Range(-randomness, randomness);
            yoffset = Random.Range(-randomness, randomness);
            zoffset = Random.Range(-randomness, randomness);
            transform.position = startPosition + new Vector3(xoffset, yoffset, zoffset);
        }
    }
}
