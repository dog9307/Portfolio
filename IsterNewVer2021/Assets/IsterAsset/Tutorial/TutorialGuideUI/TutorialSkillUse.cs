using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkillUse : MonoBehaviour
{
    private PlayerMoveController _player;
    private PlayerSkillUsage _playerSkill;

    [SerializeField]
    private FadingGuideUI _fade;
    [SerializeField]
    private TutorialFadingKeyTrigger _keyTrigger;
    [SerializeField]
    private FollowingPlayerUI _follow;
    [SerializeField]
    private TutorialPlayerFreezing _freezing;

    public bool isAlreadyStart { get; set; } = false;

    [SerializeField]
    private bool _isStartWithStart = true;

    void Start()
    {
        if (_isStartWithStart)
            StartTutorial();
    }

    public void StartTutorial()
    {
        if (isAlreadyStart) return;

        isAlreadyStart = true;

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();
        if (!_playerSkill)
            _playerSkill = _player.GetComponent<PlayerSkillUsage>();

        _fade.StartFading(1.0f);
        _keyTrigger.isEnable = true;
        _follow.FollowStart();
        _freezing.Freezing();
    }

    public void StartTutorial(bool ignoreAlreadyStart)
    {
        if (isAlreadyStart && !ignoreAlreadyStart) return;

        isAlreadyStart = true;

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();
        if (!_playerSkill)
            _playerSkill = _player.GetComponent<PlayerSkillUsage>();

        _fade.StartFading(1.0f);
        _keyTrigger.isEnable = true;
        _follow.FollowStart();
        _freezing.Freezing();
    }

    public void UseSkill()
    {
        if (!_playerSkill) return;

        //_playerSkill.UseSkill(_playerSkill.currentSkillIndex);
        _playerSkill.UseDash();
    }
}
