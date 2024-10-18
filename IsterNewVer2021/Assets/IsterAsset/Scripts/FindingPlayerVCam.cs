using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindingPlayerVCam : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _cam;
    public CinemachineVirtualCamera cam { get { return _cam; } }

    [SerializeField]
    private bool _isFollowingOriginPlayer = true;

    private PlayerMoveController _player;
    private CameraFallow _fallow;

    // Update is called once per frame
    void Update()
    {
        if (_cam.Follow) return;

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();
        if (!_fallow)
            _fallow = FindObjectOfType<CameraFallow>();

        if (_player)
        {
            if (_isFollowingOriginPlayer)
                _cam.Follow = _player.transform;
            else
                _cam.Follow = _fallow.transform;
        }
    }
}
