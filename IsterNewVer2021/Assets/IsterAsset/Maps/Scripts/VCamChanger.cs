using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamChanger : MonoBehaviour
{
    [SerializeField]
    private float _enterBlendTime = 3.0f;
    public float enterBlendTime { get { return _enterBlendTime; } set { _enterBlendTime = value; } }
    [SerializeField]
    private float _exitBlendTime = 1.0f;
    [SerializeField]
    private bool _isApplyExitBlendTime = true;

    [SerializeField]
    private CinemachineVirtualCamera _vcam;
    [SerializeField]
    private int _priority = 100;

    private CinemachineBrain _brain;

    private Coroutine _prevCoroutine;

    private PlayerMoveController _player;

    [SerializeField]
    private bool _isPlayerFreezeDuringChange = false;
    [SerializeField]
    private bool _isJustOnce = true;

    void OnEnable()
    {
        _vcam.Priority = -_priority;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            _player = player;

            if (_prevCoroutine != null)
                StopCoroutine(_prevCoroutine);

            _prevCoroutine = StartCoroutine(ChangeVCam(_enterBlendTime, _priority));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (BlackMaskController.instance.isFading) return;
        if (!_isApplyExitBlendTime)
        {
            _vcam.Priority = -_priority;
            return;
        }

        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            _player = player;

            if (_prevCoroutine != null)
                StopCoroutine(_prevCoroutine);

            _prevCoroutine = StartCoroutine(ChangeVCam(_exitBlendTime, -_priority));
        }
    }

    IEnumerator ChangeVCam(float blendTime, int toPriority)
    {
        if (_isPlayerFreezeDuringChange)
            _player.PlayerMoveFreeze(true);

        if (!_brain)
            _brain = FindObjectOfType<CinemachineBrain>();

        if (!BlackMaskController.instance.isFading)
            _brain.m_DefaultBlend.m_Time = blendTime;
        _vcam.Priority = toPriority;

        float currentTime = 0.0f;
        while (currentTime < blendTime)
        {
            yield return null;

            currentTime += IsterTimeManager.deltaTime;
        }

        _brain.m_DefaultBlend.m_Time = 0.0f;

        _prevCoroutine = null;

        if (_isPlayerFreezeDuringChange)
        {
            _player.PlayerMoveFreeze(false);

            if (_isJustOnce)
                _isPlayerFreezeDuringChange = false;
        }
    }
}
