using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAdder : MonoBehaviour
{
    [SerializeField]
    private int _skillId;
    public int skillId { get { return _skillId; } }

    [SerializeField]
    private TempSkillIconSetter _setter;

    private SkillBase _newSkill;
    public SkillBase newSkill { get { return _newSkill; } }

    [SerializeField]
    private DragableIcon _relativeIcon;

    void Start()
    {
        _newSkill = SkillFactory.CreateSkill(_skillId);
        _newSkill.Init();

        TempSkillIcon icon = GetComponent<TempSkillIcon>();
        icon.SetIcon(_newSkill, ((_skillId / 100) == 2), false);
    }

    public void AddSkill()
    {
        if (_newSkill != null)
        {
            PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
            if (playerSkill)
            {
                SkillBase skill = playerSkill.FindSkill(_newSkill.GetType());
                if (skill == null)
                {
                    bool isPassive = (_newSkill.id / 100) == 2;
                    if (!isPassive)
                    {
                        playerSkill.ActiveSkillChange(_newSkill, _relativeIcon.targetIndex);

                        if (_setter)
                            _setter.AddSkillIcon(_newSkill, _relativeIcon.targetIndex);
                    }
                    else
                    {
                        if (playerSkill.passiveSkills.Count == 0)
                        {
                            bool isSuccess = playerSkill.AddSkill(_newSkill);
                            if (isSuccess) ;
                                //FindObjectOfType<TempCostField>().UpdateCost(GetComponent<TempSkillIcon>());
                        }
                        else
                        {
                            playerSkill.PassiveSkillChange(_newSkill);
                            //FindObjectOfType<TempCostField>().UpdateCost(GetComponent<TempSkillIcon>());
                        }
                    }
                }
            }
        }
    }
}
