using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class ClueTextListController : MonoBehaviour
{
    [SerializeField]
    private string _keyward = "DarkLight";
    public string keyward { get { return _keyward; } }

    [SerializeField]
    private List<ClueTextController> _textList;
    public int Count { get { return (_textList == null ? 0 : _textList.Count); } }

    [SerializeField]
    private Text _title;
    [SerializeField]
    private string _originTitle = "°ËÀººû";

    [SerializeField]
    private LocalizeStringEvent _textTitleString;

    private void Awake()
    {
        int count = PlayerPrefs.GetInt($"Clue_{_keyward}", 0);
        if (count >= 100)
        {
            LocalizedString localizedString = new LocalizedString("ClueStringTable", "Clue_button_default");
            _textTitleString.StringReference = localizedString;

            //_title.text = "? ? ?";
        }
        else
            OpenClue();
    }

    public void OpenClue(int id)
    {
        foreach (var c in _textList)
        {
            if (c.id == id)
            {
                c.gameObject.SetActive(true);
                break;
            }
        }

        bool isOff = true;
        for (int i = _textList.Count - 1; i >= 0; --i)
        {
            if (_textList[i].gameObject.activeSelf)
            {
                if (isOff)
                {
                    isOff = false;
                    _textList[i].SeperatorOn(false);
                }
                else
                    _textList[i].SeperatorOn(true);
            }
        }
    }

    public string OpenClue()
    {
        LocalizedString localizedString = new LocalizedString("ClueStringTable", _originTitle);
        _textTitleString.StringReference = localizedString;

        //_title.text = _originTitle;

        string key = $"Clue_{_keyward}_";
        foreach (var c in _textList)
        {
            string clueKey = key + c.id.ToString();
            int clueCount = SavableDataManager.instance.FindIntSavableData(clueKey);
            if (clueCount >= 100)
                c.gameObject.SetActive(true);
            else
                c.gameObject.SetActive(false);
        }

        for (int i = _textList.Count - 1; i >= 0; --i)
        {
            if (_textList[i].gameObject.activeSelf)
            {
                _textList[i].SeperatorOn(false);
                break;
            }
        }

        return _title.text;
    }
}
