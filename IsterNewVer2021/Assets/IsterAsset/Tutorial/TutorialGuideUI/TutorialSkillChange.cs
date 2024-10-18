using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkillChange : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        int dummyroomTutorial = PlayerPrefs.GetInt("SkillChangeTutorial", 0);
        if (dummyroomTutorial != 0)
        {
            Destroy(gameObject);
        }
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

    public void EndTutorial()
    {
        _fade.StartFading(0.0f, true);
        _keyTrigger.isEnable = false;
        _follow.FollowEnd();
        _freezing.UnFreezing();

        PlayerPrefs.SetInt("SkillChangeTutorial", 1);
    }
}
