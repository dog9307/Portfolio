using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCoolTimeDownNode : StackedSkillBuffNode
{
    public bool isNotActiveSkill { get; set; }
    public bool isItSelf { get; set; }

    public bool isIgnore { get { return (isNotActiveSkill || isItSelf); } }

    [SerializeField]
    private int _skillIndex;

    protected override void Start()
    {
        base.Start();

        PlayerSkillUsage player = FindObjectOfType<PlayerSkillUsage>();
        if (!player) return;

        ActiveCoolTimeDown coolDown = player.FindSkill<ActiveCoolTimeDown>();
        if (coolDown == null) return;

        HAND hand = player.GetSkillHand<ActiveCoolTimeDown>();
        int index = _skillIndex + (int)hand * 5;
        if (index >= player.activeSkills.Count)
            isNotActiveSkill = true;
        else if (player.activeSkills[index] == coolDown)
            isItSelf = true;
        else if (!typeof(ActiveBase).IsInstanceOfType(player.activeSkills[index]))
            isNotActiveSkill = true;

        if (isIgnore)
            SetNodeCantActivated(true);
    }

    public override bool IsCanActivate()
    {
        if (isIgnore) return false;

        return base.IsCanActivate();
    }

    public override bool IsCanNonActivate()
    {
        if (isIgnore) return false;

        return base.IsCanNonActivate();
    }
}
