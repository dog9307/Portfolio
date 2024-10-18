using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReturnPosHelper : MonoBehaviour
{
    private PlayerMoveController _player;

    [SerializeField]
    private Transform _returnPos;
    public Transform returnPos { get { return _returnPos; } set { _returnPos = value; } }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            _player = player;
            SetReturnPos();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            _player = player;
            SetReturnPos();
        }
    }

    void SetReturnPos()
    {
        if (!_returnPos) return;

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _player.dashStartPos = _returnPos.position;
    }
}
