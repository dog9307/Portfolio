using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGravityUser : FloorSkillUser
{
    [SerializeField]
    private float _force;
    public float additionalForce { get; set; }
    public float totalForce { get { return _force + additionalForce; } }

    public float additionalTime { get; set; }
    public float scaleFactor { get; set; }

    public bool isRageMode { get; set; }
    public bool isElectricMode { get; set; }

    public bool isEndBomb { get; set; }
    public bool isDebuffDamaging { get; set; }

    protected override void Start()
    {
        base.Start();

        SkillInfo info = FindObjectOfType<PlayerSkillUsage>().GetComponent<SkillInfo>();
        skill = info.FindSkill<ActiveGravity>();

        scaleFactor = 1.0f;
    }
}
