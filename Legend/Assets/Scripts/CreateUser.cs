using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputField))]
public class CreateUser : MonoBehaviour {

    InputField input;
    [SerializeField]
    GameObject warning;

    public void Start()
    {
        input = GetComponent<InputField>();
    }

    public void Create()
    {
        if (!SaveLoad.Load(input.text))
        {
            if (input.text != "")
            {
                User user = new User();
                user.name = input.text;
                GameManager.Instance.user = user;
                SaveLoad.Save();
                SceneManager.LoadScene(1);
            }else
            {
                GameObject warn = (GameObject)Instantiate(warning);
                warn.transform.SetParent(transform);
                ((RectTransform)warn.transform).anchoredPosition = ((RectTransform)warning.transform).anchoredPosition;
                warn.GetComponent<Text>().text = "Enter a name to begin.";
            }
        }else
        {
            GameObject warn = (GameObject)Instantiate(warning);
            warn.transform.SetParent(transform);
            ((RectTransform)warn.transform).anchoredPosition = ((RectTransform)warning.transform).anchoredPosition;
            warn.GetComponent<Text>().text = "The name " + input.text + " is already taken.";
        }
    }
}
