using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HugeCrackTalkFrom : TalkFrom
{
    //[SerializeField]
    //private CutSceneController _firstCutScene;
    [SerializeField]
    private CutSceneController _firstOverMinDarkCutScene;
    [SerializeField]
    private int _darkLightMin = 1000;
    [SerializeField]
    private Collider2D _darkLightCollider;

    [SerializeField]
    private CutSceneController _playerTeleportCutScene;
    //[SerializeField]
    //private CutSceneController _playerCantTeleportCutScene;

    [SerializeField]
    private bool _isInTower = false;
    //[SerializeField]
    //private ParticleSystem _shield;
    //[SerializeField]
    //private SequenceParticleEffector _shieldBlast;
    [SerializeField]
    private GameObject _chains;

    [SerializeField]
    private CutSceneController _teleportedCutScene;

    [SerializeField]
    private CinemachineVirtualCamera _teleportedCutSceneFirstVCam;

    [SerializeField]
    private HugeCrackCondition _condition;

    [SerializeField]
    private ParticleSystem _windEffect;

    [SerializeField]
    private Collider2D _col;

    [SerializeField]
    private DissolveApplier[] _chainDissolves;

    private void OnEnable()
    {
        if (_darkLightCollider)
        {
            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
            _darkLightCollider.enabled = (inventory.darkLight < _darkLightMin);

            if (_col)
                _col.enabled = !_darkLightCollider.enabled;
        }
        else
        {
            if (_col)
                _col.enabled = true;
        }

        //if (_shield)
        //{
        //    _shield.Play();
        //    _shieldBlast.gameObject.SetActive(_shield.gameObject.activeInHierarchy);
        //}

        if (_isInTower)
        {
            SavableNode node = new SavableNode();

            node.key = "PlayerTowerEnter";
            node.value = 100;

            SavableDataManager.instance.AddSavableObject(node);

            SavableDataManager.instance.TowerLobbySave(_startPos.id);
        }
        else
        {
            foreach (var c in _chainDissolves)
                c.currentFade = 1.0f;
        }

        if (_condition)
            enabled = _condition.IsCrackOpen();

        //if (_windEffect)
        //    _windEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Start()
    {
        //_shield.gameObject.SetActive(!_isInTower);
        _chains.SetActive(!_isInTower);
    }

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (!inventory) return;

        //if (inventory.darkLight < _darkLightMin)
        //{
        //    if (_firstCutScene)
        //    {
        //        _firstCutScene.StartCutScene();
        //        _firstCutScene = null;
        //    }
        //    else
        //    {
        //        _playerCantTeleportCutScene.StartCutScene();
        //    }
        //}
        //else
        //{
            if (_firstOverMinDarkCutScene)
            {
                _firstOverMinDarkCutScene.StartCutScene();
                _firstOverMinDarkCutScene = null;
            }
            else
            {
                if (_playerTeleportCutScene)
                    _playerTeleportCutScene.StartCutScene();
            }
        //}
    }

    private PlayerMapChanger _mapChanger;
    public void GoToTower()
    {
        if (!_mapChanger)
            _mapChanger = FindObjectOfType<PlayerMapChanger>();

        if (_mapChanger)
            _mapChanger.MinimapTeleport(TeleportSceneType.Tower, (int)TowerID.TowerLobby, "Field_3", 99999);

        CameraDistortionController distortion = FindObjectOfType<CameraDistortionController>();
        if (distortion)
            BlackMaskController.instance.AddEvent(distortion.ResetIntensity, BlackMaskEventType.MIDDLE);
        BlackMaskController.instance.AddEvent(_mapChanger.TeleportedCutSceneStart, BlackMaskEventType.MIDDLE);
    }

    public void GoToWorld()
    {
        if (!_mapChanger)
            _mapChanger = FindObjectOfType<PlayerMapChanger>();

        if (_mapChanger)
            _mapChanger.MinimapTeleport(TeleportSceneType.World, (int)AreaID.Middle, "Ruin_Field_2", 99999);

        CameraDistortionController distortion = FindObjectOfType<CameraDistortionController>();
        if (distortion)
            BlackMaskController.instance.AddEvent(distortion.ResetIntensity, BlackMaskEventType.MIDDLE);
        BlackMaskController.instance.AddEvent(_mapChanger.TeleportedCutSceneStart, BlackMaskEventType.MIDDLE);
    }

    public void TeleportedCutSceneStart()
    {
        if (_teleportedCutScene)
            _teleportedCutScene.StartCutScene();
    }

    public void PlayerMoveAnyWhere()
    {
        FindObjectOfType<PlayerMoveController>().Move(new Vector3(1000.0f, 1000.0f, 1000.0f));
    }

    [SerializeField]
    private MapStartPos _startPos;
    public void FindPlayerPos()
    {
        if (_startPos)
            _startPos.PlayerStarting();
    }

    public void CameraShake()
    {
        CameraShakeController.instance.CameraShake(10.0f);

        _sfx.PlaySFX(_sfxName);
    }

    public void FirstVCamSetting(int priority)
    {
        if (_teleportedCutSceneFirstVCam)
            _teleportedCutSceneFirstVCam.Priority = priority;
    }
}
