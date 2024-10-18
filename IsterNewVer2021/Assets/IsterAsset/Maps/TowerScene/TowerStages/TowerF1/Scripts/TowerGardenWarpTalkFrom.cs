using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGardenWarpTalkFrom : TalkFrom
{
    [SerializeField]
    private FieldDoorController _fieldDoor;

    [SerializeField]
    private TowerGardenMoonLight _moonLight;
    [SerializeField]
    private bool _isTurnOn;

    [SerializeField]
    private InteractionEvent _dialogue;
    [SerializeField]
    private CutSceneController _changingCutScene;

    [SerializeField]
    private TowerGardenManager _manager;

    void Update()
    {
        enabled = !_manager.isBossBattleEnd;
    }

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (_dialogue)
        {
            //_dialogue.StartDialogue();
            _manager.GoToBossRoom();
        }
        else
        {
            if (_changingCutScene)
                _changingCutScene.StartCutScene();
            else
                GoToMoonLightGarden();
        }
    }

    public void GoToMoonLightGarden()
    {
        if (_fieldDoor)
        {
            _sfx.PlaySFX(_sfxName);
            _fieldDoor.GoToNextField();
        }

        if (_moonLight)
            _moonLight.Switch(_isTurnOn);
    }
}
