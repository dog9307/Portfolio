using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSwordItemTalkFrom : TalkFrom
{
    [SerializeField]
    private UndergroundTutorialCutScene _cutScene;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        Destroy(gameObject);

        PlayerPrefs.SetInt("swordCount", 100);

        FindObjectOfType<PlayerAttacker>().SwordAppear();

        KeyManager.instance.Enable("attack_left");

        _sfx.PlaySFX(_sfxName);

        _cutScene.StartCutScene();
    }
}
