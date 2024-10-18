using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveShieldController : MonoBehaviour, IObjectCreator
{
    private Damagable _player;
    private Rigidbody2D _playerRigid;

    public ActiveShieldUser user { get; set; }

    [SerializeField]
    private GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    void OnEnable()
    {
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        _player = player.GetComponent<Damagable>();
        
        _player.isCanHurt = false;

        _playerRigid = _player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (user.isCanMoving)
        {
            Vector2 dir = Vector2.zero;
            if (KeyManager.instance.IsStayKeyDown("left"))
                dir += Vector2.left;

            if (KeyManager.instance.IsStayKeyDown("right"))
                dir += -Vector2.left;

            if (KeyManager.instance.IsStayKeyDown("up"))
                dir += Vector2.up;

            if (KeyManager.instance.IsStayKeyDown("down"))
                dir += -Vector2.up;

            dir = dir.normalized;

            _playerRigid.velocity = dir * user.speed;
        }
    }

    void OnDestroy()
    {
        _player.isCanHurt = true;

        if (user.isEndKnockback)
            CreateObject();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Bullets")) return;

        // test
        // 일단 투사체 판정되는 녀석들만 걸러주기
        BulletMovable movable = collision.gameObject.GetComponent<BulletMovable>();
        if (!movable) return;
        if (!movable.isRemovable) return;

        movable.DestroyBullet();
    }

    public GameObject CreateObject()
    {
        GameObject knockbackBullet = Instantiate(effectPrefab);
        knockbackBullet.transform.position = _player.GetComponent<Movable>().center;

        return knockbackBullet;
    }
}
