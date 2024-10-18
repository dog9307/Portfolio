using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalHPItem : TalkFrom
{
    [SerializeField]
    private float _figure;

    [SerializeField]
    private GameObject _effect;

    [SerializeField]
    private Collider2D _col;

    [SerializeField]
    private DisposableObject _disposable;

    [SerializeField]
    private GameObject[] _destroyAfterTalk;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (player)
        {
            player.GetComponent<Damagable>().TotalHPUp(_figure);

            _effect.SetActive(true);

            _col.enabled = false;

            foreach (var o in _destroyAfterTalk)
                o.SetActive(false);
        }

        if (_sfx)
            _sfx.PlaySFX(_sfxName);

        _disposable.UseObject();
    }

    [SerializeField]
    private GameObject _darkLight;
    public void CreateDarkLight()
    {
        GameObject newDark = Instantiate(_darkLight);
        newDark.transform.position = transform.position;

        DarkLightTalkFrom talkFrom = newDark.GetComponent<DarkLightTalkFrom>();
        if (talkFrom)
        {
            talkFrom.figure = 100;
            talkFrom.isAutoGain = true;
        }
    }
}
