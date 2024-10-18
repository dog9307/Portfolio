using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TowerBossDieCutScene : MonoBehaviour
{
    private PlayerMoveController _player;
    [SerializeField]
    private CinemachineVirtualCamera _bossBattleVCam;
    [SerializeField]
    private CinemachineVirtualCamera _bossDieVCam;

    [SerializeField]
    private Animator _relativeBossAnim;
    [SerializeField]
    private BossHPBarController _bossHPBar;
    [SerializeField]
    private SpriteRenderer _shadow;

    [SerializeField]
    private float _zoomInTime = 5.0f;
    [SerializeField]
    private float _motionTime = 2.0f;
    [SerializeField]
    private float _zoomOutTime = 3.0f;

    [SerializeField]
    private TowerEndStairCutScene _endStair;

    [SerializeField]
    private Graphic _black;
    [SerializeField]
    private float _blackTargetAlpha;
    [SerializeField]
    private Graphic _text;
    [SerializeField]
    private float _blackTime = 1.0f;
    [SerializeField]
    private float _dialogueTime = 0.5f;
    [SerializeField]
    private float _dialogueDelayTime = 2.0f;

    [SerializeField]
    private Collider2D[] _removingWalls;

    //[SerializeField]
    //private TowerRoom _room;

    //[SerializeField]
    //private GameObject _endWall;

    void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();
        _bossDieVCam.Priority = 0;

        ApplyAlpha(_black, 0.0f);
        ApplyAlpha(_text, 0.0f);
    }

    public void StartCutScene()
    {
        _player.PlayerMoveFreeze(true);
        _player.GetComponent<LookAtMouse>().dir = CommonFuncs.CalcDir(_player, _bossDieVCam);

        KeyManager.instance.Disable("tabUI");
        KeyManager.instance.Disable("menu");

        _player.GetComponent<PlayerAttacker>().isBattle = false;

        StartCoroutine(CutScene());
    }

    IEnumerator CutScene()
    {
        CinemachineBrain brain = CinemachineCore.Instance.GetActiveBrain(0);
        float prevTime = brain.m_DefaultBlend.m_Time;

        StageBGMPlayer bgm = FindObjectOfType<StageBGMPlayer>();
        if (bgm)
            bgm.StopSound();

        // zoom in
        _bossHPBar.StartFading(1.0f, 0.0f);

        brain.m_DefaultBlend.m_Time = _zoomInTime;
        _bossBattleVCam.Priority = 0;
        _bossDieVCam.Priority = 110;
        yield return new WaitForSeconds(_zoomInTime);

        // motion sequence
        //_endWall.SetActive(true);

        float currentTime = 0.0f;
        float totalTime = _blackTime;
        float ratio = 0.0f;
        float alpha = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.0f, _blackTargetAlpha, ratio);

            ApplyAlpha(_black, alpha);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        // 대사
        currentTime = 0.0f;
        totalTime = _dialogueTime;
        ratio = 0.0f;
        alpha = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.0f, 1.0f, ratio);

            ApplyAlpha(_text, alpha);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        // 딜레이
        yield return new WaitForSeconds(_dialogueDelayTime);

        // 둘 다 없애기
        currentTime = 0.0f;
        totalTime = _dialogueTime;
        ratio = 0.0f;
        alpha = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;

            alpha = Mathf.Lerp(1.0f, 0.0f, ratio);
            ApplyAlpha(_text, alpha);

            alpha = Mathf.Lerp(_blackTargetAlpha, 0.0f, ratio);
            ApplyAlpha(_black, alpha);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }

        alpha = Mathf.Lerp(1.0f, 0.0f, 1.0f);
        ApplyAlpha(_text, alpha);

        alpha = Mathf.Lerp(_blackTargetAlpha, 0.0f, 1.0f);
        ApplyAlpha(_black, alpha);

        _relativeBossAnim.SetTrigger("dieMotionStart");

        currentTime = 0.0f;
        totalTime = _motionTime;
        Color color;
        SpriteRenderer renderer = _relativeBossAnim.GetComponent<SpriteRenderer>();
        while (currentTime < totalTime)
        {
            if (!renderer)
                break;

            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(1.0f, 0.0f, ratio);

            color = renderer.color;
            color.a = alpha;
            renderer.color = color;

            color = _shadow.color;
            color.a = alpha * 0.4f;
            _shadow.color = color;

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        if (renderer)
        {
            color = renderer.color;
            color.a = 0.0f;
            renderer.color = color;

            color = _shadow.color;
            color.a = alpha * 0.4f;
            _shadow.color = color;
        }

        if (bgm)
            bgm.PlayAmbient();

        // zoom out
        brain.m_DefaultBlend.m_Time = _zoomOutTime;
        if (_player)
            _player.PlayerMoveFreeze(false);

        _bossDieVCam.Priority = 0;
        CinemachineVirtualCamera nextCam = _bossBattleVCam;
        if (!nextCam)
            nextCam = _player.GetComponentInChildren<CinemachineVirtualCamera>();
        nextCam.Priority = 10;

        KeyManager.instance.Enable("tabUI");
        KeyManager.instance.Enable("menu");

        yield return new WaitForSeconds(_zoomOutTime);

        //_room.OpenDoors();

        _player.GetComponent<PlayerAttacker>().isBattle = false;

        foreach (var wall in _removingWalls)
            wall.gameObject.SetActive(false);


        _endStair.EndBattle();
    }

    void ApplyAlpha(Graphic g, float alpha)
    {
        Color color = g.color;
        color.a = alpha;
        g.color = color;
    }
}
