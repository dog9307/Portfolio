using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillIcon : PlayerSkillSwap
{
    [SerializeField] private int _index;

    [SerializeField]
    private Text _skillCount;

    public override void Init()
    {
        base.Init();

        if (_index < _skill.activeSkills.Count)
            _skillIcon.sprite = _skill.activeSkills[_index].skillIcon;
        _skillCount = GetComponentInChildren<Text>();
    }

    public override void UpdateGauge()
    {
        int skillIndex = (int)_attack.currentHand * PlayerSkillUsage.HAND_MAX_COUNT + _index;
        if (skillIndex >= _skill.activeSkills.Count)
        {
            _MaxGuage = 1.0f;
            _CurretGuage = 1.0f;
            _skillIcon.sprite = null;
            _outLine.sprite = null;
            _skillCount.enabled = false;
        }
        else
        {
            if (typeof(ActiveBase).IsInstanceOfType(_skill.activeSkills[skillIndex]))
            {
                _skillIcon.sprite = _skill.activeSkills[skillIndex].skillIcon;
                _outLine.sprite = _active;


                if (typeof(CountableActive).IsInstanceOfType(_skill.activeSkills[skillIndex]))
                {
                    _skillCount.enabled = true;
                    //_skillCount.text = ((CountableActive)_skill.activeSkills[skillIndex]).currentCount.ToString();
                }
                else
                    _skillCount.enabled = false;
            }
            else if (typeof(PassiveCoolTimeDown).IsInstanceOfType(_skill.activeSkills[skillIndex]))
            {
                PassiveCoolTimeDownUser user = FindObjectOfType<PlayerSkillUsage>().GetComponentInChildren<SkillUserManager>().FindUser(typeof(PassiveCoolTimeDown)) as PassiveCoolTimeDownUser;
                if (user.isCoolTimeMode)
                {
                    _skillIcon.sprite = _skill.activeSkills[skillIndex].skillIcon;
                    _outLine.sprite = _passive;
                    _MaxGuage = user.totalCoolTime;
                    _CurretGuage = user.currentCoolTime;
                    _skillCount.enabled = false;

                    return;
                }
            }
            if (typeof(PassiveBase).IsInstanceOfType(_skill.activeSkills[skillIndex]))
            {
                _skillIcon.sprite = _skill.activeSkills[skillIndex].skillIcon;
                _outLine.sprite = _passive;
                _MaxGuage = 1.0f;
                _CurretGuage = 1.0f;
                _skillCount.enabled = false;
            }
        }
    }
    //public override void ActiveUI()
    //{
    //    if (_attack.currenthand == hand.left)
    //    {
    //        if (_skill.skills[3].othertype == typeof(activebase))
    //        {
    //            _firstimage.sprite = _skill.skills[3].skillicon;
    //            _outline = resources.load<sprite>("uis/images/skilloutline/activeoutline") as sprite;
    //        }
    //        else if (_skill.skills[3].othertype == typeof(passivebase))
    //        {
    //            _firstimage.sprite = _skill.skills[3].skillicon;
    //            _outline = resources.load<sprite>("uis/images/skilloutline/passiveoutline") as sprite;
    //        }
    //    }
    //    else if (_attack.currenthand == hand.right)
    //    {
    //        if (_skill.skills[8].othertype == typeof(activebase))
    //        {
    //            _firstimage.sprite = _skill.skills[8].skillicon;
    //            _outline = resources.load<sprite>("uis/images/skilloutline/activeoutline") as sprite;
    //        }
    //        else if (_skill.skills[8].othertype == typeof(passivebase))
    //        {
    //            _firstimage.sprite = _skill.skills[8].skillicon;
    //            _outline = resources.load<sprite>("uis/images/skilloutline/passiveoutline") as sprite;
    //        }
    //    }
    //}
}
