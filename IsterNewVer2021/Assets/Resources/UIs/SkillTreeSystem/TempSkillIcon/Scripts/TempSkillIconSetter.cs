using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSkillIconSetter : MonoBehaviour
{
    [SerializeField]
    private bool _isPassive = false;

    [SerializeField]
    private GameObject _iconPrefab;
    [SerializeField]
    private GameObject _relativePanel;

    [SerializeField]
    private List<GameObject> _iconList = new List<GameObject>();

    private PlayerSkillUsage _skill;

    private void OnEnable()
    {
        if (!_relativePanel) return;

        if (!_skill)
            _skill = FindObjectOfType<PlayerSkillUsage>();

        //foreach (var icon in _iconList)
        //    Destroy(icon);
        //_iconList.Clear();

        List<SkillBase> relativeSkills = null;
        if (_isPassive)
            relativeSkills = _skill.passiveSkills;
        else
            relativeSkills = _skill.activeSkills;

        if (relativeSkills == null) return;

        if (!_isPassive)
        //{
        //    foreach (var skill in relativeSkills)
        //        AddSkillIcon(skill);
        //}
        //else
        {
            for (int i = 0; i < relativeSkills.Count; ++i)
                AddSkillIcon(relativeSkills[i], i);
        }
    }

    public void AddSkillIcon(SkillBase skill)
    {
        GameObject currentIcon = Instantiate(_iconPrefab);
        TempSkillIcon icon = currentIcon.GetComponent<TempSkillIcon>();
        if (icon)
            icon.SetIcon(skill, _isPassive);

        //SelectableIcon select = currentIcon.GetComponent<SelectableIcon>();
        //if (select)
        //    select.relativeSkill = skill;

        currentIcon.transform.parent = _relativePanel.transform;
        currentIcon.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        _iconList.Add(currentIcon);
    }

    public void AddSkillIcon(SkillBase skill, int index, bool isChangeIcon = true)
    {
        //GameObject currentIcon = Instantiate(_iconPrefab);
        GameObject currentIcon = _iconList[index];
        TempSkillIcon icon = currentIcon.GetComponent<TempSkillIcon>();
        if (icon)
            icon.SetIcon(skill, _isPassive, isChangeIcon);

        //SelectableIcon select = currentIcon.GetComponent<SelectableIcon>();
        //if (select)
        //    select.relativeSkill = skill;

        currentIcon.transform.parent = _relativePanel.transform;
        //currentIcon.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //_iconList.Add(currentIcon);
    }
}
