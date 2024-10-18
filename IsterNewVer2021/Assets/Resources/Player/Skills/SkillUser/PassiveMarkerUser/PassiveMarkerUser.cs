using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMarkerUser : SkillUser, IObjectCreator
{
    public GameObject effectPrefab { get; set; }
    public GameObject targetEnemy { get; set; }

    public float additionalDamage { get; set; }

    public bool isTimeBomb { get; set; }
    public bool isRageMode { get; set; }
    public bool isDebuffRemover { get; set; }

    public bool isDamageProportion { get; set; }

    public bool isNewMarker { get; set; }
    public bool isRemoveAll { get; set; }

    //public class MarkerUserHandChanger : SkillUserHandChanger<PassiveMarker>
    //{
    //    public override void BuffOn()
    //    {
    //        _buff.isMarkerOn = true;
    //    }

    //    public override void BuffOff()
    //    {
    //        _buff.isMarkerOn = false;
    //    }
    //}

    protected override void Start()
    {
        base.Start();

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/DamageMarker/Prefab/DamageMarker");

        _buff.isMarkerOn = true;

        //DualSwordSetting<MarkerUserHandChanger>();
    }

    public GameObject CreateObject()
    {
        GameObject marker = Instantiate(effectPrefab);
        marker.transform.parent = targetEnemy.transform;
        marker.transform.localPosition = Vector3.zero;

        return marker;
    }

    public override void UseSkill()
    {
        DamageMarker marker = targetEnemy.GetComponentInChildren<DamageMarker>();
        if (!marker)
        {
            marker = CreateObject().GetComponent<DamageMarker>();

            PassiveMarkerUser user = FindObjectOfType<PlayerSkillUsage>().FindUser<PassiveMarker>() as PassiveMarkerUser;
            marker.passiveUser = user;
        }
    }
}
