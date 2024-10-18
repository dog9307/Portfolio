using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDoor : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private TowerDoorEffectHelper _effect;

    [SerializeField]
    private Collider2D _doorCollider;

    public bool isAlwaysClose { get; set; } = false;

    [HideInInspector]
    [SerializeField]
    private MapStartPos _startingPos;
    public int startPosID { get { if (!_startingPos) return -1; return _startingPos.id; } }

    public TowerDoorManager manager { get; set; }

    [SerializeField]
    private bool _isStartDoor = false;

    [SerializeField]
    private SpriteRenderer _nextRewardPreview;
    [SerializeField]
    private GameObject _guideArrow;
    private GameObject _nextReward;
    [SerializeField]
    private bool _isRewardDoor = true;

    private bool _isBossDoor = false;

    void OnEnable()
    {
        if (!_effect)
            _effect = GetComponentInChildren<TowerDoorEffectHelper>();

        if (_isStartDoor)
            OpenDoor();
    }

    void OnDisable()
    {
        ReturnItem();
    }

    private void GetNextReward()
    {
        if (_isBossDoor) return;
        if (!_isRewardDoor) return;

        if (!TowerItemRewardManager.instance) return;

        _nextReward = TowerItemRewardManager.instance.GetNextItem();
        if (!_nextReward) return;

        SpriteRenderer sprite = _nextReward.GetComponentInChildren<SpriteRenderer>();
        _nextRewardPreview.sprite = sprite.sprite;
        _nextReward.transform.parent = transform;
        _nextRewardPreview.gameObject.SetActive(true);

        SkillItemTalkFrom item = _nextReward.GetComponent<SkillItemTalkFrom>();
        if (item)
        {
            Vector3 scale = _nextRewardPreview.transform.localScale;
            scale.x *= item.previewScale;
            scale.y *= item.previewScale;
            _nextRewardPreview.transform.localScale = scale;
        }
    }

    private void ReturnItem()
    {
        if (!_nextReward) return;
        if (!TowerItemRewardManager.instance) return;

        TowerItemRewardManager.instance.ReturnItem(_nextReward);
    }

    private int nextStartPosID;

    public void PlayerFreezing()
    {
        FindObjectOfType<PlayerMoveController>().PlayerMoveFreeze(true);
    }

    public void PlayerUnfreezing()
    {
        FindObjectOfType<PlayerMoveController>().PlayerMoveFreeze(false);
    }

    public void OpenMap()
    {
        TowerRoomSetter roomSetter = FindObjectOfType<TowerRoomSetter>();
        if (!roomSetter) return;

        nextStartPosID = 0;

        roomSetter.CloseCurrentRoom();
        TowerRoom nextRoom = roomSetter.OpenNextRoom();
        if (nextRoom)
        {
            TowerDoor nextDoor = nextRoom.GetRandomDoor();
            if (nextDoor)
            {
                nextStartPosID = nextDoor.startPosID;
                nextDoor.isAlwaysClose = true;
                nextDoor.SetToNormalDoor();
            }
        }

        TowerItemRewardManager.instance.ReturnReward();
        SetReward();
    }

    public void ToNextRoom()
    {
        MoveRoomBlackMask.instance.AddEvent(PlayerFreezing, BlackMaskEventType.PRE);
        MoveRoomBlackMask.instance.AddEvent(OpenMap, BlackMaskEventType.MIDDLE);
        MoveRoomBlackMask.instance.AddEvent(FindStartPos, BlackMaskEventType.MIDDLE);
        MoveRoomBlackMask.instance.AddEvent(PlayerUnfreezing, BlackMaskEventType.POST);

        MoveRoomBlackMask.instance.from = _startingPos.dir;
        MoveRoomBlackMask.instance.StartFading(1.0f, 1.0f);
    }

    public void FindStartPos()
    {
        MapStartPos[] poses = FindObjectsOfType<MapStartPos>();
        foreach (var pos in poses)
        {
            if (pos.id == nextStartPosID)
            {
                pos.PlayerStarting();
                if (MoveRoomBlackMask.instance)
                    MoveRoomBlackMask.instance.to = pos.dir;
                break;
            }
        }
    }

    public void OpenDoor()
    {
        if (isAlwaysClose) return;

        if (_effect)
            _effect.EffectOff();
        if (_doorCollider)
            _doorCollider.isTrigger = true;

        if (_guideArrow)
            _guideArrow.SetActive(true);

        GetNextReward();
    }

    public void CloseDoor()
    {
        if (_effect)
            _effect.EffectOn();
        if (_doorCollider)
            _doorCollider.isTrigger = false;
    }

    public void SetReward()
    {
        if (!_nextReward) return;

        TowerItemRewardManager.instance.reward = _nextReward;
        _nextReward = null;
    }

    public void SetToNormalDoor()
    {
        _effect.SetToNormalEffect();
        _isBossDoor = false;
    }

    public void SetToBossDoor()
    {
        _effect.SetToBossEffect();
        _isBossDoor = true;
    }
}
