using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryingBulletController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _mask;

    private Collider2D _collider;
    private List<Collider2D> _cols = new List<Collider2D>();
    private ContactFilter2D _filter = new ContactFilter2D();

    [SerializeField]
    private float _stunTotalTime = 0.5f;
    
    void Start()
    {
        _collider = GetComponent<Collider2D>();

        _filter.useTriggers = true;
        _filter.useLayerMask = true;
        _filter.layerMask = _mask;
    }

    void FixedUpdate()
    {
        _cols.Clear();

        Physics2D.OverlapCollider(_collider, _filter, _cols);
        foreach (var col in _cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Bullets"))
            {
                Damager damager = col.GetComponent<Damager>();
                if (!damager)
                {
                    Damager[] temp = col.GetComponentsInChildren<Damager>();
                    if (temp.Length != 0)
                        damager = temp[0];
                }

                col.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");

                BulletMovable move = col.GetComponent<BulletMovable>();
                if (!move) continue;
                if (!move.isMoveBullet) continue;

                if (damager)
                    damager.owner = OWNER.PLAYER;

                float speed = move.GetSpeed();
                Vector2 moveDir = CommonFuncs.CalcDir(this, move);

                ParryedBulletMovable newMove = col.gameObject.AddComponent<ParryedBulletMovable>();
                move.CopyMovable(newMove);

                //newMove.speed = speed;
                newMove.speed = 20.0f;
                newMove.dir = moveDir;
                newMove.destroy = move.DestroyBullet;

                move.enabled = false;

                newMove.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
                newMove.transform.Rotate(new Vector3(0.0f, 0.0f, CommonFuncs.DirToDegree(moveDir) + 90.0f));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParryingStunController stun = collision.GetComponent<ParryingStunController>();
        if (!stun) return;

        stun.StunStart(_stunTotalTime);
    }
}
