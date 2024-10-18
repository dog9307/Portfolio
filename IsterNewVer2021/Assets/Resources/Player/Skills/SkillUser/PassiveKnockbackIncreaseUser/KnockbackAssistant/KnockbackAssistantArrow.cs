using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackAssistantArrow : MonoBehaviour
{
    public Movable owner { get; set; }
    public NormalBulletCreator creator { get; set; }

    // Update is called once per frame
    void Update()
    {
        Vector2 ownerToBullet = CommonFuncs.CalcDir(owner, creator);
        Vector2 ownerToEnemy = CommonFuncs.CalcDir(owner, this);
        Vector2 dir = (ownerToBullet + ownerToEnemy).normalized;

        float angle = CommonFuncs.DirToDegree(dir);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        transform.Rotate(new Vector3(0.0f, 0.0f, angle));
    }

    public bool IsSameOwner(Movable move)
    {
        return (move == owner);
    }
}
