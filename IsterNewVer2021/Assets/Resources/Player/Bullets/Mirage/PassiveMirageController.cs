using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageController : MirageController, IObjectCreator
{
    public PassiveMirageTarget target { get; set; }
    public bool isArrive { get; set; }
    
    public bool isStartBomb { get; set; }
    public bool isEndBomb { get; set; }

    [SerializeField]
    private GameObject _bomb;
    public GameObject effectPrefab { get { return _bomb; } set { _bomb = value; } }

    public float additionalSpeed { get; set; }
    public bool isSkillUse { get; set; }

    public override void Start()
    {
        base.Start();

        if (isStartBomb)
            CreateObject();
    }

    protected override void ComputeVelocity()
    {
        if (!isArrive)
            dir = CommonFuncs.CalcDir(this, target);
        _targetVelocity = dir * (_speed + additionalSpeed);
    }

    public override void MultiplierIncrease()
    {
        if (isArrive) return;
        isArrive = true;
        isHide = false;

        _speed = 0.0f;
        if (isSkillUse)
            GetComponent<Animator>().SetFloat("attackMultiplier", 1.0f * 1.5f * _player.attackSpeedMultiplier);
        else
            GetComponent<Animator>().SetFloat("attackMultiplier", 1.0f * 1.5f);

        _creator.damageMultiplier = damageMultiplier;
        _creator.CreateObject();

        if (isEndBomb)
            CreateObject();

        if (!_skillInfo) return;

        if (!isSkillUse)
            _skillInfo.passiveSkills.Clear();
        
        List<SkillBase> skills = _skillInfo.passiveSkills;
        //int start = (int)currentHand * 5;
        for (int i = 0; i < skills.Count; ++i)
        {
            if (skills[i] == null) continue;

            if (typeof(AttackPassive).IsInstanceOfType(skills[i]))
            {
                if (typeof(PassiveMirage).IsInstanceOfType(skills[i])) continue;

                skills[i].UseSkill();
            }
        }
    }

    public GameObject CreateObject()
    {
        if (!_bomb) return null;

        GameObject newBomb = Instantiate(effectPrefab);
        newBomb.transform.position = transform.position;

        return newBomb;
    }
}
