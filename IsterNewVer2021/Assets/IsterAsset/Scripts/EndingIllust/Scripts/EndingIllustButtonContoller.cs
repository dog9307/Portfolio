using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using TMPro;

public class EndingIllustButtonContoller : MonoBehaviour
{
    [SerializeField]
    private string _keyward = "";
    public string keyward { get { return _keyward; } }

    [HideInInspector]
    [SerializeField]
    private Button _button;
    [HideInInspector]
    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private string _title = "";

    [SerializeField]
    private EndingIllustButtonManager _manager;

    private bool _isEndingGained = false;

    [SerializeField]
    private LocalizeStringEvent _buttonString;

    private void OnEnable()
    {
        if (_isEndingGained)
        {
            _button.interactable = true;

            LocalizedString localizedString = new LocalizedString("EndingStringTable", _title);
            _buttonString.StringReference = localizedString;
        }
        else
        {
            _button.interactable = false;

            LocalizedString localizedString = new LocalizedString("ClueStringTable", "Clue_button_default");
            _buttonString.StringReference = localizedString;
        }
    }

    public void OpenButton()
    {
        _isEndingGained = true;
    }

    public void OnClick(GameObject clickedPanel)
    {
        if (_manager)
            _manager.OnClick(clickedPanel);
    }
}
