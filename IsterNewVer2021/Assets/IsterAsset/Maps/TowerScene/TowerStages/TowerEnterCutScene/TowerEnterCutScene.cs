using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TowerEnterCutScene : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private CinemachineVirtualCamera _zoomInVCam;
    [SerializeField]
    private float _zoomInTime = 1.0f;
    [SerializeField]
    private float _zoomDelayTime = 2.0f;

    [HideInInspector]
    [SerializeField]
    private WalkThroughCutScene _walkCutScene;

    private PlayerMoveController _player;
    private LookAtMouse _look;
    private Rigidbody2D _rigid;

    [SerializeField]
    private Transform _stair;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();
        _player.PlayerMoveFreeze(true);

        BlackMaskController.instance.AddEvent(CutSceneStart, BlackMaskEventType.POST);
    }

    public void CutSceneStart()
    {
        _look = _player.GetComponent<LookAtMouse>();
        _rigid = _player.GetComponent<Rigidbody2D>();

        _walkCutScene.OnDuringWalk.AddListener(OnDuringWalk);

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
        PlayerAttacker _attack = _player.GetComponent<PlayerAttacker>();

        _player.PlayerMoveFreeze(true);

        _zoomInVCam.Priority = 100;
        yield return new WaitForEndOfFrame();

        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();

        //yield return new WaitForSeconds(_zoomDelayTime);

        brain.m_DefaultBlend.m_Time = _zoomInTime;
        _zoomInVCam.Priority = 100;

        _walkCutScene.StartWalk(_player.transform);
        while (!_walkCutScene.isWalkEnd)
            yield return null;

        _attack.isBattle = true;
        _player.PlayerMoveFreeze(false);

        _zoomInVCam.Priority = -100;
        yield return new WaitForSeconds(1.0f);
        brain.m_DefaultBlend.m_Time = 0.0f;

        if (_stair)
            StartCoroutine(StairDisappear());
    }

    IEnumerator StairDisappear()
    {
        float currentTime = 0.0f;
        float totalTime = 2.0f;
        Vector3 startScale = _stair.localScale;
        Vector3 endScale = new Vector3(0.0f, startScale.y, startScale.z);
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            Vector3 newScale = Vector3.Lerp(startScale, endScale, ratio);

            _stair.localScale = newScale;

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
        _stair.localScale = endScale;
    }
}
