using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class SkillDescController : MonoBehaviour
{
    //[SerializeField]
    //private GameObject _skillTutorial;
    [SerializeField]
    private GameObject _descPanel;

    [SerializeField]
    private Image _activeIcon;
    [SerializeField]
    private Image _passiveIcon;
    [SerializeField]
    private Image _relicIcon;
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _desc;

    [SerializeField]
    private SFXPlayer _sfx;

    public float width { get; set; }
    public float height { get; set; }

    [SerializeField]
    private string _stringTableName = "InventoryStringTable";
    [SerializeField]
    private LocalizeStringEvent _nameString;
    [SerializeField]
    private LocalizeStringEvent _descString;

    private void OnEnable()
    {
        TurnOnDesc(false);
    }

    public void TurnOnDesc(bool isOn)
    {
        //_skillTutorial.SetActive(!isOn);
        _descPanel.SetActive(isOn);
    }

    public void SetDesc(Image icon, string name, string desc, bool isRelic = false, bool isPassive = false, bool isYFlip = false)
    {
        _sfx.PlaySFX("click");

        if (!icon)
        {
            if (_passiveIcon)
                _passiveIcon.gameObject.SetActive(false);
            if (_activeIcon)
                _activeIcon.gameObject.SetActive(false);
            if (_relicIcon)
                _relicIcon.gameObject.SetActive(false);
        }
        else
        {
            if (!isRelic)
            {
                if (_relicIcon)
                    _relicIcon.gameObject.SetActive(false);

                if (isPassive)
                {
                    if (_activeIcon)
                        _activeIcon.gameObject.SetActive(false);

                    if (_passiveIcon)
                    {
                        _passiveIcon.gameObject.SetActive(true);
                        _passiveIcon.sprite = icon.sprite;
                        _passiveIcon.color = icon.color;

                        float multi = 1.0f;
                        if (isYFlip)
                            multi = -1.0f;

                        Vector3 scale = _passiveIcon.rectTransform.localScale;
                        scale.y = Mathf.Abs(scale.y) * multi;
                        _passiveIcon.rectTransform.localScale = scale;
                    }
                }
                else
                {
                    if (_passiveIcon)
                        _passiveIcon.gameObject.SetActive(false);

                    if (_activeIcon)
                    {
                        _activeIcon.gameObject.SetActive(true);
                        _activeIcon.sprite = icon.sprite;
                    }
                }
            }
            else
            {
                if (_passiveIcon)
                    _passiveIcon.gameObject.SetActive(false);
                if (_activeIcon)
                    _activeIcon.gameObject.SetActive(false);

                if (_relicIcon)
                {
                    _relicIcon.gameObject.SetActive(true);
                    _relicIcon.sprite = icon.sprite;

                    _relicIcon.rectTransform.sizeDelta = new Vector2(width, height);
                }
            }
        }
        //_name.text = name;
        //_desc.text = desc;

        LocalizedString localizedString = new LocalizedString(_stringTableName, name);
        _nameString.StringReference = localizedString;
        localizedString = new LocalizedString(_stringTableName, desc);
        _descString.StringReference = localizedString;

        TurnOnDesc(true);
    }
}
