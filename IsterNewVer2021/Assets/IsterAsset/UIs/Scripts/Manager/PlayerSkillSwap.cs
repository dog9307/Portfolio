using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerSkillSwap : ProgressBase
{
    protected PlayerSkillUsage _skill;
    protected PlayerAttacker _attack;

    [SerializeField]
    protected Image _skillIcon;
    [SerializeField]
    protected Image _outLine;

    protected static Sprite _active = null;
    protected static Sprite _passive = null;

    public override void Init()
    {
        if (!_active)
            _active = Resources.Load<Sprite>(("UIs/Images/SkillOutLine/ActiveOutLine"));
        if (!_passive)
            _passive = Resources.Load<Sprite>(("UIs/Images/SkillOutLine/PassiveOutLine"));
        _attack = GameObject.FindObjectOfType<PlayerAttacker>();
        _skill = GameObject.FindObjectOfType<PlayerSkillUsage>();      
    }
}
    
