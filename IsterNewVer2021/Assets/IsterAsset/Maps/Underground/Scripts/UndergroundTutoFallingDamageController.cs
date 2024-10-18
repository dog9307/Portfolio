using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundTutoFallingDamageController : MonoBehaviour
{
    private Damagable _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            _player = player.GetComponent<Damagable>();
            if (_player)
                _player.isTutorial = true;
        }
    }

    private void OnDestroy()
    {
        if (_player)
            _player.isTutorial = false;
    }
}
