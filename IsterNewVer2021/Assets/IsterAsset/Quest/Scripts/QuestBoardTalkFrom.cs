using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoardTalkFrom : TalkFrom
{
    private QuestBoardUIController _questBoard;

    [SerializeField]
    private GameObject _effectPrefab;
    public GameObject effectPrefab { get { return _effectPrefab; } set { _effectPrefab = value; } }
    [SerializeField]
    private Transform _effectPos;

    public GameObject CreateObject()
    {
        GameObject newEffect = Instantiate(_effectPrefab);
        newEffect.transform.position = _effectPos.position;

        if (_sfx)
            _sfx.PlaySFX("activate");

        return newEffect;
    }

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        CreateObject();

        QuestCutScene cutscene = FindObjectOfType<QuestCutScene>();
        if (cutscene)
        {
            if (cutscene.isAlreadyStart)
                BoardOpen();
            else
                cutscene.StartCutScene();
        }
        else
            BoardOpen();
    }

    public void BoardOpen()
    {
        if (!_questBoard)
            _questBoard = FindObjectOfType<QuestBoardUIController>();

        _questBoard.BoardOn();

        if (_sfx)
            _sfx.PlaySFX("open");
    }
}
