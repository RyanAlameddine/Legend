using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class LoadFade : MonoBehaviour {
	void Start () {
        if (Directory.GetFiles(Application.persistentDataPath + "/").Length == 0)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
