using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeleportTalkFrom : TalkFromStayKey
{
    [SerializeField]
    private int _minimapButtonID;

    [SerializeField]
    private ParticleSystem _stayEffect;
    private PlayerAnimController _player;

    [SerializeField]
    private ParticleSystem _teleportEffect;

    [SerializeField]
    private GlowUpAndDown _glow;
    [SerializeField]
    private float _glowMultiplier = 2.0f;
    private float _startFrequency;

    [SerializeField]
    private bool _isTutorialEnd = false;

    void Start()
    {
        _startFrequency = _glow.frequency;
    }

    private void OnEnable()
    {
        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();
        CinemachineVirtualCamera activeCam = brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        //activeCam.Follow = null;

        if (activeCam)
        {
            FindingPlayerVCam finding = activeCam.GetComponent<FindingPlayerVCam>();
            if (finding)
                finding.enabled = true;
        }
    }

    void Update()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerAnimController>();

        if (!_player) return;

        _stayEffect.transform.position = _player.transform.position;

        if (ratio <= 0.0f)
        {
            _stayEffect.Stop();

            _player.PlayerStayInteract(false);
        }
        else
        {
            if (!_stayEffect.isPlaying)
            {
                _sfx.PlaySFX("charging");
                _stayEffect.Play();
            }

            _player.PlayerStayInteract(true);
        }
    }

    public override void ResetRatio()
    {
        base.ResetRatio();

        if (!_player)
            _player = FindObjectOfType<PlayerAnimController>();

        _player.PlayerStayInteract(false);

        _glow.ratio = 1.0f;
        _glow.frequency = _startFrequency;
    }

    public override void UpdateRatio(float deltaTime)
    {
        base.UpdateRatio(deltaTime);

        _glow.ratio = (1.0f + ratio * _glowMultiplier);
        _glow.frequency = Mathf.Lerp(_startFrequency, _startFrequency * 3.0f, ratio);
    }

    public void Teleport()
    {
        _teleportEffect.transform.position = _player.transform.position;

        _teleportEffect.Play();

        _stayEffect.Stop();

        Vector3 newPos = _player.transform.position;
        newPos.x += 1000.0f;
        _player.transform.position = newPos;

        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();
        CinemachineVirtualCamera activeCam = brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        activeCam.Follow = null;

        FindingPlayerVCam finding = activeCam.GetComponent<FindingPlayerVCam>();
        if (finding)
            finding.enabled = false;
    }

    // test
    [Header("버닝비버용")]
    [SerializeField]
    private TeleportSceneType _type;
    [SerializeField]
    private string _toFieldName;
    [SerializeField]
    private AreaID _areaID;
    [SerializeField]
    private TowerID _towerID;
    [SerializeField]
    private int _startPosID;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        //UIManager.instance.MinimapOpen();

        MinimapIconList iconList = FindObjectOfType<MinimapIconList>();
        if (iconList)
            iconList.ButtonOn(_minimapButtonID);

        if (SavableDataManager.instance)
        {
            SavableNode node = new SavableNode();
            node.key = "minimap_button_" + _minimapButtonID.ToString();
            node.value = 100;

            SavableDataManager.instance.AddSavableObject(node);
        }

        PlaySFX();

        // test
        PlayerMapChanger playerMapChanger = FindObjectOfType<PlayerMapChanger>();
        if (playerMapChanger)
        {
            switch (_type)
            {
                case TeleportSceneType.World:
                    playerMapChanger.MinimapTeleport(_type, (int)_areaID, _toFieldName, _startPosID, _isTutorialEnd);
                break;

                case TeleportSceneType.Tower:
                    playerMapChanger.MinimapTeleport(_type, (int)_towerID, _toFieldName, _startPosID);
                break;
            }
        }

        Teleport();
    }
}
