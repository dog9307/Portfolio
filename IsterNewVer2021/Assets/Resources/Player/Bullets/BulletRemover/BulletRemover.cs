using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRemover : MonoBehaviour
{
    public PassiveRemoverUser user { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Bullets")) return;

        // test
        // 일단 투사체 판정되는 녀석들만 걸러주기
        BulletMovable movable = collision.GetComponent<BulletMovable>();
        if (!movable) return;
        if (!movable.isRemovable) return;

        movable.DestroyBullet();
        if (user)
        {
            if (user.isStackOn)
                user.stackCount += 1;
        }
    }
}
