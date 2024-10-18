using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairObject : MonoBehaviour
{
    [SerializeField]
    private Transform _upSide;
    [SerializeField]
    private Transform _downSide;

    [SerializeField]
    private float _ratio = 0.7f;

    private PlayerMoveController _player;
    private DebuffInfo _playerDebuff;

    public Vector2 dir { get { return CommonFuncs.CalcDir(_downSide, _upSide); } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            _player = player;

            if (!_playerDebuff)
                _playerDebuff = player.GetComponent<DebuffInfo>();

            _playerDebuff.AddSlow(_ratio);
            _player.additionalDir = dir;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            _player = player;

            if (!_playerDebuff)
                _playerDebuff = player.GetComponent<DebuffInfo>();

            _playerDebuff.RemoveSlow(_ratio);
            _player.additionalDir = Vector2.zero;
        }
    }
}
