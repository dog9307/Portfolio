using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TowerF1BossDoorOpenCutScene : BossDoorOpenCutSceneBase
{
    [SerializeField]
    private CinemachineVirtualCamera _playerVCam;
    [SerializeField]
    private CinemachineVirtualCamera _doorVCam;

    private CinemachineBrain _brain;

    [SerializeField]
    private float _toDoorVCamTime = 3.0f;
    [SerializeField]
    private float _doorVCamDelayTime = 1.0f;
    [SerializeField]
    private float _toPlayerVCamTime = 1.0f;

    [SerializeField]
    private Collider2D _doorCollider;

    [SerializeField]
    private TowerF1BossDoorAnim _anim;
    [SerializeField]
    private float _openAnimTime = 3.0f;

    [SerializeField]
    private float _effectMoveTime = 3.0f;
    private bool _isEffectMoveEnd;
    private TowerF1BuffEffectController _keyEffect;

    private PlayerMoveController _player;

    [SerializeField]
    private TowerBossBattleCutScene _battleCutscene;

    [SerializeField]
    private SFXPlayer _sfx;

    protected override void ReadyCutScene()
    {
        if (_doorCollider)
            Destroy(_doorCollider);

        _player = FindObjectOfType<PlayerMoveController>();
        _player.PlayerMoveFreeze(true);

        _player.GetComponent<LookAtMouse>().dir = Vector2.up;

        _brain = FindObjectOfType<CinemachineBrain>();
    }

    IEnumerator EffectMove(Transform target)
    {
        if (_sfx)
            _sfx.PlaySFX("flower_flying");

        _isEffectMoveEnd = false;
        target.transform.parent = _effectTargetPos;

        float currentTime = 0.0f;
        Vector2 startPos = target.localPosition;
        while (currentTime < _effectMoveTime)
        {
            float ratio = currentTime / _effectMoveTime;
            target.localPosition = Vector2.Lerp(startPos, Vector2.zero, ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }

        TowerF1FlowerUIController ui = FindObjectOfType<TowerF1FlowerUIController>();
        if (ui)
            ui.UIOff(_keyEffect.type);

        _isEffectMoveEnd = true;
    }

    protected override IEnumerator CutScene()
    {
        KeyManager.instance.Disable("tabUI");
        KeyManager.instance.Disable("menu");

        PlayerAttacker attack = FindObjectOfType<PlayerAttacker>();
        attack.isBattle = false;

        _brain.m_DefaultBlend.m_Time = _toDoorVCamTime;
        _doorVCam.Priority = 999;

        yield return new WaitForSeconds(_toDoorVCamTime + _doorVCamDelayTime);

        // 이펙트 연출
        FindKeyEffect();
        StartCoroutine(EffectMove(_keyEffect.transform));
        while (!_isEffectMoveEnd)
            yield return null;

        _keyEffect.Destroy();
        _anim.OpenAnim();

        yield return new WaitForSeconds(_openAnimTime + 0.5f);

        _brain.m_DefaultBlend.m_Time = _toPlayerVCamTime;
        _doorVCam.Priority = -999;

        yield return new WaitForSeconds(_toPlayerVCamTime);

        _brain.m_DefaultBlend.m_Time = 0.0f;

        _battleCutscene.CutSceneStart();
    }

    private Transform _effectTargetPos;
    void FindKeyEffect()
    {
        TowerF1Manager manager = FindObjectOfType<TowerF1Manager>();
        foreach (var effect in manager.effects)
        {
            if (effect.type == manager.keyFlower.type)
            {
                _keyEffect = effect;
                _effectTargetPos = manager.effectTargetPos;
                break;
            }
        }
    }
}
