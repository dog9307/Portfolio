using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FloorDirection
{
    NONE = -1,
    LeftUp,
    RightUp,
    UpUp,
    DownUp,
    END
}

public class FloorControlSignal : MonoBehaviour
{
    [SerializeField]
    private FloorManager manager;

    [SerializeField]
    private FloorDirection _dir = FloorDirection.NONE;
    [SerializeField]
    private Collider2D _relativeCol;

    private float _downSide;
    private float _upSide;

    [SerializeField]
    private int _downFloor;
    [SerializeField]
    private int _upFloor;

    static private PlayerMoveController _player;

    private int _readyFloor;
    private int _enterFloor;

    void Start()
    {
        if (!manager)
            manager = FindObjectOfType<FloorManager>();

        if (!_relativeCol)
            _relativeCol = GetComponent<Collider2D>();

        switch (_dir)
        {
            case FloorDirection.LeftUp:
                _downSide = _relativeCol.bounds.max.x;
                _upSide = _relativeCol.bounds.min.x;
            break;

            case FloorDirection.RightUp:
                _downSide = _relativeCol.bounds.min.x;
                _upSide = _relativeCol.bounds.max.x;
            break;

            case FloorDirection.UpUp:
                _downSide = _relativeCol.bounds.min.y;
                _upSide = _relativeCol.bounds.max.y;
            break;

            case FloorDirection.DownUp:
                _downSide = _relativeCol.bounds.max.y;
                _upSide = _relativeCol.bounds.min.y;
            break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!manager)
        {
            manager = FindObjectOfType<FloorManager>();
            if (!manager) return;
        }

        _player = collision.GetComponent<PlayerMoveController>();
        if (!_player) return;

        float playerPos = 0.0f;
        if (_dir == FloorDirection.RightUp ||
            _dir == FloorDirection.LeftUp)
            playerPos = _player.center.x;
        else if (_dir == FloorDirection.UpUp ||
                 _dir == FloorDirection.DownUp)
            playerPos = _player.center.y;

        bool isColorVChange = false;
        switch (_dir)
        {
            case FloorDirection.LeftUp:
            case FloorDirection.DownUp:
                if (playerPos > _downSide)
                    _readyFloor = _upFloor;
                else if (playerPos < _upSide)
                {
                    _readyFloor = _downFloor;
                    isColorVChange = true;
                }
            break;

            case FloorDirection.RightUp:
            case FloorDirection.UpUp:
                if (playerPos < _downSide)
                    _readyFloor = _upFloor;
                else if (playerPos > _upSide)
                {
                    isColorVChange = true;
                    _readyFloor = _downFloor;
                }
            break;
        }
        manager.EnterFloor(_readyFloor, isColorVChange);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!manager)
        {
            manager = FindObjectOfType<FloorManager>();
            if (!manager) return;
        }

        _player = collision.GetComponent<PlayerMoveController>();
        if (!_player) return;

        float playerPos = 0.0f;
        if (_dir == FloorDirection.RightUp ||
            _dir == FloorDirection.LeftUp)
            playerPos = _player.center.x;
        else if (_dir == FloorDirection.UpUp ||
                 _dir == FloorDirection.DownUp)
            playerPos = _player.center.y;

        bool isColorVChange = false;
        switch (_dir)
        {
            case FloorDirection.LeftUp:
            case FloorDirection.DownUp:
                if (playerPos < _upSide)
                {
                    isColorVChange = true;
                    _enterFloor = _upFloor;
                }
                else if (playerPos > _downSide)
                    _enterFloor = _downFloor;
            break;

            case FloorDirection.RightUp:
            case FloorDirection.UpUp:
                if (playerPos > _upSide)
                {
                    isColorVChange = true;
                    _enterFloor = _upFloor;
                }
                else if (playerPos < _downSide)
                    _enterFloor = _downFloor;
            break;
        }
        manager.EnterFloor(_enterFloor, isColorVChange);
    }
}
