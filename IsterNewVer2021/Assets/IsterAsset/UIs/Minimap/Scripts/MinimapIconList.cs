using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconList : MonoBehaviour
{
    [SerializeField]
    private MinimapTeleportButton[] _buttons;

    void Start()
    {
        foreach (var button in _buttons)
        {
            int id = button.buttonID;
            string key = "minimap_button_" + id.ToString();

            int count = PlayerPrefs.GetInt(key, 0);
            if (count >= 100)
            {
                Transform prevParent = button.transform.parent;
                button.transform.parent = null;
                button.transform.parent = prevParent;
                button.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                button.gameObject.SetActive(true);
            }
            else
                button.gameObject.SetActive(false);
        }
        //for (int i = 0; i < 1; ++i)
        //{
        //    int id = 100 + i;
        //    string key = "minimap_button_" + id.ToString();

        //    int count = PlayerPrefs.GetInt(key, 0);

        //    if (count >= 100)
        //        ButtonOn(id);
        //}

        ButtonOn(100);
    }

    public void ButtonOn(int id)
    {
        foreach (var button in _buttons)
        {
            if (button.buttonID == id)
            {
                Transform prevParent = button.transform.parent;
                Vector3 pos = button.transform.localPosition;
                button.transform.parent = null;
                button.transform.parent = prevParent;
                button.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                button.transform.localPosition = pos;

                button.gameObject.SetActive(true);
            }
        }
    }
}
