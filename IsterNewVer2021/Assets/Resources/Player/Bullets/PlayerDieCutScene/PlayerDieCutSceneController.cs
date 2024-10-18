using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerDieCutSceneController : MonoBehaviour
{
    public bool isAlreadyStart { get; set; }

    private PlayerMoveController _player;
    [SerializeField]
    private CinemachineVirtualCamera _playerVCam;

    [SerializeField]
    private float _fadeTime = 1.0f;
    private bool _isFadeEnd;

    [SerializeField]
    private float _moveTime = 1.0f;
    private bool _isMovingEnd;

    private CinemachineBrain _brain;

    private GameObject _dieEffect;

    [SerializeField]
    private GameObject _sceneSkipper;

    [SerializeField]
    private float _dieSceneChangeDelay = 5.0f;

    void Start()
    {
        isAlreadyStart = false;
        _playerVCam.gameObject.SetActive(false);
        _sceneSkipper.SetActive(false);
    }

    public void StartDieCutScene()
    {
        if (isAlreadyStart) return;

        isAlreadyStart = true;

        _brain = FindObjectOfType<CinemachineBrain>();
        _player = FindObjectOfType<PlayerMoveController>();
        _playerVCam.gameObject.SetActive(true);

        _isFadeEnd = false;
        _isMovingEnd = false;

        InGameUIFinder ingameUI = FindObjectOfType<InGameUIFinder>();
        if (ingameUI)
            ingameUI.GraphicsOff();

        IsterTimeManager.globalTimeScale = 0.0f;

        BossHPBarController bossHP = FindObjectOfType<BossHPBarController>();
        if (bossHP)
        {
            if (bossHP.isBattleStart)
                bossHP.EndBattle();
        }

        _dieEffect = FindObjectOfType<PlayerDieEffectFindHelper>().relativeObj;

        FindObjectOfType<StageBGMPlayer>().StopSound();

        if (_sceneSkipper)
            _sceneSkipper.SetActive(true);

        _player.GetComponent<PlayerAttacker>().isBattle = false;

        KeyManager.instance.Disable("tabUI");
        KeyManager.instance.Disable("menu");

        SoundSystem.instance.StopLoopSFXAll();

        StartCoroutine(DieCutScene());
    }

    IEnumerator DieCutScene()
    {
        //int playerDie = PlayerPrefs.GetInt("isPlayerDie", 0);
        //playerDie++;
        //PlayerPrefs.SetInt("isPlayerDie", playerDie);

        //PlayerPrefs.SetInt("quest_103", 100);

        float prevBlendTime = _brain.m_DefaultBlend.m_Time;

        _brain.m_DefaultBlend.m_Time = 3.0f;
        _playerVCam.Priority = 9999;

        while (!_isFadeEnd)
            yield return null;

        while (!_isMovingEnd)
            yield return null;

        if (_dieEffect)
            _dieEffect.SetActive(true);

        yield return new WaitForSeconds(_dieSceneChangeDelay);

        SavableDataManager.instance.PlayerDieSave();

        _brain.m_DefaultBlend.m_Time = prevBlendTime;
        DieEnd();

        KeyManager.instance.Enable("tabUI");
        KeyManager.instance.Enable("menu");
    }

    public void PlayerDieFadeEnd()
    {
        _isFadeEnd = true;
    }

    public void PlayerDieMoveEnd()
    {
        _isMovingEnd = true;
    }

    public void DieEnd()
    {
        IsterTimeManager.globalTimeScale = 1.0f;

        SceneManager.LoadScene("PlayerDieScene", LoadSceneMode.Single);
    }
}
