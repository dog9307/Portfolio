using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class PlayerTeleportCutScene : MonoBehaviour
{
    private PlayerMoveController _player;

    [SerializeField]
    private CinemachineVirtualCamera _startVCam;
    [SerializeField]
    private CinemachineVirtualCamera _zoomVCam;
    [SerializeField]
    private float _beforeZoomTime = 0.0f;
    [SerializeField]
    private float _zoomTime = 2.0f;
    [SerializeField]
    private float _zoomDelayTime = 5.0f;
    [SerializeField]
    private float _teleportTime = 1.0f;
    [SerializeField]
    private float _cameraChangeTime = 1.0f;
    [SerializeField]
    private GameObject _effect;
    [SerializeField]
    private GameObject _teleportEffect;

    public UnityEvent OnTeleportEnd;

    private bool _isCutSceneDoing;

    [SerializeField]
    private SFXPlayer _sfx;

    private StageBGMPlayer _bgm;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();
        _effect.SetActive(false);

        _bgm = FindObjectOfType<StageBGMPlayer>();
        _bgm.StopBGM();
        _bgm.PlayAmbient();

        StartCutScene();
    }

    void Update()
    {
        if (!_isCutSceneDoing) return;

        _player.PlayerMoveFreeze(true);
    }

    void StartCutScene()
    {
        _player.Move(new Vector3(1000.0f, 1000.0f, 1000.0f));

        _startVCam.Priority = 100;

        _isCutSceneDoing = true;

        _bgm.PlayAmbient();
        StartCoroutine(CutScene());
    }

    void CreateEffect()
    {
        GameObject newEffect = Instantiate(_teleportEffect);
        newEffect.transform.position = _effect.transform.position;
    }

    IEnumerator CutScene()
    {
        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();

        yield return new WaitForEndOfFrame();

        _bgm.PlayAmbient();
        _player.PlayerMoveFreeze(true);

        yield return new WaitForSeconds(_beforeZoomTime);

        brain.m_DefaultBlend.m_Time = _zoomTime;

        _startVCam.Priority = -100;
        _zoomVCam.Priority = 100;
        yield return new WaitForSeconds(_zoomTime);

        yield return new WaitForSeconds(_zoomDelayTime);

        _effect.SetActive(true);
        if (_sfx)
            _sfx.PlaySFX("start");

        yield return new WaitForSeconds(_teleportTime);
        CreateEffect();
        yield return new WaitForSeconds(0.1f);
        _player.Move(_effect.transform.position);

        if (_sfx)
            _sfx.PlaySFX("teleport");

        _player.PlayerMoveFreeze(false);

        if (_bgm)
            _bgm.PlayBGM(0.5f);

        CameraShakeController.instance.CameraShake(5.0f);

        _isCutSceneDoing = false;

        brain.m_DefaultBlend.m_Time = _cameraChangeTime;
        _zoomVCam.Priority = -100;

        yield return new WaitForSeconds(_cameraChangeTime);

        brain.m_DefaultBlend.m_Time = 0.0f;

        if (OnTeleportEnd != null)
            OnTeleportEnd.Invoke();
    }
}
