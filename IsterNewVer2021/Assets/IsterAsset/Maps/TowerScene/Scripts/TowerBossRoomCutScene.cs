using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TowerBossRoomCutScene : MonoBehaviour
{
    private PlayerMoveController _player;
    [SerializeField]
    private CinemachineVirtualCamera _bossRoomVCam;
    [SerializeField]
    private CinemachineVirtualCamera _bossBattleVCam;
    [SerializeField]
    private CinemachineVirtualCamera _bossZoomVCam;


    [SerializeField]
    private float _bossZoomBlendTime = 1.0f;
    [SerializeField]
    private float _bossZoomTime = 4.0f;
    [SerializeField]
    private float _playerLookTime = 2.0f;
    [SerializeField]
    private float _zoomOutTime = 5.0f;

    [SerializeField]
    private BossControllerBase _relativeBoss;
    [SerializeField]
    private BossHPBarController _relativeBossHPBar;

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
    private Collider2D[] _tempWalls;

    // Start is called before the first frame update
    void Start()
    {
        //MoveRoomBlackMask.instance.AddEvent(StartCutScene, BlackMaskEventType.POST);

        _player = FindObjectOfType<PlayerMoveController>();

        ApplyAlpha(_black, 0.0f);
        ApplyAlpha(_text, 0.0f);
    }

    public void StartCutScene()
    {
        _player.PlayerMoveFreeze(true);
        _player.GetComponent<LookAtMouse>().dir = Vector2.up;

        StartCoroutine(CutScene());
    }

    IEnumerator CutScene()
    {
        StageBGMPlayer bgm = FindObjectOfType<StageBGMPlayer>();
        if (bgm)
            bgm.StopBGM();

        yield return new WaitForSeconds(_playerLookTime);

        CinemachineBrain brain = CinemachineCore.Instance.GetActiveBrain(0);
        float prevTime = brain.m_DefaultBlend.m_Time;

        brain.m_DefaultBlend.m_Time = _bossZoomBlendTime;
        _bossZoomVCam.Priority = 101;
        yield return new WaitForSeconds(_bossZoomTime + _bossZoomBlendTime);

        brain.m_DefaultBlend.m_Time = _zoomOutTime;
        _bossRoomVCam.m_Lens.OrthographicSize = 20;
        _bossRoomVCam.Priority = 102;
        yield return new WaitForSeconds(_zoomOutTime);

        // 블랙
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

        if (bgm)
            bgm.PlayBossBGM();

        if (_relativeBossHPBar)
        {
            _relativeBossHPBar.StartBattle();
            while (!_relativeBossHPBar.isBattleStart)
                yield return null;
        }

        brain.m_DefaultBlend.m_Time = prevTime;
        if (_relativeBoss)
            _relativeBoss.BossWakeUp();
        if (_player)
            _player.PlayerMoveFreeze(false);

        _player.GetComponent<PlayerAttacker>().isBattle = true;

        KeyManager.instance.Enable("tabUI");
        KeyManager.instance.Enable("menu");

        brain.m_DefaultBlend.m_Time = 1.0f;
        _bossBattleVCam.Priority = 103;
        yield return new WaitForSeconds(1.0f);

        brain.m_DefaultBlend.m_Time = prevTime;

        foreach (var wall in _tempWalls)
            wall.gameObject.SetActive(true);
    }

    void ApplyAlpha(Graphic g, float alpha)
    {
        Color color = g.color;
        color.a = alpha;
        g.color = color;
    }
}
