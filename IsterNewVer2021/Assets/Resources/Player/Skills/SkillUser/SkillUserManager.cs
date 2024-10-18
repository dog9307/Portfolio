using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUserManager : MonoBehaviour
{
    private List<SkillUser> _users = new List<SkillUser>();

    // type == SkillBase
    public void AddUser(System.Type type)
    {
        if (type == null) return;

        string userName = type.Name + "User";
        string path = "Player/Skills/SkillUser/" + userName + "/";
        GameObject prefab = Resources.Load<GameObject>(path + userName);
        GameObject newUser = Instantiate(prefab);
        newUser.transform.parent = transform;
        newUser.transform.localPosition = Vector3.zero;

        SkillUser user = newUser.GetComponent<SkillUser>();
        user.manager = this;
        _users.Add(user);
    }

    // type == SkillBase
    public void RemoveUser(System.Type type)
    {
        SkillUser remove = FindUser(type);
        if (remove)
        {
            remove.RemoveAll();
            _users.Remove(remove);
            Destroy(remove.gameObject);
        }
    }

    // type == SkillBase
    public SkillUser FindUser(System.Type type)
    {
        SkillUser find = null;
        string userName = type.Name + "User";
        foreach (var user in _users)
        {
            if (user == null) continue;

            if (userName.Equals(user.GetType().Name))
            {
                find = user;
                break;
            }
        }

        return find;
    }

    public SkillUser FindUser(string typeName)
    {
        SkillUser find = null;
        string userName = typeName + "User";
        foreach (var user in _users)
        {
            if (user == null) continue;

            if (userName.Equals(user.GetType().Name))
            {
                find = user;
                break;
            }
        }

        return find;
    }

    public List<T> FindUsers<T>(System.Type except = null) where T : SkillUser
    {
        List<T> temp = new List<T>();
        foreach (var user in _users)
        {
            if (typeof(T).IsInstanceOfType(user))
            {
                if (except == null)
                    temp.Add((T)user);
                else
                {
                    if (!except.IsInstanceOfType(user))
                        temp.Add((T)user);
                }
            }
        }

        return temp;
    }


    public List<ICoolTime> FindCoolTimeUsers()
    {
        List<ICoolTime> users = new List<ICoolTime>();
        foreach (var user in _users)
        {
            if (typeof(ICoolTime).IsInstanceOfType(user))
                users.Add((ICoolTime)user);
        }

        return users;
    }

    // type == SkillBase
    public void UseSkill(System.Type type)
    {
        SkillUser user = FindUser(type);
        if (!user) return;

        user.UseSkill();
    }

    // before, after == SkillBase
    public void ChangeUser(System.Type before, System.Type after)
    {
        if (before != null)
            RemoveUser(before);
        AddUser(after);
    }

    public void SkillEndAll(List<SkillUser> except = null)
    {
        foreach (SkillUser user in _users)
        {
            if (except != null)
            {
                bool isContinue = false;
                foreach (SkillUser ex in except)
                {
                    if (!ex) continue;

                    if (ex == user)
                    {
                        isContinue = true;
                        break;
                    }
                }

                if (isContinue) continue;
            }

            user.SkillEnd();
        }
    }
}
