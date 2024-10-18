using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempSkillIcon : MonoBehaviour
{
    [SerializeField]
    private Image _icon;
    public Image icon { get { return _icon; } }
    [SerializeField]
    private Image _outline;

    [SerializeField]
    private Sprite _activeOutline;
    [SerializeField]
    private Sprite _passiveOutline;

    [SerializeField]
    private Sprite _dummySprite;

    [SerializeField]
    private Text _costText;

    public void SetIcon(SkillBase skill, bool isPassive, bool isChangeIcon = true)
    {
        if (isChangeIcon)
            _icon.sprite = (skill == null ? _dummySprite : skill.skillIcon);
        _outline.sprite = (isPassive ? _passiveOutline : _activeOutline);

        DynamicSkillDescSetterHelper helper = GetComponent<DynamicSkillDescSetterHelper>();
        if (helper)
            helper.relativeSkill = skill;

        if (_costText)
        {
            if (skill == null)
                _costText.gameObject.SetActive(false);
            else
            {
                if (typeof(PassiveBase).IsInstanceOfType(skill))
                    _costText.text = ((PassiveBase)skill).cost.ToString();
                else
                    _costText.gameObject.SetActive(false);
            }
        }
    }

    public void SetIcon(TempSkillIcon sour)
    {
        if (sour)
        {
            _icon.sprite = sour.icon.sprite;
            _icon.color = sour.icon.color;

            Vector2 scale= _icon.rectTransform.localScale;
            scale.y = sour.icon.rectTransform.localScale.y;
            _icon.rectTransform.localScale = scale;

            _costText.gameObject.SetActive(sour._costText.gameObject.activeSelf);
            _costText.text = sour._costText.text;
        }
        else
        {
            _icon.sprite = _dummySprite;
            _costText.gameObject.SetActive(false);
        }
    }
}
