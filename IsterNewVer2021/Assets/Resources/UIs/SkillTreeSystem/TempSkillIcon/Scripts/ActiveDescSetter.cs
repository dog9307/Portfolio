using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActiveDescSetter : SkillDescSetter
{
    [SerializeField]
    private Image _icon;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!_descCon)
            _descCon = FindObjectOfType<SkillDescController>();

        SetDesc();
    }

    protected override void SetDesc()
    {
        _descCon.SetDesc(_icon, _skillName, _desc);
    }
}
