using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointTalkFrom : TalkFrom
{
    [SerializeField]
    private SavePointController _savePoint;
    private Damagable _player;

    [HideInInspector]
    [SerializeField]
    private MapInfoSignal _signal;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (_savePoint)
        {
            _savePoint.Save();

            if (_sfx)
                _sfx.PlaySFX(_sfxName);

            if (!_player)
                _player = FindObjectOfType<PlayerMoveController>().GetComponent<Damagable>();

            if (_player)
                _player.Heal(_player.totalHP);

            if (_signal)
                _signal.Signal();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            CameraShakeController.instance.CameraShake(10.0f);
        }
    }
}
