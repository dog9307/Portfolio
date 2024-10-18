using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveRange : MonoBehaviour
{
    private PlayerAttacker _player;
    [SerializeField]
    private TowerBattleEventController _battle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_battle.isBattleAllEnd) return;

        if (!_player)
            _player = FindObjectOfType<PlayerAttacker>();

        _player.isBattle = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_player)
            _player = FindObjectOfType<PlayerAttacker>();

        _player.isBattle = false;
    }
}
