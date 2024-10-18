using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerVCamController : MonoBehaviour
{
    private CinemachineVirtualCamera _cam;

    private Transform _player;

    private float _defaultOrthoSize;

    public static int PLAYER_PRIORITY = 10;

    void Start()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();

        _player = FindObjectOfType<PlayerMoveController>().transform;

        _defaultOrthoSize = _cam.m_Lens.OrthographicSize;
    }

    public void VCamOn()
    {
        _cam.m_Priority = PLAYER_PRIORITY;
    }

    public void VCamOff()
    {
        _cam.m_Priority = -PLAYER_PRIORITY;
    }

    public void ChageFollowToPlayer()
    {
        _cam.Follow = _player;
    }

    public void ChangeFollow(Transform t)
    {
        _cam.Follow = t;
    }

    public void SetOrthoSize(float size)
    {
        _cam.m_Lens.OrthographicSize = size;
    }

    public void SetDefaultOrthoSize()
    {
        _cam.m_Lens.OrthographicSize = _defaultOrthoSize;
    }
}
