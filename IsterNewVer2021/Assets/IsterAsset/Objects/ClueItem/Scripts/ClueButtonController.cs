using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using TMPro;

public class ClueButtonController : MonoBehaviour
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
    private ClueTextListManager _manager;

    private bool _isClueGained = false;

    [SerializeField]
    private LocalizeStringEvent _buttonString;

    private void OnEnable()
    {
        if (_isClueGained)
        {
            _button.interactable = true;

            LocalizedString localizedString = new LocalizedString("ClueStringTable", _title);
            _buttonString.StringReference = localizedString;
            //_text.text = _title;
        }
        else
        {
            _button.interactable = false;

            LocalizedString localizedString = new LocalizedString("ClueStringTable", "Clue_button_default");
            _buttonString.StringReference = localizedString;
            //_text.text = "? ? ?";
        }
    }

    public void OpenButton()
    {
        _isClueGained = true;
    }

    public void GainClue(int id)
    {
        _isClueGained = true;

        _manager.GainClue(_keyward, id);
    }

    public void OnClick()
    {
        if (_manager)
        {
            ClueTextListController listCon = _manager.FindTextList(_keyward);
            if (listCon)
                _manager.ChangeList(listCon);
        }
    }
}
