using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.Dark;

public class ItemContentDescPanel : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private UIDissolveEffect _dissolve;

    [HideInInspector]
    [SerializeField]
    private Image _icon;
    [HideInInspector]
    [SerializeField]
    private TextMeshProUGUI _name;
    [HideInInspector]
    [SerializeField]
    private TextMeshProUGUI _desc;

    // Start is called before the first frame update
    void Start()
    {
        _dissolve.location = 1.0f;
    }

    public void OpenPanel(Sprite sprite, string name, string desc)
    {
        _dissolve.DissolveIn();

        _icon.sprite = sprite;
        _name.text = name;
        _desc.text = desc;
    }

    public void ClosePanel()
    {
        _dissolve.DissolveOut();
    }
}
