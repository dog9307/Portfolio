using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCoolTimeDownUser : ActiveUserBase, IObjectCreator
{
    public GameObject effectPrefab { get; set; }
    public ActiveCoolTimeDown skill { get; set; }
    
    private HAND _hand;

    // Skill Tree System
    public float[] slotsAdditiveFigure { get; set; }

    public int itSelfIndex { get; set; }
    public bool isSelfDown { get; set; }

    public bool isRandomReset { get; set; }
    private List<ActiveBase> _randomList = new List<ActiveBase>();
    private bool _isItselfReset;

    protected override void Start()
    {
        base.Start();

        effectPrefab = Resources.Load<GameObject>("Player/Skills/Effects/CoolTimeDown/CoolTimeDown");
        skill = _info.FindSkill<ActiveCoolTimeDown>();
        skill.user = this;

        PlayerSkillUsage playerSkillUsage = _info.GetComponent<PlayerSkillUsage>();
        _hand = playerSkillUsage.GetSkillHand<ActiveCoolTimeDown>();
        itSelfIndex = 0;
        for (int i = 0; i < 5 && i < _info.activeSkills.Count; ++i)
        {
            int index = i + (int)_hand * 5;
            if (_info.activeSkills[index] == skill)
                itSelfIndex = i;
        }

        slotsAdditiveFigure = new float[5];
    }

    public GameObject CreateObject()
    {
        GameObject effect = Instantiate(effectPrefab);
        effect.transform.position = FindObjectOfType<PlayerMoveController>().transform.position;

        return effect;
    }

    public override void UseSkill()
    {
        CreateObject();
        
        PlayerSkillUsage playerSkillUsage = _info.GetComponent<PlayerSkillUsage>();
        List<ICoolTime> users = playerSkillUsage.FindCoolTimeUsers();
        foreach (var user in users)
        {
            if (typeof(ActiveCoolTimeDownUser).IsInstanceOfType(user)) continue;

            user.CoolTimeDown(skill.figure);
        }

        if (isRandomReset)
        {
            int rnd = Random.Range(0, users.Count);

            ICoolTime active = users[rnd];
            if (active == this)
                _isItselfReset = true;
            else
                active.CoolTimeDown(active.totalCoolTime);
        }
    }

    public void SetAdditiveFigure(int index, float figure)
    {
        slotsAdditiveFigure[index] += figure;

        ActiveBase active = _info.activeSkills[(int)_hand * 5 + index] as ActiveBase;
        if (!_randomList.Contains(active))
            _randomList.Add(active);

        if (slotsAdditiveFigure[index] < 0.0f)
        {
            slotsAdditiveFigure[index] = 0.0f;
            if (index == itSelfIndex)
                isSelfDown = false;
        }
    }

    public void SelfDown()
    {
        if (!isSelfDown) return;

        if (_isItselfReset)
        {
            currentCoolTime = totalCoolTime;
            _isItselfReset = false;
        }
        else
            currentCoolTime = slotsAdditiveFigure[itSelfIndex];
    }

    public override void CoolTimeStart()
    {
        base.CoolTimeStart();
        SelfDown();
    }
}
