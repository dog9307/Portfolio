using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveShieldUser : ActiveUserBase, IObjectCreator
{
    public GameObject effectPrefab { get; set; }

    private ActiveShield _skill;
    public bool isStayKey { get; set; }
    public GameObject shield { get; set; }
    public bool isSkillUsing { get { return (shield != null); } }

    public string skillKey { get; set; }

    private EventTimer _shieldTimer;
    public float additionalTime { get; set; }

    public float coolTimeMultipler { get; set; } = 1.0f;

    public bool isCanMoving { get; set; }
    public float speed { get; set; }

    public bool isEndKnockback { get; set; }

    public override float totalCoolTime
    {
        get { return base.totalCoolTime * coolTimeMultipler; }
        set => base.totalCoolTime = value;
    }

    protected override void Start()
    {
        base.Start();

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/ActiveShield/Prefab/ActiveShield");

        SkillInfo info = FindObjectOfType<PlayerMoveController>().GetComponent<SkillInfo>();
        _skill = info.FindSkill<ActiveShield>();
        shield = null;

        isStayKey = false;
        coolTimeMultipler = 1.0f;
    }

    void Update()
    {
        if (!isStayKey) return;
        if (!isSkillUsing) return;

        // before skill rotation
        //if (!KeyManager.instance.IsStayKeyDown(skillKey))
        // after skill rotation
        if (!KeyManager.instance.IsStayKeyDown("skill_use"))
            _shieldTimer.TimeEnd();
    }

    public GameObject CreateObject()
    {
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        Vector3 pos = player.transform.position;
        pos.z = 0.0f;

        GameObject newBullet = GameObject.Instantiate(effectPrefab);
        newBullet.transform.parent = player.transform;
        newBullet.transform.localPosition = Vector3.zero;

        return newBullet;
    }

    public override void SkillEnd()
    {
        PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
        if (!playerSkill.isSkillUsing) return;

        Destroy(shield);

        playerSkill.SkillEnd(typeof(ActiveShield));
    }

    public override void UseSkill()
    {
        shield = CreateObject();
        ActiveShieldController controller = shield.GetComponent<ActiveShieldController>();
        controller.user = this;

        PlayerAnimController playerAnimController = FindObjectOfType<PlayerAnimController>();

        _shieldTimer = shield.GetComponent<EventTimer>();
        _shieldTimer.totalTime += additionalTime;
        _shieldTimer.AddEvent(SkillEnd);
        _shieldTimer.AddEvent(playerAnimController.SkillEnd);

        // before skill rotation
        //SkillInfo info = playerAnimController.GetComponent<SkillInfo>();

        //int index = -1;
        //for (int i = 0; i < info.activeSkills.Count; ++i)
        //{
        //    if (typeof(ActiveShield).IsInstanceOfType(info.activeSkills[i]))
        //    {
        //        index = i;
        //        break;
        //    }
        //}
        //if (index == -1) return;

        //skillKey = "skill" + ((index % 5) + 1);
    }
}
