using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FloorSkillUser : ActiveUserBase
{
    [SerializeField]
    protected FloorSkillCreator _creator;

    public FloorActive skill { get; set; }
    public bool isSkillStart { get; set; }
    public string skillKey { get; set; }

    protected const int FRAME_LIMIT = 10;
    protected int _frameCount;
    public int frameCount { get { return _frameCount; } }

    protected override void Start()
    {
        base.Start();
        
        isSkillStart = false;
    }

    void Update()
    {
        if (!isSkillStart) return;

        // before skill rotation
        //if (!KeyManager.instance.IsStayKeyDown(skillKey))
        // after skill rotation
        if (!KeyManager.instance.IsStayKeyDown("skill_use"))
        {
            CreateObject();
            SkillEnd();

            if (_frameCount < FRAME_LIMIT)
                _creator.EffectPos(PlayerSkillUsage.useMousePointToWorld);
        }
        else
            _frameCount++;
    }
    public void FrameCountReset()
    {
        _frameCount = 0;
    }

    public override void SkillEnd()
    {
        if (!isSkillStart) return;

        PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
        playerSkill.SkillEnd(skill.GetType());

        PlayerAnimController playerAnimController = FindObjectOfType<PlayerAnimController>();
        playerAnimController.SkillEnd();

        _creator.ScaleEnd();
        isSkillStart = false;
    }
    
    public virtual void CreateObject()
    {
        if (!isSkillStart) return;

        GameObject newBullet = _creator.CreateObject();

        if (_frameCount < FRAME_LIMIT)
            newBullet.transform.position = PlayerSkillUsage.useMousePointToWorld;

        // test
        // 액티브스킬 Bullet Creator들에게 IsPowerUp일 경우 처리 다 넣어주기
        SkillInfo info = FindObjectOfType<PlayerSkillUsage>().GetComponent<SkillInfo>();
        float scale = 1.0f;
        if (skill.isReversePowerUp)
        {
            int figure = (int)info.FindSkill<PassiveReversePower>().figure;
            // test
                 if (figure == 0) scale = 1.2f;
            else if (figure == 1) scale = 1.4f;
            else if (figure == 2) scale = 1.6f;
            else if (figure == 3) scale = 2.0f;
        }
        Vector3 localScale = newBullet.transform.localScale;
        localScale.x *= scale;
        localScale.y *= scale;
        localScale.z *= scale;
        newBullet.transform.localScale = localScale;

        float damageMultiplier = _damageMultiplier;

        float additionalDamage = 0.0f;

        if (_buff)
        {
            additionalDamage += _buff.additionalSkillDamage;
            damageMultiplier *= _buff.skillDamageMultiplier;
        }

        Damager damager = newBullet.GetComponent<Damager>();
        if (!damager) return;

        damager.damage = DamageCreator.Create(damager.gameObject, ((ActiveRangeAttack)skill).damage, additionalDamage, damageMultiplier, 1.0f);
    }
    
    public override void UseSkill()
    {
        if (isSkillStart) return;

        _creator.gameObject.SetActive(true);
        _creator.ScaleStart();
        isSkillStart = true;

        // before skill rotation
        //SkillInfo info = FindObjectOfType<PlayerSkillUsage>().GetComponent<SkillInfo>();

        //int index = -1;
        //for (int i = 0; i < info.activeSkills.Count; ++i)
        //{
        //    if (skill.GetType().IsInstanceOfType(info.activeSkills[i]))
        //    {
        //        index = i;
        //        break;
        //    }
        //}
        //if (index == -1) return;

        //skillKey = "skill" + ((index % 5) + 1);
    }
}
