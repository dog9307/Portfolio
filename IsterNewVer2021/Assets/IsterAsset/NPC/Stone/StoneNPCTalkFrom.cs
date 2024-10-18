using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneNPCTalkFrom : NpcTalkFrom
{
    private int _talkCount = 0;

    [SerializeField]
    private int _talkCountMax = 20;
    [SerializeField]
    private string _endingDialogueName = "";
    [SerializeField]
    private string _repeatDialogueName = "StoneNPCRepeat";

    public bool isAlreadyEnding { get; set; } = false;

    private void OnEnable()
    {
        _talkCount = 0;

        int count = PlayerPrefs.GetInt("Ending_OpusAndStone", -1);
        if (count >= 100)
        {
            isAlreadyEnding = true;
            _interaction.dialogueName = _repeatDialogueName;
        }
    }

    public override void Talk()
    {
        if (!isAlreadyEnding)
        {
            _talkCount++;
            if (_talkCount >= _talkCountMax)
                _interaction.dialogueName = _endingDialogueName;
        }

        base.Talk();
    }
}
