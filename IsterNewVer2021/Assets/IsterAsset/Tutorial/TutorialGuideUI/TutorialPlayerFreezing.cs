using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerFreezing : MonoBehaviour
{
    private PlayerMoveController _player;

    [SerializeField]
    private bool _isFreezingOnEnable = false;

    private void OnEnable()
    {
        if (_isFreezingOnEnable)
            Freezing();
    }

    public void Freezing()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        if (_player)
            _player.PlayerMoveFreeze(true);
    }

    public void UnFreezing()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        if (_player)
            _player.PlayerMoveFreeze(false);
    }
}
