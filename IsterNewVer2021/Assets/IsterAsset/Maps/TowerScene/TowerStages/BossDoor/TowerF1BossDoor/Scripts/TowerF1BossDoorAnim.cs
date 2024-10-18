using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1BossDoorAnim : BossDoorCloseAnimBase
{
    public override void CloseAnim()
    {
        if (_sfx)
            _sfx.PlaySFX("close");

        _anim.SetTrigger("shake");
    }

    public override void OpenAnim()
    {
        QuestListManager questManager = FindObjectOfType<QuestListManager>();
        if (questManager)
            questManager.QuestUpdate(100);

        Damagable player = FindObjectOfType<PlayerMoveController>().GetComponent<Damagable>();
        if (player)
        {
            if (!player.isAleardyHitted)
            {
                if (questManager)
                    questManager.QuestUpdate(102);
            }

        }

        StageBGMPlayer bgm = FindObjectOfType<StageBGMPlayer>();
        if (bgm)
        {
            bgm.StopBGM();
            bgm.StopAmbient();
        }

        if (_sfx)
            _sfx.PlaySFX("open");

        _anim.SetBool("isOpen", true);
    }
}
