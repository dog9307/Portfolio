using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGravityCreator : FloorSkillCreator
{
    // Start is called before the first frame update
    void Start()
    {
        effectPrefab = Resources.Load<GameObject>("Player/Bullets/ActiveGravity/Prefab/ActiveGravity");
    }

    public override GameObject CreateObject()
    {
        GameObject newBullet = base.CreateObject();
        GravityController gravity = newBullet.GetComponentInChildren<GravityController>();
        gravity.user = (ActiveGravityUser)_owner;

        Vector3 scale = newBullet.transform.localScale;
        scale.x *= ((ActiveGravityUser)_owner).scaleFactor;
        scale.y *= ((ActiveGravityUser)_owner).scaleFactor;
        newBullet.transform.localScale = scale;

        return newBullet;
    }
}
