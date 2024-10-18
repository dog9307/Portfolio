using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo : MonoBehaviour
{
    [SerializeField]
    protected SkillUserManager _skillManager;

    protected List<SkillBase> _activeSkills = new List<SkillBase>(8);
    public List<SkillBase> activeSkills { get { return _activeSkills; } }

    protected List<SkillBase> _passiveSkills = new List<SkillBase>();
    public List<SkillBase> passiveSkills { get { return _passiveSkills; } }

    public void ActiveDashGain()
    {
        _skillManager.AddUser(typeof(ActiveDash));
    }

    public SkillBase FindSkill(System.Type skill)
    {
        foreach (var s in activeSkills)
        {
            if (skill.IsInstanceOfType(s))
                return s;
        }

        foreach (var s in passiveSkills)
        {
            if (skill.IsInstanceOfType(s))
                return s;
        }

        return null;
    }

    public T FindSkill<T>() where T : SkillBase
    {
        T skill = null;
        foreach (var s in activeSkills)
        {
            if (typeof(T).IsInstanceOfType(s))
            {
                skill = (T)s;
                break;
            }
        }
        if (skill != null)
            return skill;

        foreach (var s in passiveSkills)
        {
            if (typeof(T).IsInstanceOfType(s))
            {
                skill = (T)s;
                break;
            }
        }

        return skill;
    }

    public List<T> FindSkills<T>(System.Type except = null) where T : SkillBase
    {
        List<SkillBase> tempList = null;
        if (typeof(T).BaseType == typeof(PassiveBase))
            tempList = _passiveSkills;
        if (typeof(T).BaseType == typeof(ActiveBase))
            tempList = _activeSkills;

        if (tempList == null)
            return null;

        List<T> temp = new List<T>();

        foreach (var s in tempList)
        {
            if (except != null)
            {
                if (except.IsInstanceOfType(s))
                    continue;
            }

            temp.Add((T)s);

            //if (typeof(T).IsInstanceOfType(s))
            //{
            //    if (except == null)
            //        temp.Add((T)s);
            //    else
            //    {
            //        if (!except.IsInstanceOfType(s))
            //            temp.Add((T)s);
            //    }
            //}
        }

        return temp;
    }

    public List<T> FindSkills<T>(HAND hand, System.Type except = null) where T : SkillBase
    {
        List<T> temp = new List<T>();

        for (int i = 0; i < 5 && i < activeSkills.Count; ++i)
        {
            int index = i + (int)hand;
            SkillBase s = activeSkills[index];
            if (typeof(T).IsInstanceOfType(s))
            {
                if (except == null)
                    temp.Add((T)s);
                else
                {
                    if (!except.IsInstanceOfType(s))
                        temp.Add((T)s);
                }
            }
        }

        return temp;
    }

    public void SkillChange(SkillBase before, SkillBase after)
    {
        for (int i = 0; i < _activeSkills.Count; ++i)
        {
            if (_activeSkills[i] == before)
            {
                _skillManager.ChangeUser(before.GetType(), after.GetType());
                _activeSkills[i] = after;
                break;
            }
        }
    }

    public void PassiveSkillChange(SkillBase before, SkillBase after)
    {
        if (before == null)
        {
            if (after != null)
                AddSkill(after);
        }
        else
        {
            _skillManager.RemoveUser(before.GetType());
            _passiveSkills.Clear();
            if (after != null)
                AddSkill(after);
        }
    }

    public bool ActiveSkillChange(SkillBase before, SkillBase after, int targetIndex)
    {
        if (before == null && after == null) return false;

        bool isSuccess = false;
        if (before == null || after == null)
        {
            if (before == null)
            {
                isSuccess = true;
                _skillManager.ChangeUser(null, after.GetType());
            }
            else if (after == null)
            {
                isSuccess = true;
                _skillManager.ChangeUser(before.GetType(), null);
            }
        }
        else
        {
            isSuccess = true;
            _skillManager.ChangeUser(before.GetType(), after.GetType());
        }

        if (after != null)
        {
            after.owner = this;
            after.Init();
        }
        _activeSkills[targetIndex] = after;

        return isSuccess;
    }

    public void AddSkill(SkillBase skill)
    {
        if (typeof(ActiveBase).IsInstanceOfType(skill))
            _activeSkills.Add(skill);
        else if (typeof(PassiveBase).IsInstanceOfType(skill))
        {
            _passiveSkills.Add(skill);

            if (typeof(BuffPassive).IsInstanceOfType(skill))
                skill.UseSkill();
        }

        _skillManager.AddUser(skill.GetType());
    }

    public void ClearList()
    {
        foreach (var skill in _activeSkills)
            _skillManager.RemoveUser(skill.GetType());
        _activeSkills.Clear();

        foreach (var skill in _passiveSkills)
            _skillManager.RemoveUser(skill.GetType());
        _passiveSkills.Clear();
    }
}
