using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SequenceCameraChanging : CutSceneSqeunceBase
{
    private static CinemachineBrain _brain;

    [Header("카메라")]
    [SerializeField]
    private bool _isUseActiveVCam = false;
    [SerializeField]
    private CinemachineVirtualCamera _vCam;
    [SerializeField]
    private Transform _playerLookTarget;

    [SerializeField]
    private bool _isReturnVCamWithEnd = false;
    private static int Priority = 1000;

    [SerializeField]
    private bool _isOrthographicChange = false;
    [SerializeField]
    private float _orthoSize = 16.0f;
    
    void Start()
    {
        if (_vCam)
            _vCam.Priority = -9999;
    }

    protected override IEnumerator DuringSequence()
    {
        if (_playerLookTarget)
            playerLook.dir = CommonFuncs.CalcDir(player, _playerLookTarget);

        if (!_brain)
            _brain = FindObjectOfType<CinemachineBrain>();

        _brain.m_DefaultBlend.m_Time = _sequenceTime;

        if (_isUseActiveVCam)
            _vCam = FindObjectOfType<FindingPlayerVCam>().cam;

        _vCam.Priority = Priority;
        Priority++;

        if (_isOrthographicChange)
            _vCam.m_Lens.OrthographicSize = _orthoSize;

        yield return new WaitForSeconds(_sequenceTime);

        if (_isReturnVCamWithEnd)
            _vCam.Priority = -Priority;

        _brain.m_DefaultBlend.m_Time = 0.0f;

        _isDuringSequence = false;
    }
}
