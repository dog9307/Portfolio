using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryingController : MonoBehaviour
{
    [SerializeField]
    private ParryingShieldController _controller;

    [SerializeField]
    private LayerMask _mask;

    private BoxCollider2D _collider;
    private List<Collider2D> _cols = new List<Collider2D>();
    private ContactFilter2D _filter = new ContactFilter2D();

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();

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
                Vector2 bulletToShieldDir = CommonFuncs.CalcDir(col, transform.parent);
                if (!_controller.IsCorrectDirection(bulletToShieldDir)) continue;

                _controller.isParryingSuccess = true;
                _controller.gameObject.SetActive(false);

                Damager damager = col.GetComponent<Damager>();
                if (!damager)
                {
                    Damager[] temp = col.GetComponentsInChildren<Damager>();
                    if (temp.Length == 0) continue;

                    damager = temp[0];
                }

                col.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");

                BulletMovable move = col.GetComponent<BulletMovable>();
                if (!move) continue;
                if (!move.isMoveBullet) continue;

                damager.owner = OWNER.PLAYER;

                float speed = move.GetSpeed();
                Vector2 moveDir = CommonFuncs.CalcDir(this, move);

                ParryedBulletMovable newMove = col.gameObject.AddComponent<ParryedBulletMovable>();
                move.CopyMovable(newMove);

                newMove.speed = speed;
                newMove.dir = moveDir;
                newMove.destroy = move.DestroyBullet;

                move.enabled = false;

                newMove.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
                newMove.transform.Rotate(new Vector3(0.0f, 0.0f, CommonFuncs.DirToDegree(moveDir) + 90.0f));
            }
        }
    }
}
