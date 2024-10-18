using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDummyRoomFlow : MonoBehaviour
{
    private PlayerMoveController _player;

    [SerializeField]
    private GameObject _attackTrigger;

    [SerializeField]
    private FollowingPlayerUI _attackFollow;
    [SerializeField]
    private FadingGuideUI _attackFader;
    [SerializeField]
    private TutorialFadingKeyTrigger _attackKeyTrigger;

    [SerializeField]
    private GameObject _attackGuide;
    [SerializeField]
    private Animation _attackGuideAnim;
    [SerializeField]
    private GameObject _attackGuideCollider;

    [SerializeField]
    private TowerDummyRoomTutorialCutScene _cutscene;
    [SerializeField]
    private Animator _dummyAnim;

    // Start is called before the first frame update
    void Start()
    {
        int dummyroomTutorial = PlayerPrefs.GetInt("DummyroomTutorial", 0);
        if (dummyroomTutorial != 0)
        {
            Destroy(gameObject);

            _dummyAnim.SetBool("isEyeOpen", true);
        }
        else
        {
            _player = FindObjectOfType<PlayerMoveController>();
            _player.PlayerMoveFreeze(true);
            //FindObjectOfType<MapInfo>().AddEvent(AfterShowInfo);

            _attackGuideCollider.SetActive(false);

            _dummyAnim.SetBool("isEyeOpen", false);
        }
    }

    public void AfterShowInfo()
    {
        _player.PlayerMoveFreeze(false);
    }

    public void DestroyAttackTrigger()
    {
        if (_attackTrigger)
            Destroy(_attackTrigger);

        if (_attackKeyTrigger)
        {
            KeyManager.instance.Enable("attack_left");

            _attackFollow.FollowStart();
            _attackFader.StartFading(1.0f);
            _attackKeyTrigger.isEnable = true;

            _attackGuide.SetActive(true);
            _attackGuideAnim.Play("tutorial_mouse_left_click");
            _attackGuideCollider.SetActive(true);
        }
    }

    public void StartTutorialCutScene()
    {
        if (_cutscene)
            _cutscene.StartCutScene();
    }
}
