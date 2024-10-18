using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageUser : SkillUser, IObjectCreator
{
    public GameObject effectPrefab { get; set; }
    public PassiveMirageTarget target { get; set; }

    private PlayerAttacker _attack;

    // Skill Tree System
    public float additionalDamage { get; set; }

    public bool isStartBomb { get; set; }
    public bool isEndBomb { get; set; }

    public float createTimeDecrease { get; set; }
    public float mirageSpeedIncrease { get; set; }

    public bool isOneMore { get; set; }
    public bool isSkillUse { get; set; }

    protected override void Start()
    {
        base.Start();
        
        effectPrefab = Resources.Load<GameObject>("Player/Skills/SkillUser/PassiveMirageUser/PassiveMirageCreator");
        
        GameObject targetPrefab = Resources.Load<GameObject>("Player/Bullets/Mirage/Prefab/PassiveMirageTarget");
        target = Instantiate(targetPrefab).GetComponent<PassiveMirageTarget>();

        _attack = transform.parent.GetComponentInParent<PlayerAttacker>();
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate(effectPrefab);
        newBullet.transform.position = _attack.attackStartPos;

        return newBullet;
    }

    public override void UseSkill()
    {
        target.transform.position = _attack.transform.position;

        GameObject mirage = CreateObject();

        PassiveMirageCreator creator = mirage.GetComponent<PassiveMirageCreator>();
        creator.owner = this;
        creator.timeDecrease = createTimeDecrease;
        creator.isOneMore = isOneMore;
    }
}
