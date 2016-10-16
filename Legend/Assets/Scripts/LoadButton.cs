using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour {
    InputField input;
    [SerializeField]
    GameObject warning;

    void Start () {
        input = GetComponent<InputField>();
	}

    public void Load()
    {
        if (SaveLoad.Load(input.text))
        {
            SceneManager.LoadScene(1);
        }else
        {
            GameObject warn = (GameObject)Instantiate(warning);
            warn.transform.SetParent(transform);
            ((RectTransform)warn.transform).anchoredPosition = ((RectTransform)warning.transform).anchoredPosition;
            warn.GetComponent<Text>().text = "This user does not exist!";
        }
    }
}
