using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeCrackReverseGravity : MonoBehaviour
{
    private PlayerMoveController _player;
    private bool isPlayerIn { get { return _player; } }

    private float _currentForce = 0.0f;
    private float _currentAngle = 0.0f;
    [SerializeField]
    private float _frequency = 1.0f;

    private void LateUpdate()
    {
        _frequency = 1.5f;
        _currentAngle += 2.0f * Mathf.PI * Mathf.Rad2Deg * IsterTimeManager.enemyDeltaTime * _frequency;

        if (!_player) return;

        _currentForce = 10.0f * (Mathf.Sin(_currentAngle * Mathf.Deg2Rad) + 1.0f) / 2.0f;
        Vector2 dir = CommonFuncs.CalcDir(this, _player);

        _player.additionalForce = dir * _currentForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
            _player = player;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            _player.additionalForce = Vector2.zero;
            _player = null;
        }
    }
}
