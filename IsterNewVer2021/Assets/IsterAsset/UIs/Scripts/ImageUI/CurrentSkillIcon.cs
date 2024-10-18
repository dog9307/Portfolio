using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSkillIcon : SkillImageBase
{
    [SerializeField] private int _index;
    

    public override void Init()
    {
        base.Init();

        if (_index < _skill.activeSkills.Count)
            _skillIcon.sprite = _skill.activeSkills[_index].skillIcon;
    }

    public override void ActiveUI()
    {
        int skillIndex = _index;
        if (skillIndex >= _skill.activeSkills.Count)
        {
            _skillIcon.sprite = null;
            _outLine.sprite = null;
        }
        else
        {
            if (typeof(ActiveBase).IsInstanceOfType(_skill.activeSkills[skillIndex]))
            {
                _skillIcon.sprite = _skill.activeSkills[skillIndex].skillIcon;
                _outLine.sprite = _active;
            }
        }
    }
   
}
