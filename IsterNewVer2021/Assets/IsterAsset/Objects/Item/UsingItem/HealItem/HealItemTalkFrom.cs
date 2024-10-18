using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItemTalkFrom : TalkFrom
{
    [SerializeField]
    private float _figure;

    [SerializeField]
    private GameObject _effect;
    [SerializeField]
    private GameObject[] _images;

    [SerializeField]
    private Collider2D _col;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (player)
        {
            player.GetComponent<Damagable>().Heal(_figure);

            _effect.SetActive(true);
            foreach (var i in _images)
                i.SetActive(false);

            _col.enabled = false;
        }

        if (_sfx)
            _sfx.PlaySFX(_sfxName);
    }
}
