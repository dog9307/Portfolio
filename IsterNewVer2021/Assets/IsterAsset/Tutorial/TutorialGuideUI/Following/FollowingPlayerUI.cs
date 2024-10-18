using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayerUI : MonoBehaviour
{
    private PlayerMoveController _player;
    [SerializeField]
    private Vector2 _offset;
    [SerializeField]
    private bool _isFollowWithStart = false;

    public bool isFollow { get; set; }

    private Transform _originParent;

    // Start is called before the first frame update
    void Start()
    {
        _originParent = transform.parent;

        if (!_player)
        {
            PlayerFindHelper finder = FindObjectOfType<PlayerFindHelper>();
            if (finder)
                _player = finder.player.GetComponent<PlayerMoveController>();
        }

        if (_isFollowWithStart)
            FollowStart();
    }

    public void FollowStart()
    {
        if (isFollow) return;

        if (!_player)
        {
            PlayerFindHelper finder = FindObjectOfType<PlayerFindHelper>();
            if (finder)
                _player = finder.player.GetComponent<PlayerMoveController>();
        }

        if (_player)
        {
            isFollow = true;

            transform.parent = _player.transform;
            transform.localPosition = _offset;
        }
    }

    public void FollowEnd()
    {
        if (!isFollow) return;

        if (!_player)
        {
            PlayerFindHelper finder = FindObjectOfType<PlayerFindHelper>();
            if (finder)
                _player = finder.player.GetComponent<PlayerMoveController>();
        }

        if (_player)
        {
            isFollow = false;

            transform.parent = _originParent;
            transform.position = _player.transform.position + (Vector3)_offset;
        }
    }
}
