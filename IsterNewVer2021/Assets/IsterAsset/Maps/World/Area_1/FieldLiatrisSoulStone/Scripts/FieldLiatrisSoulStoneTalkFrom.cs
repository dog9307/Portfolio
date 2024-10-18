using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLiatrisSoulStoneTalkFrom : RelicItemTalkFrom
{
    [SerializeField]
    private CutSceneController _stoneGainCutScene;

    public override void Talk()
    {
        if (_stoneGainCutScene)
            _stoneGainCutScene.StartCutScene();

        base.Talk();
    }
}
