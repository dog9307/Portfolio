using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillUser : MonoBehaviour
{
    protected BuffInfo _buff;
    public BuffInfo buff { get { return _buff; }  set { _buff = value; } }
    protected float _damageMultiplier;
    public float damageMultiplier { get { return _damageMultiplier; } set { _damageMultiplier = value; } }

    public abstract void UseSkill();

    public bool isMirage { get; set; }

    protected SkillInfo _info;
    public SkillUserManager manager { get; set; }

    [SerializeField]
    protected SFXPlayer _sfx;
    [SerializeField]
    protected string _sfxName;

    public virtual void Init() { }

    protected virtual void Start()
    {
        _info = transform.parent.GetComponentInParent<SkillInfo>();
        if (typeof(MirageSkillInfo).IsInstanceOfType(_info))
        {
            _damageMultiplier = transform.parent.GetComponentInParent<MirageController>().damageMultiplier;
            isMirage = true;
        }
        else
            _damageMultiplier = 1.0f;

        _buff = transform.parent.GetComponentInParent<BuffInfo>();
    }

    public virtual void SkillEnd() { }

    // Skill Tree System
    protected List<SkillBuffBase> _skillBuffs = new List<SkillBuffBase>();
    public List<SkillBuffBase> skillBuffs { get { return _skillBuffs; } }

    public void AddBuff(SkillBuffBase buff)
    {
        buff.owner = this;
        buff.BuffOn();
        _skillBuffs.Add(buff);
    }

    public SkillBuffBase FindBuff(string name)
    {
        SkillBuffBase find = null;
        foreach (SkillBuffBase buff in _skillBuffs)
        {
            if (buff.GetType().Name == name)
            {
                find = buff;
                break;
            }
        }

        return find;
    }


    public SkillBuffBase FindBuff(SkillBuffBase want)
    {
        SkillBuffBase find = null;
        foreach (SkillBuffBase buff in _skillBuffs)
        {
            if (buff == want)
            {
                find = buff;
                break;
            }
        }

        return find;
    }

    public void RemoveBuff(string name)
    {
        SkillBuffBase buff = FindBuff(name);
        if (buff == null) return;

        buff.BuffOff();
        _skillBuffs.Remove(buff);
    }
    
    public void RemoveBuff(SkillBuffBase remove)
    {
        SkillBuffBase buff = FindBuff(remove);
        if (buff == null) return;

        buff.BuffOff();
        _skillBuffs.Remove(buff);
    }

    public void RemoveAll()
    {
        foreach (SkillBuffBase buff in _skillBuffs)
            buff.BuffOff();

        _skillBuffs.Clear();
    }
}
