using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Destroy : MonoBehaviour {
    [SerializeField]
    bool TextAlphaZero;
    Text text;
	
    void Start()
    {
        text = GetComponent<Text>();
    }

	void Update () {
	    if(TextAlphaZero && text.color.a <= 0)
        {
            Destroy(transform.gameObject);
        }
	}
}
