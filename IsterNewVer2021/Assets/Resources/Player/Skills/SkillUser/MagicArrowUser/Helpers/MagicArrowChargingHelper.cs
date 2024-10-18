using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowChargingHelper : MagicArrowUserHelperBase
{
    public GameObject bulletPrefab { get; set; }

    public override GameObject CreateObject()
    {
        ChargingArrowCreator charging = owner.GetComponentInChildren<ChargingArrowCreator>();
        if (charging) return null;

        GameObject newBullet = GameObject.Instantiate(effectPrefab);
        newBullet.transform.parent = owner.transform;
        newBullet.transform.localPosition = Vector2.zero;

        return newBullet;
    }

    public override void Init()
    {
        PlayerAnimController player = GameObject.FindObjectOfType<PlayerMoveController>().GetComponent<PlayerAnimController>();
        player.Charging(true);

        if (!effectPrefab)
            effectPrefab = Resources.Load<GameObject>("Player/Bullets/MagicArrow/ChargingArrow/Prefab/ChargingEffect");

        if (!bulletPrefab)
            bulletPrefab = Resources.Load<GameObject>("Player/Bullets/MagicArrow/NormalMagicArrow/Prefab/MagicArrow");
    }

    public override void Release()
    {
    }

    public override void Update()
    {
        owner.transform.localPosition = Vector2.zero;
    }

    public override void UseSkill(GameObject newBullet)
    {
        ChargingArrowCreator charging = newBullet.GetComponent<ChargingArrowCreator>();
        charging.owner = owner;
        charging.effectPrefab = bulletPrefab;
    }
}
