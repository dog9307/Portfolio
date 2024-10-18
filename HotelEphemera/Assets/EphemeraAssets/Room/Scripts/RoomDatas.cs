using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RoomOpenEventInfo
{
    public string savableKey;
    public InteractionEvent interact;
    public InteractionConditionBase condition;
    public bool isAutoRun;
}

public class RoomDatas : MonoBehaviour
{
    [SerializeField]
    private Sprite _background;
    [SerializeField]
    private AudioClip _bgmClip;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _bgmVolume;
    [SerializeField]
    private AudioClip _ambClip;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _ambVolume;
    [SerializeField]
    private int _ambChannel = 0;
    [SerializeField]
    private NPCController _currentNPC;
    public NPCController currentNPC { get { return _currentNPC; } set { _currentNPC = value; } }
    [SerializeField]
    private string _roomName;
    public string roomName { get { return _roomName; } }
    [SerializeField]
    private int _roomNumber = 0;
    [SerializeField]
    private string _roomEffectname = "";

    [SerializeField]
    private InteractionConditionBase _roomOpenCondition;

    [SerializeField]
    private RoomOpenEventInfo[] _roomEventInfoes;
    public RoomOpenEventInfo[] roomEventInfoes { get { return _roomEventInfoes; } }

    [SerializeField]
    private AlertSignal _signal;

    [SerializeField]
    private StageBGMPlayer _bgm;

    void Start()
    {
        if (_currentNPC)
            _currentNPC.transform.parent = transform;

        int count = SavableDataManager.instance.FindIntSavableData($"Room_{_roomNumber}_Opened");
        if (count >= 100)
        {
            RoomInfo info = GetComponent<RoomInfo>();
            if (info)
            {
                info._isRoomActivate = true;
                info._isCanSelect = true;
                info._isEvenOnceEnterRoom = true;
            }
        }
    }

    public void OpenRoom()
    {
        if (_roomOpenCondition)
        {
            if (!_roomOpenCondition.IsCanInteraction())
            {
                _signal?.Signal();

                return;
            }
        }

        if (!_bgm)
            _bgm = FindObjectOfType<StageBGMPlayer>();

        if (_bgm)
        {
            if (_bgmClip)
            {
                _bgm.bgm = _bgmClip;
                _bgm.bgmVolume = _bgmVolume;
                _bgm.PlayBGM();
            }

            if (_ambClip)
            {
                _bgm.amb = _ambClip;
                _bgm.ambVolume = _ambVolume;
                _bgm.PlayAmbient(_ambChannel);
            }
        }

        RoomRenderer.instance.currentRoom = this;
        RoomRenderer.instance.roomName = _roomName;
        RoomRenderer.instance.roomNumber = _roomNumber;
        RoomRenderer.instance.SetRoomRenderer(_background, _currentNPC);
        RoomRenderer.instance.EffectPlay(_roomEffectname);
        RoomRenderer.instance.RoomOpen();
    }

    [Header("¹Ì´Ï¸Ê NPC °ü·Ã")]
    [SerializeField]
    private Transform _minimapNPCPos;
    public Transform minimapNPCPos { get { return _minimapNPCPos; } }
}
