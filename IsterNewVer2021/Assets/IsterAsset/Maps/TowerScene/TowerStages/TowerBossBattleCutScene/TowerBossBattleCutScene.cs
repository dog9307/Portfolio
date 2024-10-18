using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TowerBossBattleCutScene : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private WalkThroughCutScene _walkCutScene;

    private PlayerMoveController _player;
    private LookAtMouse _look;
    private Rigidbody2D _rigid;

    [SerializeField]
    private TowerBossRoomCutScene _bossRoomCutScene;

    [SerializeField]
    private CinemachineVirtualCamera _zoomInVCam;
    [SerializeField]
    private float _zoomInTime = 1.0f;

    public void CutSceneStart()
    {
        _player = FindObjectOfType<PlayerMoveController>();
        _look = _player.GetComponent<LookAtMouse>();
        _rigid = _player.GetComponent<Rigidbody2D>();

        _walkCutScene.OnDuringWalk.AddListener(OnDuringWalk);
        _walkCutScene.startPos.position = _player.transform.position;

        StartCoroutine(CutScene());
    }

    public void OnDuringWalk()
    {
        _look.dir = _walkCutScene.moveDir;
        _rigid.velocity = _walkCutScene.moveDir * 0.02f;
        _rigid.GetComponent<Animator>().SetFloat("velocity", 1.0f);
    }

    IEnumerator CutScene()
    {
        CinemachineBrain brain = CinemachineCore.Instance.GetActiveBrain(0);

        brain.m_DefaultBlend.m_Time = _zoomInTime;
        int prevPrioriy = _zoomInVCam.Priority;
        _zoomInVCam.Priority = 100;

        _player.PlayerMoveFreeze(true);

        _walkCutScene.StartWalk(_player.transform);
        while (!_walkCutScene.isWalkEnd)
            yield return null;

        brain.m_DefaultBlend.m_Time = 0.0f;

        _bossRoomCutScene.StartCutScene();
    }
}
