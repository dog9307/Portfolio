using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicIconDesc : SkillDescSetter
{
    [SerializeField]
    private RelicIconController _controller;

    [SerializeField]
    private float _width = 1000.0f;
    [SerializeField]
    private float _height = 1000.0f;

    protected override void SetDesc()
    {
        _descCon.width = _width;
        _descCon.height = _height;

        _descCon.SetDesc(_controller.icon, _skillName, _desc, true);
    }
}
