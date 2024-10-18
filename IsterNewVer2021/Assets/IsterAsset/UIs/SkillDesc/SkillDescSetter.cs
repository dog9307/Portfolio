using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillDescSetter : MonoBehaviour, IPointerEnterHandler
{
    private TempSkillIcon _skillIcon;

    [SerializeField]
    protected string _skillName;

    [SerializeField]
    [TextArea(5, 20)]
    protected string _desc;

    [SerializeField]
    protected SkillDescController _descCon;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!_descCon)
            _descCon = FindObjectOfType<SkillDescController>();

        if (!gameObject.activeSelf)
        {
            _descCon.TurnOnDesc(false);
            return;
        }

        if (!_skillIcon)
            _skillIcon = GetComponent<TempSkillIcon>();

        SkillAdder adder = GetComponent<SkillAdder>();
        if (adder)
        {
            _skillName = adder.newSkill.skillName;
            _desc = adder.newSkill.skillDesc;
        }

        SetDesc();
    }

    protected virtual void SetDesc()
    {
        if (_skillIcon)
            _descCon.SetDesc(_skillIcon.icon, _skillName, _desc);
    }
}
