using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillManager : MonoBehaviour
{
    [SerializeField]
    private PassiveSkillSelector[] _selectors;

    private PlayerSkillUsage _playerSkill;

    private void OnEnable()
    {
        if (!_playerSkill)
            _playerSkill = FindObjectOfType<PlayerSkillUsage>();

        if (!_playerSkill) return;

        if (_playerSkill.passiveSkills.Count == 0)
            Select(_selectors[0]);
        else
        {
            for (int i = 1; i < +_selectors.Length; ++i)
            {
                if (_playerSkill.passiveSkills[0].id == _selectors[i].skillId)
                {
                    Select(_selectors[i]);
                    break;
                }
            }
        }
    }

    public void Select(PassiveSkillSelector select)
    {
        foreach (var selector in _selectors)
        {
            if (select == selector)
                selector.Select();
            else
                selector.Deselect();
        }
    }
}
