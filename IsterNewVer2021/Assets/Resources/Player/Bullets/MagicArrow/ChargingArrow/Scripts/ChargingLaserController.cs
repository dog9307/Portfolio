using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingLaserController : MonoBehaviour
{
    [SerializeField]
    private Animator _bulletAnimator;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _bulletSpeed;
    public float bulletSpeed { get { return _bulletSpeed; } set { _bulletSpeed = value; } }

    [SerializeField]
    private float _playerSpeed;

    private PlayerMoveController _player;

    private Vector3 _bulletPos;

    void OnDestroy()
    {
        if (_player.GetComponent<PlayerSkillUsage>().isSkillUsing)
        {
            Animator anim = _player.GetComponent<Animator>();
            anim.SetTrigger("chargingLaserEnd");
        }
    }

    void Start()
    {
        if (_bulletAnimator)
            _bulletAnimator.SetFloat("animSpeed", _bulletSpeed * IsterTimeManager.enemyTimeScale);

        _player = FindObjectOfType<PlayerMoveController>();

        _bulletPos = _bulletAnimator.transform.localPosition;
    }

    void Update()
    {
        if (!_player) return;

        if (_bulletAnimator)
        {
            _bulletAnimator.SetFloat("animSpeed", _bulletSpeed * IsterTimeManager.enemyTimeScale);
            _bulletAnimator.transform.localPosition = _bulletPos;
        }

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

        Rigidbody2D rigid = _player.GetComponent<Rigidbody2D>();
        rigid.velocity = dir * _playerSpeed;

    }
}
