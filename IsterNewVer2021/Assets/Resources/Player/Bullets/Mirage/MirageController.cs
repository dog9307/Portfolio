using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageController : Movable
{
    public Vector2 dir { get; set; }
    public HAND currentHand { get; set; }

    public float damageMultiplier { get; set; }

    protected MirageSkillInfo _skillInfo;
    protected PlayerAttacker _player;
    protected NormalBulletCreator _creator;

    public bool isSecondShoot { get; set; }

    public virtual void Start()
    {
        _player = FindObjectOfType<PlayerAttacker>();
        _speed = _player.startSpeed;
        _skillInfo = GetComponent<MirageSkillInfo>();

        _creator = GetComponentInChildren<NormalBulletCreator>();

        isHide = true;
    }

    public virtual void MultiplierIncrease()
    {
        isHide = false;

        _speed = 0.0f;
        GetComponent<Animator>().SetFloat("attackMultiplier", 1.0f * 1.5f * _player.attackSpeedMultiplier);

        _creator.CreateObject();
        _creator.damageMultiplier = damageMultiplier;

        if (!_skillInfo) return;

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

    protected override void ComputeVelocity()
    {
        _targetVelocity = dir * _speed;
    }
}
