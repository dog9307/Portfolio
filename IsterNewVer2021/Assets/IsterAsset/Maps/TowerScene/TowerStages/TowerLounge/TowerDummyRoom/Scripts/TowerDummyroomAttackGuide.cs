using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDummyroomAttackGuide : MonoBehaviour
{
    private PlayerAttacker _player;
    private LookAtMouse _look;

    [SerializeField]
    private FollowingPlayerUI _follow;
    [SerializeField]
    private FadingGuideUI _fade;

    private PassiveChargeAttackUser _charge;

    private bool _isPrevAttacking = false;
    private float _currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerAttacker>();
        _look = _player.GetComponent<LookAtMouse>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player || !_look) return;

        if (_player.isAttacking)
            _follow.FollowEnd();
        else
        {
            _follow.FollowStart();

            float angle = CommonFuncs.DirToDegree(_look.dir);
            _currentAngle = Mathf.LerpAngle(_currentAngle, angle, 0.3f);
            transform.localRotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, 0.0f, (_currentAngle - 90.0f)));
        }

        if (_isPrevAttacking)
        {
            if (!_player.isAttacking && !IsCharging())
                _fade.StartFading(1.0f);
        }
        else
        {
            if (_player.isAttacking || IsCharging())
                _fade.StartFading(0.0f);
        }

        _isPrevAttacking = _player.isAttacking || IsCharging();
    }

    private bool IsCharging()
    {
        if (!_charge)
            _charge = FindObjectOfType<PassiveChargeAttackUser>();

        if (!_charge)
            return false;

        return _charge.isChargingStart;
    }
}
