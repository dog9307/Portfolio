using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class EndingIllustReceiver : MonoBehaviour
{
    [SerializeField]
    private EndingIllustDictionary _illustDic;

    [HideInInspector]
    [SerializeField]
    private Text _title;
    [HideInInspector]
    [SerializeField]
    private Text _hint;
    [HideInInspector]
    [SerializeField]
    private Image _illust;

    [SerializeField]
    private LocalizeStringEvent _titleString;
    [SerializeField]
    private LocalizeStringEvent _hintString;


    public bool ReceiveEndingIllust()
    {
        bool isSuccess = false;

        string endingKey = PlayerPrefs.GetString("EndingIllust", "JustDie");
        if (endingKey != "JustDie")
        {
            if (_illustDic.ContainsKey(endingKey))
            {
                //_title.text = $"< {_illustDic[endingKey].title} >";
                //_hint.text = _illustDic[endingKey].hint;
                _illust.sprite = _illustDic[endingKey].sprite;

                LocalizedString localizedString = null;
                string tableKey = "";

                tableKey = $"Ending_{endingKey}_title";
                localizedString = new LocalizedString("EndingStringTable", tableKey);
                _titleString.StringReference = localizedString;
                tableKey = $"Ending_{endingKey}_hint";
                localizedString = new LocalizedString("EndingStringTable", tableKey);
                _hintString.StringReference = localizedString;

                isSuccess = true;
            }
        }

        return isSuccess;
    }
}

[System.Serializable]
public struct EndingIllustInfo
{
    public string title;
    [TextArea(5, 10)]
    public string hint;
    public Sprite sprite;
    public float width;
    public float height;
}

[System.Serializable]
public class EndingIllustDictionary : SerializableDictionary<string, EndingIllustInfo> { };
