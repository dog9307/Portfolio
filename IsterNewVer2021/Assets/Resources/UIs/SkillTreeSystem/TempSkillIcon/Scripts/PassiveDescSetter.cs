using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDescSetter : SkillDescSetter
{
    private PassiveSkillSelector _selector;

    [SerializeField]
    private bool _isYFlip = true;

    void Start()
    {
        _selector = GetComponent<PassiveSkillSelector>();
    }

    protected override void SetDesc()
    {
        _descCon.SetDesc(_selector.icon, _skillName, _desc, false, true, _isYFlip);
    }
}
