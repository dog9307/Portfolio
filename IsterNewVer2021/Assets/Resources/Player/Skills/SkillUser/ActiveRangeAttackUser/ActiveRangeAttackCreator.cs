using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRangeAttackCreator : FloorSkillCreator
{
    // Start is called before the first frame update
    void Start()
    {
        effectPrefab = Resources.Load<GameObject>("Player/Bullets/RangeAttack/Prefab/RangeAttackTimer");
    }

    public override GameObject CreateObject()
    {
        GameObject newBullet = GameObject.Instantiate(effectPrefab);
        newBullet.transform.position = _skillPos.transform.position;
        Vector3 scale = newBullet.transform.localScale;
        scale.z = scale.x;
        newBullet.transform.localScale = scale;

        RangeAttackTimer timer = newBullet.GetComponent<RangeAttackTimer>();
        timer.user = (ActiveRangeAttackUser)_owner;

        return newBullet;
    }
}
