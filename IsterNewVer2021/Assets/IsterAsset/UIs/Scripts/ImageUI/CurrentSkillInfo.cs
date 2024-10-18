using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSkillInfo : SkillImageBase
{
    [SerializeField] private int _index;

    [SerializeField] Text _name;
    [SerializeField] Text _info;
    [SerializeField] Text _coolTime;

    public override void Init()
    {
        base.Init();

        if (_index < _skill.activeSkills.Count)
            _skillIcon.sprite = _skill.activeSkills[_index].skillIcon;
    }

    public override void ActiveUI()
    {
        int skillIndex = (int)_attack.currentHand * 5 + _index;
        if (skillIndex >= _skill.activeSkills.Count)
        {
            _skillIcon.sprite = null;
            _outLine.sprite = null;
            _name.text = null;
            _info.text = null;
            _coolTime.text = null;
        }
        else
        {
            if (typeof(ActiveBase).IsInstanceOfType(_skill.activeSkills[skillIndex]))
            {
                _skillIcon.sprite = _skill.activeSkills[skillIndex].skillIcon;
                _outLine.sprite = _active;
                _name.text = _skill.activeSkills[skillIndex].skillName;
                _info.text = _skill.activeSkills[skillIndex].skillDesc;
                //_coolTime.text = "CoolTime : "+ ((ActiveBase)_skill.activeSkills[skillIndex]).totalCoolTime.ToString() + "Sec";
            }
            if (typeof(PassiveBase).IsInstanceOfType(_skill.activeSkills[skillIndex]))
            {
                _skillIcon.sprite = _skill.activeSkills[skillIndex].skillIcon;
                _outLine.sprite = _passive;
                _name.text = _skill.activeSkills[skillIndex].skillName;
                _info.text = _skill.activeSkills[skillIndex].skillDesc;
            }
        }
    }

}