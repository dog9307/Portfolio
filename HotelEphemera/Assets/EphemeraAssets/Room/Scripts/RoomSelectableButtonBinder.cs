using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomSelectableButtonBinder : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private InteractionEvent _evt;
    [HideInInspector]
    [SerializeField]
    private TextMeshProUGUI _textNormal;
    [HideInInspector]
    [SerializeField]
    private TextMeshProUGUI _textHighlighted;
    [HideInInspector]
    [SerializeField]
    private Image _newIcon;

    public string buttonText
    {
        set
        {
            _textNormal.text = value;
            _textHighlighted.text = value;
        }
    }

    public void Bind(string buttonName, string dialogueName, InteractionConditionBase condition, string savableKey)
    {
        _evt.dialogueName = dialogueName;
        _evt.condition = condition;

        //int count = SavableDataManager.instance.FindIntSavableData(savableKey);
        //if (count >= 100)
        //    buttonText = buttonName;
        //else
        //    buttonText = "? ? ?";

        buttonText = buttonName;

        int count = SavableDataManager.instance.FindIntSavableData(savableKey);
        _newIcon.gameObject.SetActive(count < 100);
    }
}
