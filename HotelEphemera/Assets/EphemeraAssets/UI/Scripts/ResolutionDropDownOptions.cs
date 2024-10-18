using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class ResolutionDropDownOptions : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private TMP_Dropdown _dropdown;
    private List<Resolution> _availableReses;

    private int _currentResWidth;
    private int _currentResHeight;
    private bool _isFullScreen;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteKey("ResDropDownValue");
        //PlayerPrefs.DeleteKey("ResWidth");
        //PlayerPrefs.DeleteKey("ResHeight");
        //PlayerPrefs.DeleteKey("ResFullScreen");

        _availableReses = new List<Resolution>(Screen.resolutions);
        _availableReses.Reverse();

        List<TMP_Dropdown.OptionData> datas = new List<TMP_Dropdown.OptionData>();
        StringBuilder builder = new StringBuilder();
        foreach (var re in _availableReses)
        {
            builder.Clear();
            builder.Append($"{re.width} x {re.height} {re.refreshRate}hz");

            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData(builder.ToString());
            datas.Add(data);
        }

        _dropdown.options = datas;

        LoadRes();
    }

    void LoadRes()
    {
        _dropdown.value = PlayerPrefs.GetInt("ResDropDownValue", 0);
        _currentResWidth = PlayerPrefs.GetInt("ResWidth", 1920);
        _currentResHeight = PlayerPrefs.GetInt("ResHeight", 1080);
        _isFullScreen = (PlayerPrefs.GetInt("ResFullScreen", 100) >= 100);
    }

    void SaveRes()
    {
        PlayerPrefs.SetInt("ResDropDownValue", _dropdown.value);
        PlayerPrefs.SetInt("ResWidth", _currentResWidth);
        PlayerPrefs.SetInt("ResHeight", _currentResHeight);
        if (_isFullScreen)
            PlayerPrefs.SetInt("ResFullScreen", 100);
        else
            PlayerPrefs.SetInt("ResFullScreen", 0);
    }

    void SetResolution()
    {
        Screen.SetResolution(_currentResWidth, _currentResHeight, _isFullScreen);

        SaveRes();
    }

    public void OnResValueChanged()
    {
        _currentResWidth = _availableReses[_dropdown.value].width;
        _currentResHeight = _availableReses[_dropdown.value].height;

        SetResolution();
    }

    public void OnFullScreenValueChanged(bool isFull)
    {
        _isFullScreen = isFull;

        SetResolution();
    }
}
