using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicSkillDescSetter : SkillDescSetter
{
    private DynamicSkillDescSetterHelper _helper;

    [SerializeField]
    private Image _icon;

    protected override void SetDesc()
    {
        if (!_helper)
        {
            _helper = GetComponent<DynamicSkillDescSetterHelper>();
            if (!_helper)
            {
                base.SetDesc();
                return;
            }
        }

        if (_helper.relativeSkill == null)
        {
            _descCon.TurnOnDesc(false);
            return;
        }

        Sprite icon = null;
        string name = "";
        string desc = "";
        if (_helper.relativeSkill != null)
        {
            icon = _helper.relativeSkill.skillIcon;
            name = _helper.relativeSkill.skillName;
            desc = _helper.relativeSkill.skillDesc;
        }

        _descCon.SetDesc(_icon, name, desc);
    }
}
